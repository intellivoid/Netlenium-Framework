using Netlenium.WebServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;

namespace Netlenium_Server
{
    public class APIServer
    {
        /// <summary>
        /// Private Server Object
        /// </summary>
        private static HttpServer Server;

        /// <summary>
        /// The directory of the executing 
        /// </summary>
        private static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(File.Exists($"{Path.GetDirectoryName(path)}{Path.DirectorySeparatorChar}Netlenium.dll") ? path : Process.GetCurrentProcess().MainModule.FileName);
            }
        }

        /// <summary>
        /// Starts the Web Service on a random port, if port is set to another value other than 0
        /// it will use that port instead of a random port
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static string Start(int port = 0)
        {
            Server = new HttpServer();

            if (port != 0)
            {
                Server.EndPoint = new IPEndPoint(IPAddress.Loopback, port);
            }

            Server.RequestReceived += (s, e) => { RequestReceived(s, e); };
            Server.Start();

            return $"http://{Server.EndPoint}/";
        }

        /// <summary>
        /// Stops the server
        /// </summary>
        public static void Stop()
        {
            Server.Stop();
            Sessions.CloseAllSessions();
        }

        /// <summary>
        /// Sends a response back to the client
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <param name="content"></param>
        public static void SendResponse(HttpResponse httpResponse, string content)
        {
            httpResponse.Headers.Add("X-Powered-By", "Netlenium Framework");
            using (var writer = new StreamWriter(httpResponse.OutputStream))
            {
                writer.Write(content);
            }
        }

        /// <summary>
        /// Sends a response back to the client as a file
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <param name="filepath"></param>
        public static void SendFile(HttpResponse httpResponse, string filepath)
        {
            httpResponse.Headers.Add("X-Powered-By", "Netlenium Framework");

            using (var Stream = File.OpenRead(filepath))
            {
                Stream.CopyTo(httpResponse.OutputStream);
            }
        }

        /// <summary>
        /// Raised when a request is received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="httpRequest"></param>
        public static void RequestReceived(object sender, HttpRequestEventArgs httpRequest)
        {
            if (httpRequest.Request.RequestType.ToUpper() != "GET" && httpRequest.Request.RequestType.ToUpper() != "POST")
            {
                APIHandler.UnsupportedRequestMethod(httpRequest);
                return;
            }
            
            switch (httpRequest.Request.Path.ToLower())
            {
                case "/":
                    APIHandler.Root(httpRequest);
                    break;

                case "/favicon.ico":
                    var FaviconLocation = $"{AssemblyDirectory}{Path.DirectorySeparatorChar}WebResources{Path.DirectorySeparatorChar}favicon.ico";
                    if (File.Exists(FaviconLocation))
                    {
                        httpRequest.Response.Headers.Add("Content-Type", "image/ico");
                        SendFile(httpRequest.Response, FaviconLocation);
                    }
                    break;

                case "/create_session":
                    APIHandler.CreateSession(httpRequest);
                    break;

                default:
                    APIHandler.NotFound(httpRequest);
                    break;
            }
        }

        /// <summary>
        /// Gets the paramerter either from a GET or POST request
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string GetParamerter(HttpRequest httpRequest, string parameter)
        {
            switch(httpRequest.RequestType.ToUpper())
            {
                case "GET":
                    return httpRequest.QueryString.Get(parameter);

                case "POST":
                    return httpRequest.Form.Get(parameter);

                default:
                    throw new UnsupportedRequestMethodException();
            }
        }
    }
}
