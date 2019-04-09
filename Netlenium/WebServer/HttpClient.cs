﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace Netlenium.WebServer
{
    internal class HttpClient : IDisposable
    {

        private static readonly Regex PrologRegex = new Regex("^([A-Z]+) ([^ ]+) (HTTP/[^ ]+)$", RegexOptions.Compiled);

        private bool _disposed;
        private readonly byte[] _writeBuffer;
        private NetworkStream _stream;
        private ClientState _state;
        private MemoryStream _writeStream;
        private HttpRequestParser _parser;
        private HttpContext _context;
        private bool _errored;

        public HttpServer Server { get; private set; }

        public TcpClient TcpClient { get; private set; }

        public string Method { get; private set; }

        public string Protocol { get; private set; }

        public string Request { get; private set; }

        public Dictionary<string, string> Headers { get; private set; }

        public NameValueCollection PostParameters { get; set; }

        public List<HttpMultiPartItem> MultiPartItems { get; set; }

        public HttpReadBuffer ReadBuffer { get; private set; }

        public Stream InputStream { get; set; }

        public HttpClient(HttpServer server, TcpClient client)
        {
            Server = server ?? throw new ArgumentNullException("server");
            TcpClient = client ?? throw new ArgumentNullException("client");

            ReadBuffer = new HttpReadBuffer(server.ReadBufferSize);
            _writeBuffer = new byte[server.WriteBufferSize];

            _stream = client.GetStream();
        }

        private void Reset()
        {
            _state = ClientState.ReadingProlog;
            _context = null;

            if (_parser != null)
            {
                _parser.Dispose();
                _parser = null;
            }

            if (_writeStream != null)
            {
                _writeStream.Dispose();
                _writeStream = null;
            }

            if (InputStream != null)
            {
                InputStream.Dispose();
                InputStream = null;
            }

            ReadBuffer.Reset();

            Method = null;
            Protocol = null;
            Request = null;
            Headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            PostParameters = new NameValueCollection();

            if (MultiPartItems != null)
            {
                foreach (var item in MultiPartItems)
                {
                    if (item.Stream != null)
                        item.Stream.Dispose();
                }

                MultiPartItems = null;
            }
        }

        public void BeginRequest()
        {
            Reset();

            BeginRead();
        }

        private void BeginRead()
        {
            if (_disposed)
                return;

            try
            {
                Server.TimeoutManager.ReadQueue.Add(
                    ReadBuffer.BeginRead(_stream, ReadCallback, null),
                    this
                );
            }
            catch (Exception ex)
            {
                Logging.WriteEntry(Types.LogType.Information, "Netlenium.WebServer", $"BeginRead failed {ex}");

                Dispose();
            }
        }

        private void ReadCallback(IAsyncResult asyncResult)
        {
            if (_disposed)
                return;

            // The below state matches the RequestClose state. Dispose immediately
            // when this occurs.

            if (
                _state == ClientState.ReadingProlog &&
                Server.State != HttpServerState.Started
            )
            {
                Dispose();
                return;
            }

            try
            {
                if (ReadBuffer == null)
                {
                    Dispose();
                }
                try
                {
                    ReadBuffer.EndRead(_stream, asyncResult);
                } catch
                {

                    /// <summary>
                    /// SLOWLORIS ATTACK PREVENTION
                    /// 
                    /// This behavior is quite similar to a SlowHTTP-type attack, meaning the client will open a 
                    /// connection, and won't let the server end it due to regulations of the HTTP protocol.
                    /// 
                    /// Through this, we'll dispose the current Http client by terminating the current HTTP connection
                    /// forcefully.
                    /// 
                    /// This catch check may be safely ignored as ReadBuffer is now directly processed and it's operation is returned to the client.
                    /// 
                    /// </summary>

                }
                

                if (ReadBuffer.DataAvailable)
                {
                    ProcessReadBuffer();
                    return;
                }
                else
                {
                    Dispose();
                    return;
                }
                    
            }
            catch (ObjectDisposedException ex)
            {
                Logging.WriteEntry(Types.LogType.Information, "Netlenium.WebServer", $"Failed to read {ex}");

                Dispose();
            }
            catch (Exception ex)
            {
                Logging.WriteEntry(Types.LogType.Information, "Netlenium.WebServer", $"Failed to read from the HTTP connection {ex}");

                ProcessException(ex);
            }
        }

        private void ProcessReadBuffer()
        {
            while (_writeStream == null && ReadBuffer.DataAvailable)
            {
                switch (_state)
                {
                    case ClientState.ReadingProlog:
                        ProcessProlog();
                        break;

                    case ClientState.ReadingHeaders:
                        ProcessHeaders();
                        break;

                    case ClientState.ReadingContent:
                        ProcessContent();
                        break;

                    default:
                        throw new InvalidOperationException("Invalid state");
                }
            }

            if (_writeStream == null)
                BeginRead();
        }

        private void ProcessProlog()
        {
            string line = ReadBuffer.ReadLine();

            if (line == null)
                return;

            var match = PrologRegex.Match(line);

            if (!match.Success)
                throw new ProtocolException(String.Format("Could not parse prolog '{0}'", line));

            Method = match.Groups[1].Value;
            Request = match.Groups[2].Value;
            Protocol = match.Groups[3].Value;

            // Continue reading the headers.

            _state = ClientState.ReadingHeaders;

            ProcessHeaders();
        }

        private void ProcessHeaders()
        {
            string line;

            while ((line = ReadBuffer.ReadLine()) != null)
            {
                // Have we completed receiving the headers?

                if (line.Length == 0)
                {
                    // Reset the read buffer which resets the bytes read.

                    ReadBuffer.Reset();

                    // Start processing the body of the request.

                    _state = ClientState.ReadingContent;

                    ProcessContent();

                    return;
                }

                string[] parts = line.Split(new[] { ':' }, 2);

                if (parts.Length != 2)
                    throw new ProtocolException("Received header without colon");

                Headers[parts[0].Trim()] = parts[1].Trim();
            }
        }

        private void ProcessContent()
        {
            if (_parser != null)
            {
                _parser.Parse();
                return;
            }

            if (ProcessExpectHeader())
                return;

            if (ProcessContentLengthHeader())
                return;

            // The request has been completely parsed now.

            ExecuteRequest();
        }

        private bool ProcessExpectHeader()
        {
            // Process the Expect: 100-continue header.


            if (Headers.TryGetValue("Expect", out string expectHeader))
            {
                // Remove the expect header for the next run.

                Headers.Remove("Expect");

                int pos = expectHeader.IndexOf(';');

                if (pos != -1)
                    expectHeader = expectHeader.Substring(0, pos).Trim();

                if (!String.Equals("100-continue", expectHeader, StringComparison.OrdinalIgnoreCase))
                    throw new ProtocolException(String.Format("Could not process Expect header '{0}'", expectHeader));

                SendContinueResponse();
                return true;
            }

            return false;
        }

        private bool ProcessContentLengthHeader()
        {
            // Read the content.


            if (Headers.TryGetValue("Content-Length", out string contentLengthHeader))
            {

                if (!int.TryParse(contentLengthHeader, out int contentLength))
                    throw new ProtocolException(String.Format("Could not parse Content-Length header '{0}'", contentLengthHeader));

                string contentType = null;
                string contentTypeExtra = null;

                if (Headers.TryGetValue("Content-Type", out string contentTypeHeader))
                {
                    string[] parts = contentTypeHeader.Split(new[] { ';' }, 2);

                    contentType = parts[0].Trim().ToLowerInvariant();
                    contentTypeExtra = parts.Length == 2 ? parts[1].Trim() : null;
                }

                if (_parser != null)
                {
                    _parser.Dispose();
                    _parser = null;
                }

                switch (contentType)
                {
                    case "application/x-www-form-urlencoded":
                        _parser = new HttpUrlEncodedRequestParser(this, contentLength);
                        break;

                    case "multipart/form-data":
                        string boundary = null;

                        if (contentTypeExtra != null)
                        {
                            string[] parts = contentTypeExtra.Split(new[] { '=' }, 2);

                            if (
                                parts.Length == 2 &&
                                String.Equals(parts[0], "boundary", StringComparison.OrdinalIgnoreCase)
                            )
                                boundary = parts[1];
                        }

                        if (boundary == null)
                            throw new ProtocolException("Expected boundary with multipart content type");

                        _parser = new HttpMultiPartRequestParser(this, contentLength, boundary);
                        break;

                    default:
                        _parser = new HttpUnknownRequestParser(this, contentLength);
                        break;
                }

                // We've made a parser available. Recurs back to start processing
                // with the parser.

                ProcessContent();
                return true;
            }

            return false;
        }

        private void SendContinueResponse()
        {
            var sb = new StringBuilder();

            sb.Append(Protocol);
            sb.Append(" 100 Continue\r\nServer: ");
            sb.Append(Server.ServerBanner);
            sb.Append("\r\nDate: ");
            sb.Append(DateTime.UtcNow.ToString("R"));
            sb.Append("\r\n\r\n");

            var bytes = Encoding.ASCII.GetBytes(sb.ToString());

            if (_writeStream != null)
                _writeStream.Dispose();

            _writeStream = new MemoryStream();
            _writeStream.Write(bytes, 0, bytes.Length);
            _writeStream.Position = 0;

            BeginWrite();
        }

        private void BeginWrite()
        {
            try
            {
                // Copy the next part from the write stream.

                int read = _writeStream.Read(_writeBuffer, 0, _writeBuffer.Length);

                Server.TimeoutManager.WriteQueue.Add(
                    _stream.BeginWrite(_writeBuffer, 0, read, WriteCallback, null),
                    this
                );
            
            }catch(System.IO.IOException ex)
            {
                Logging.WriteEntry(Types.LogType.Verbose, "Netlenium.WebServer", $"The HTTP server wasn't able to write to the stream because the stream was terminated by the client/host machine. Priority=NORMAL");

                Dispose();
            }
            catch (Exception ex)
            {
                Logging.WriteEntry(Types.LogType.Error, "Netlenium.WebServer", $"BeginWrite failed {ex}");

                Dispose();
            }
        }

        private void WriteCallback(IAsyncResult asyncResult)
        {
            if (_disposed)
                return;

            try
            {
                _stream.EndWrite(asyncResult);

                if (_writeStream != null && _writeStream.Length != _writeStream.Position)
                {
                    // Continue writing from the write stream.

                    BeginWrite();
                }
                else
                {
                    if (_writeStream != null)
                    {
                        _writeStream.Dispose();
                        _writeStream = null;
                    }

                    switch (_state)
                    {
                        case ClientState.WritingHeaders:
                            WriteResponseContent();
                            break;

                        case ClientState.WritingContent:
                            ProcessRequestCompleted();
                            break;

                        default:
                            Debug.Assert(_state != ClientState.Closed);

                            if (ReadBuffer.DataAvailable)
                            {
                                try
                                {
                                    ProcessReadBuffer();
                                }
                                catch (Exception ex)
                                {
                                    ProcessException(ex);
                                }
                            }
                            else
                            {
                                BeginRead();
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.WriteEntry(Types.LogType.Information, "Netlenium.WebServer", $"Failed to write {ex}");

                Dispose();
            }
        }

        public void ExecuteRequest()
        {
            _context = new HttpContext(this);

            Logging.WriteVerboseEntry("Netlenium.WebServer", $"Accepted request {_context.Request.RawUrl}");

            Server.RaiseRequest(_context);

            WriteResponseHeaders();
        }

        private void WriteResponseHeaders()
        {
            var headers = BuildResponseHeaders();

            if (_writeStream != null)
                _writeStream.Dispose();

            _writeStream = new MemoryStream(headers);

            _state = ClientState.WritingHeaders;

            BeginWrite();
        }

        private byte[] BuildResponseHeaders()
        {
            var response = _context.Response;
            var sb = new StringBuilder();

            // Write the prolog.

            sb.Append(Protocol);
            sb.Append(' ');
            sb.Append(response.StatusCode);

            if (!String.IsNullOrEmpty(response.StatusDescription))
            {
                sb.Append(' ');
                sb.Append(response.StatusDescription);
            }

            sb.Append("\r\n");

            // Write all headers provided by Response.

            if (!String.IsNullOrEmpty(response.CacheControl))
                WriteHeader(sb, "Cache-Control", response.CacheControl);

            if (!String.IsNullOrEmpty(response.ContentType))
            {
                string contentType = response.ContentType;

                if (!String.IsNullOrEmpty(response.CharSet))
                    contentType += "; charset=" + response.CharSet;

                WriteHeader(sb, "Content-Type", contentType);
            }

            WriteHeader(sb, "Expires", response.ExpiresAbsolute.ToString("R"));

            if (!String.IsNullOrEmpty(response.RedirectLocation))
                WriteHeader(sb, "Location", response.RedirectLocation);

            // Write the remainder of the headers.

            foreach (string key in response.Headers.AllKeys)
            {
                WriteHeader(sb, key, response.Headers[key]);
            }

            // Write the content length (we override custom headers for this).

            WriteHeader(sb, "Content-Length", response.OutputStream.BaseStream.Length.ToString(CultureInfo.InvariantCulture));

            for (int i = 0; i < response.Cookies.Count; i++)
            {
                WriteHeader(sb, "Set-Cookie", response.Cookies[i].GetHeaderValue());
            }

            sb.Append("\r\n");

            return response.HeadersEncoding.GetBytes(sb.ToString());
        }

        private void WriteHeader(StringBuilder sb, string key, string value)
        {
            sb.Append(key);
            sb.Append(": ");
            sb.Append(value);
            sb.Append("\r\n");
        }

        private void WriteResponseContent()
        {
            if (_writeStream != null)
                _writeStream.Dispose();

            _writeStream = _context.Response.OutputStream.BaseStream;
            _writeStream.Position = 0;

            _state = ClientState.WritingContent;

            BeginWrite();
        }

        private void ProcessRequestCompleted()
        {

            // Do not accept new requests when the server is stopping.

            if (
                !_errored &&
                Server.State == HttpServerState.Started &&
                Headers.TryGetValue("Connection", out string connectionHeader) &&
                string.Equals(connectionHeader, "keep-alive", StringComparison.OrdinalIgnoreCase)
            )
                BeginRequest();
            else
                Dispose();
        }

        public void RequestClose()
        {
            if (_state == ClientState.ReadingProlog)
            {
                var stream = _stream;

                if (stream != null)
                    stream.Dispose();
            }
        }

        public void ForceClose()
        {
            var stream = _stream;

            if (stream != null)
                stream.Dispose();
        }

        public void UnsetParser()
        {
            Debug.Assert(_parser != null);

            _parser = null;
        }

        private void ProcessException(Exception exception)
        {
            if (_disposed)
                return;

            _errored = true;

            // If there is no request available, the error didn't occur as part
            // of a request (e.g. the client closed the connection). Just close
            // the channel in that case.

            if (Request == null)
            {
                Dispose();
                return;
            }

            try
            {
                if (_context == null)
                    _context = new HttpContext(this);

                _context.Response.Status = "500 Internal Server Error";

                bool handled;

                try
                {
                    handled = Server.RaiseUnhandledException(_context, exception);
                }
                catch
                {
                    handled = false;
                }

                if (!handled && _context.Response.OutputStream.CanWrite)
                {
                    string resourceName = GetType().Namespace + ".Resources.InternalServerError.html";

                    using (var stream = GetType().Assembly.GetManifestResourceStream(resourceName))
                    {
                        byte[] buffer = new byte[4096];
                        int read;

                        while ((read = stream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            _context.Response.OutputStream.Write(buffer, 0, read);
                        }
                    }
                }

                WriteResponseHeaders();
            }
            catch (Exception ex)
            {
                Logging.WriteEntry(Types.LogType.Information, "Netlenium.WebServer", $"Failed to process internal server error response {ex}");

                Dispose();
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                Server.UnregisterClient(this);

                _state = ClientState.Closed;

                if (_stream != null)
                {
                    _stream.Dispose();
                    _stream = null;
                }

                if (TcpClient != null)
                {
                    TcpClient.Close();
                    TcpClient = null;
                }

                Reset();

                _disposed = true;
            }
        }

        private enum ClientState
        {
            ReadingProlog,
            ReadingHeaders,
            ReadingContent,
            WritingHeaders,
            WritingContent,
            Closed
        }
    }
}
