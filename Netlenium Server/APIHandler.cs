using Netlenium.WebServer;
using Newtonsoft.Json;
using System;

namespace Netlenium_Server
{
    /// <summary>
    /// Handles API Requests and processes a proper reponse
    /// </summary>
    public class APIHandler
    {
        /// <summary>
        /// Checks the session
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        private static bool CheckSession(HttpRequestEventArgs httpRequest)
        {
            if (httpRequest.Request.QueryString.Get("session_id") == null)
            {
                APIServer.SendJsonMissingParamerterResponse(httpRequest.Response, "session_id");
                return false;
            }

            if (Sessions.SessionExists(httpRequest.Request.QueryString.Get("session_id")) == false)
            {
                APIServer.SendJsonErrorResponse(
                    httpRequest.Response,
                    ErrorTypes.UnauthorizedSession,
                    "The given session was not found or you don't have access to it",
                    403
                );
                return false;
            }

            return true;
        }

        /// <summary>
        /// The root request
        /// </summary>
        /// <param name="httpRequest"></param>
        public static void Root(HttpRequestEventArgs httpRequest)
        {
            APIServer.SendJsonResponse(
                httpRequest.Response, new
                {
                    Status = true,
                    ResponseCode = 200,
                    ServerName = "Netlenium Framework Server",
                    ServerVersion = Program.Version
                }, 200
             );
        }

        /// <summary>
        /// Returns when a requested method was not found or is unsupported
        /// </summary>
        /// <param name="httpRequest"></param>
        public static void NotFound(HttpRequestEventArgs httpRequest)
        {
            APIServer.SendJsonErrorResponse(
                httpRequest.Response, ErrorTypes.MethodNotFound,
                "The requested method to the server was not found or is unsupported", 404
            );

            return;
        }

        /// <summary>
        /// Unsupported Request Method Error
        /// </summary>
        /// <param name="httpRequest"></param>
        public static void UnsupportedRequestMethod(HttpRequestEventArgs httpRequest)
        {
            APIServer.SendJsonErrorResponse(
                httpRequest.Response, ErrorTypes.MethodNotFound,
                "Only POST/GET requests are allowed", 405
            );

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
                APIServer.SendJsonMissingParamerterResponse(httpRequest.Response, "target_driver");
                return;
            }
            
            try
            {
                var Session = Sessions.CreateSession(httpRequest.Request.QueryString.Get("target_driver"));

                APIServer.SendJsonResponse(
                    httpRequest.Response, new
                    {
                        Status = true,
                        ResponseCode = 200,
                        SessionId = Session.Id
                    }, 200
                 );

                return;
            }
            catch(UnsupportedDriverException)
            {
                APIServer.SendJsonErrorResponse(httpRequest.Response, ErrorTypes.UnsupportedDriver,
                    "The given target driver is not supported", 401
                );
                
                return;
            }
            catch (Exception exception)
            {
                APIServer.SendJsonInternalServerErrorResponse(httpRequest.Response, "Error while trying to construct driver", exception.Message);

                return;
            }

        }

        /// <summary>
        /// Navigates to the requested page
        /// </summary>
        /// <param name="httpRequest"></param>
        public static void Navigate(HttpRequestEventArgs httpRequest)
        {

            if(CheckSession(httpRequest) == false)
            {
                return;
            }

            if(APIServer.GetParamerter(httpRequest.Request, "url") == null)
            {
                APIServer.SendJsonMissingParamerterResponse(httpRequest.Response, "url");
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
                APIServer.SendJsonErrorResponse(
                    httpRequest.Response, ErrorTypes.UnsupportedMethod,
                    "The given method is not supported for the requested driver", 400
                );
                
                return;
            }
            catch(Exception exception)
            {
                APIServer.SendJsonInternalServerErrorResponse(
                    httpRequest.Response, "There was an error while trying to execute the given method", exception.Message
                );
                return;
            }

        }

        /// <summary>
        /// Gets the elements from the webpage
        /// </summary>
        /// <param name="httpRequest"></param>
        public static void SetElementScope(HttpRequestEventArgs httpRequest)
        {
            if (CheckSession(httpRequest) == false)
            {
                return;
            }

            if (APIServer.GetParamerter(httpRequest.Request, "search_type") == null)
            {
                APIServer.SendJsonMissingParamerterResponse(httpRequest.Response, "search_type");
                return;
            }

            if (APIServer.GetParamerter(httpRequest.Request, "value") == null)
            {
                APIServer.SendJsonMissingParamerterResponse(httpRequest.Response, "value");
                return;
            }

            if (APIServer.GetParamerter(httpRequest.Request, "scope") == null)
            {
                APIServer.SendJsonMissingParamerterResponse(httpRequest.Response, "scope");
                return;
            }

            Netlenium.Types.SearchType searchType;

            switch(APIServer.GetParamerter(httpRequest.Request, "search_type").ToUpper())
            {
                case "ID":
                    searchType = Netlenium.Types.SearchType.Id;
                    break;

                case "CLASSNAME":
                    searchType = Netlenium.Types.SearchType.ClassName;
                    break;

                case "CSSSELECTOR":
                    searchType = Netlenium.Types.SearchType.CssSelector;
                    break;

                case "TAGNAME":
                    searchType = Netlenium.Types.SearchType.TagName;
                    break;

                case "NAME":
                    searchType = Netlenium.Types.SearchType.Name;
                    break;

                default:
                    APIServer.SendJsonErrorResponse(
                        httpRequest.Response, ErrorTypes.InvalidSearchType,
                        "The given search type is invalid", 400
                    );
                    return;
            }

            try
            {
                var Elements = Sessions.GetSession(httpRequest.Request.QueryString.Get("session_id")).ObjectController.GetElements(searchType, APIServer.GetParamerter(httpRequest.Request, "value"));

                //if(SelectedIndex != null)
                //{

                //}

                httpRequest.Response.StatusCode = 200;
                httpRequest.Response.Headers.Add("content-Type", "application/json");

                var Response = new
                {
                    Status = true,
                    ResponseCode = httpRequest.Response.StatusCode
                };

                APIServer.SendResponse(httpRequest.Response, JsonConvert.SerializeObject(Elements));
                return;
            }
            catch (Netlenium.Driver.MethodNotSupportedForDriver)
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
        }

    }
}
