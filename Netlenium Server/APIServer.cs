using Netlenium.WebServer;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

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

        public static Thread StartThread(string domain = "localhost", int port = 0)
        {
            Thread HTTPThread = new Thread(new ParameterizedThreadStart(StartWSthread));
            string[] HTTPParams = new string[2];
            HTTPParams[0] = domain;
            HTTPParams[1] = port.ToString();
            HTTPThread.Start(HTTPParams);
            return HTTPThread;
        }

        private static void StartWSthread(object parameters)
        {
            string[] Params = (string[])parameters;
            string domain = Params[0];
            int port = Convert.ToInt32(Params[1]);
            Server = new HttpServer();

            if (port != 0)
            {
                Server.EndPoint = new IPEndPoint(IPAddress.Any, port);
            }

            Server.RequestReceived += (s, e) => { RequestReceived(s, e); };
            Server.Start();
            Program.HttpController["Started"] = true;
            Program.HttpController["Status"] = true;
            Task.Run(async () =>
            {
                while (true)
                {
                    
                    if((bool)Program.HttpController["ShutdownSignal"])
                    {
                        Program.HttpController["ShutdownSignal"] = false;
                        Server.Stop();
                        Program.HttpController["Started"] = false;
                        Program.HttpController["Status"] = false;
                        break;

                    } else
                    {
                        // don't run again for at least 200 milliseconds
                        await Task.Delay(200);
                    }

                    
                }
            });

        }
        /// <summary>
        /// Stops the server
        /// </summary>
        public static void StopThread()
        {
            Program.HttpController["ShutdownSignal"] = true;
            Task.Run(async () =>
            {
                while (true)
                {
                    
                    if (!(bool)Program.HttpController["Status"])
                    {
                        Program.HTTPServer.Abort();
                        break;
                    }
                    else
                    {
                        // don't run again for at least 200 milliseconds
                        await Task.Delay(200);
                    }

                }
            });
            SessionManager.CloseAllSessions();
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
        /// Sends a JSON Response back to the client
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <param name="content"></param>
        /// <param name="statusCode"></param>
        public static void SendJsonResponse(HttpResponse httpResponse, object content, int statusCode = 200)
        {
            httpResponse.StatusCode = statusCode;
            httpResponse.Headers.Add("content-Type", "application/json");

            SendResponse(httpResponse, JsonConvert.SerializeObject(content, Formatting.Indented));
        }

        /// <summary>
        /// Sends a JSON Response back to the client saying that the paramerter is missing
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <param name="parameterName"></param>
        public static void SendJsonMissingParamerterResponse(HttpResponse httpResponse, string parameterName)
        {
            SendJsonResponse(
                httpResponse, new
                {
                    Status = false,
                    ResponseCode = 401,
                    ErrorType = ErrorTypes.MissingParamerter,
                    Message = $"Missing paramerter \"{parameterName}\""
                }, 401
            );
        }

        /// <summary>
        /// Sends a JSON Response back to the client explaining the internal server error
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <param name="message"></param>
        /// <param name="exceptionMessage"></param>
        public static void SendJsonInternalServerErrorResponse(HttpResponse httpResponse, string message, string exceptionMessage)
        {
            SendJsonResponse(
                httpResponse, new
                {
                    Status = false,
                    ResponseCode = 500,
                    ErrorType = ErrorTypes.InternalServerError,
                    Message = message,
                    Exception = exceptionMessage
                }, 500
            );
        }

        /// <summary>
        /// Returns a error response as a JSON Payload
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <param name="errorType"></param>
        /// <param name="message"></param>
        /// <param name="responseCode"></param>
        public static void SendJsonErrorResponse(HttpResponse httpResponse, string errorType, string message, int responseCode)
        {
            SendJsonResponse(
                httpResponse, new
                {
                    Status = false,
                    ResponseCode = responseCode,
                    ErrorType = errorType,
                    Message = message
                }, responseCode
            );
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

                case "/rc/active_sessions":
                    APIHandler.ActiveSessions(httpRequest);
                    break;

                case "/create_session":
                    APIHandler.CreateSession(httpRequest);
                    break;

                case "/close_session":
                    APIHandler.CloseSession(httpRequest);
                     break;

                case "/navigate":
                    APIHandler.Navigate(httpRequest);
                    break;

                case "/set_element_scope":
                    APIHandler.SetElementScope(httpRequest);
                    break;

                case "/send_keys":
                    APIHandler.SendKeys(httpRequest);
                    break;

                case "/get_attribute":
                    APIHandler.GetAttribute(httpRequest);
                    break;

                case "/set_attribute":
                    APIHandler.SetAttribute(httpRequest);
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
