using System;
using Netlenium.WebServer;
using Newtonsoft.Json;

namespace Netlenium_Server
{
    public class APIHandler
    {
        /// <summary>
        /// The root request
        /// </summary>
        /// <param name="httpRequest"></param>
        public static void Root(HttpRequestEventArgs httpRequest)
        {
            httpRequest.Response.StatusCode = 200;
            httpRequest.Response.Headers.Add("content-Type", "application/json");

            var Response = new
            {
                Status = true,
                ResponseCode = httpRequest.Response.StatusCode,
                ServerName = "Netlenium Framework Server",
                ServerVersion = Program.Version
            };

            APIServer.SendResponse(httpRequest.Response, JsonConvert.SerializeObject(Response));
        }

        /// <summary>
        /// Returns when a requested method was not found or is unsupported
        /// </summary>
        /// <param name="httpRequest"></param>
        public static void NotFound(HttpRequestEventArgs httpRequest)
        {
            httpRequest.Response.StatusCode = 404;
            httpRequest.Response.Headers.Add("content-Type", "application/json");

            var Response = new
            {
                Status = false,
                ResponseCode = httpRequest.Response.StatusCode,
                ErrorType = "METHOD_NOT_FOUND",
                Message = "The requested method to the server was not found or is unsupported"
            };

            APIServer.SendResponse(httpRequest.Response, JsonConvert.SerializeObject(Response));
        }

        /// <summary>
        /// Unsupported Request Method Error
        /// </summary>
        /// <param name="httpRequest"></param>
        public static void UnsupportedRequestMethod(HttpRequestEventArgs httpRequest)
        {
            httpRequest.Response.StatusCode = 405;
            httpRequest.Response.Headers.Add("content-Type", "application/json");

            var Response = new
            {
                Status = false,
                ResponseCode = httpRequest.Response.StatusCode,
                ErrorType = "METHOD_NOT_ALLOWED",
                Message = "only POST/GET request methods are allowed"
            };

            APIServer.SendResponse(httpRequest.Response, JsonConvert.SerializeObject(Response));
            return;
        }

        /// <summary>
        /// Creates a new Session in memory
        /// </summary>
        /// <param name="httpRequest"></param>
        public static void CreateSession(HttpRequestEventArgs httpRequest)
        {
            /// Check if paramerter is set
            if(APIServer.GetParamerter(httpRequest.Request, "target_driver") == null)
            {
                httpRequest.Response.StatusCode = 401;
                httpRequest.Response.Headers.Add("content-Type", "application/json");

                var Response = new
                {
                    Status = false,
                    ResponseCode = httpRequest.Response.StatusCode,
                    ErrorType = "MISSING_PARAMERTER",
                    Message = "Missing paramerter 'target_driver'"
                };

                APIServer.SendResponse(httpRequest.Response, JsonConvert.SerializeObject(Response));
                return;
            }


            try
            {
                var Session = Sessions.CreateSession(httpRequest.Request.QueryString.Get("target_driver"));

                httpRequest.Response.StatusCode = 200;
                httpRequest.Response.Headers.Add("content-Type", "application/json");

                var Response = new
                {
                    Status = true,
                    ResponseCode = httpRequest.Response.StatusCode,
                    SessionId = Session.Id
                };

                APIServer.SendResponse(httpRequest.Response, JsonConvert.SerializeObject(Response));
                return;
            }
            catch(UnsupportedDriverException)
            {
                httpRequest.Response.StatusCode = 401;
                httpRequest.Response.Headers.Add("content-Type", "application/json");

                var Response = new
                {
                    Status = false,
                    ResponseCode = httpRequest.Response.StatusCode,
                    ErrorType = "UNSUPPORTED_DRIVER",
                    Message = "The given target driver is not supported"
                };

                APIServer.SendResponse(httpRequest.Response, JsonConvert.SerializeObject(Response));
                return;
            }
            catch (Exception exception)
            {
                httpRequest.Response.StatusCode = 500;
                httpRequest.Response.Headers.Add("content-Type", "application/json");

                var Response = new
                {
                    Status = false,
                    ResponseCode = httpRequest.Response.StatusCode,
                    ErrorType = "INTERNAL_SERVER_ERROR",
                    Message = exception.Message
                };

                APIServer.SendResponse(httpRequest.Response, JsonConvert.SerializeObject(Response));
                return;
            }

        }

        /// <summary>
        /// Navigates to the requested page
        /// </summary>
        /// <param name="httpRequest"></param>
        public static void Navigate(HttpRequestEventArgs httpRequest)
        {

            if(httpRequest.Request.QueryString.Get("session_id") == null)
            {
                httpRequest.Response.StatusCode = 400;
                httpRequest.Response.Headers.Add("content-Type", "application/json");

                var ErrorResponse = new
                {
                    Status = false,
                    ResponseCode = httpRequest.Response.StatusCode,
                    ErrorType = "MISSING_PARAMERTER",
                    Message = "Missing parameter 'session_id'"
                };

                APIServer.SendResponse(httpRequest.Response, JsonConvert.SerializeObject(ErrorResponse));
                return;
            }

            if(Sessions.SessionExists(httpRequest.Request.QueryString.Get("session_id")) == false)
            {
                httpRequest.Response.StatusCode = 403;
                httpRequest.Response.Headers.Add("content-Type", "application/json");

                var ErrorResponse = new
                {
                    Status = false,
                    ResponseCode = httpRequest.Response.StatusCode,
                    ErrorType = "UNAUTHORIZED_SESSION",
                    Message = "The given session was not found or you don't have access to it"
                };

                APIServer.SendResponse(httpRequest.Response, JsonConvert.SerializeObject(ErrorResponse));
                return;
            }

            if(APIServer.GetParamerter(httpRequest.Request, "url") == null)
            {
                httpRequest.Response.StatusCode = 400;
                httpRequest.Response.Headers.Add("content-Type", "application/json");

                var ErrorResponse = new
                {
                    Status = false,
                    ResponseCode = httpRequest.Response.StatusCode,
                    ErrorType = "MISSING_PARAMERTER",
                    Message = "Missing parameter 'url'"
                };

                APIServer.SendResponse(httpRequest.Response, JsonConvert.SerializeObject(ErrorResponse));
                return;
            }

            try
            {
                Sessions.GetSession(httpRequest.Request.QueryString.Get("session_id")).ObjectController.Navigate(APIServer.GetParamerter(httpRequest.Request, "url"));

                httpRequest.Response.StatusCode = 200;
                httpRequest.Response.Headers.Add("content-Type", "application/json");

                var Response = new
                {
                    Status = true,
                    ResponseCode = httpRequest.Response.StatusCode
                };

                APIServer.SendResponse(httpRequest.Response, JsonConvert.SerializeObject(Response));
                return;
            }
            catch(Netlenium.Driver.MethodNotSupportedForDriver)
            {
                httpRequest.Response.StatusCode = 400;
                httpRequest.Response.Headers.Add("content-Type", "application/json");

                var ErrorResponse = new
                {
                    Status = false,
                    ResponseCode = httpRequest.Response.StatusCode,
                    ErrorType = "UNSUPPORTED_METHOD",
                    Message = "The given method is not supported for the requested driver"
                };

                APIServer.SendResponse(httpRequest.Response, JsonConvert.SerializeObject(ErrorResponse));
                return;
            }
            catch(Exception exception)
            {
                httpRequest.Response.StatusCode = 500;
                httpRequest.Response.Headers.Add("content-Type", "application/json");

                var ErrorResponse = new
                {
                    Status = false,
                    ResponseCode = httpRequest.Response.StatusCode,
                    ErrorType = "INTERNAL_SERVER_ERROR",
                    Message = exception.Message
                };

                APIServer.SendResponse(httpRequest.Response, JsonConvert.SerializeObject(ErrorResponse));
                return;
            }

        }

    }
}
