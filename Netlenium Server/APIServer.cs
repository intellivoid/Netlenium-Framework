using Netlenium.WebServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Netlenium_Server
{
    public class APIServer
    {
        /// <summary>
        /// Private Server Object
        /// </summary>
        private static HttpServer Server;
        
        /// <summary>
        /// Starts the Web Service on a random port, if port is set to another value other than 0
        /// it will use that port instead of a random port
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static string Start(int port = 0)
        {
            Server = new HttpServer();

            if(port != 0)
            {
                Server.EndPoint = new IPEndPoint(IPAddress.Loopback, port);
            }

            Server.RequestReceived += (s, e) => { RequestReceived(s, e); };
            Server.Start();

            return $"http://{Server.EndPoint}/";
        }

        /// <summary>
        /// Sends a response back tot he client
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
        /// Raised when a request is received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="httpRequest"></param>
        public static void RequestReceived(object sender, HttpRequestEventArgs httpRequest)
        {
            switch (httpRequest.Request.Path.ToLower())
            {
                case "/":
                    SendResponse(httpRequest.Response, "test");
                    break;

                case "/create_session":
                    APIHandler.CreateSession(httpRequest);
                    break;
            }
        }
        

    }
}
