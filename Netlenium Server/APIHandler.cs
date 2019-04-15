using Netlenium.WebServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

            if (SessionManager.SessionExists(httpRequest.Request.QueryString.Get("session_id")) == false)
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
        /// Returns the current active sessions
        /// </summary>
        /// <param name="httpRequest"></param>
        public static void ActiveSessions(HttpRequestEventArgs httpRequest)
        {
            if(SessionManager.activeSessions != null)
            {
                if (SessionManager.activeSessions.Count > 0)
                {
                    List<Object> ActiveSessionsList = new List<object>();

                    foreach (var ActiveSession in SessionManager.activeSessions.Values)
                    {
                        ActiveSessionsList.Add(new
                        {
                            SessionId = ActiveSession.Id,
                            CreationDate = ActiveSession.CreationDate.ToString(),
                            DriverType = ActiveSession.ObjectController.DriverType.ToString(),
                            CurrentUrl = ActiveSession.ObjectController.Url
                        });
                    }

                    APIServer.SendJsonResponse(
                        httpRequest.Response, new
                        {
                            Status = true,
                            ResponseCode = 200,
                            ActiveSessions = ActiveSessionsList
                        }, 200
                     );

                    return;

                }
            }
            
            APIServer.SendJsonResponse(
                httpRequest.Response, new
                {
                    Status = true,
                    ResponseCode = 200,
                    ActiveSessions = new List<Object>()
                }, 200
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
                var Session = SessionManager.CreateSession(APIServer.GetParamerter(httpRequest.Request, "target_driver"));

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
            
        }

        /// <summary>
        /// Closes the current session
        /// </summary>
        /// <param name="httpRequest"></param>
        public static void CloseSession(HttpRequestEventArgs httpRequest)
        {
            if (CheckSession(httpRequest) == false)
            {
                return;
            }

            try
            {
                SessionManager.KillSession(httpRequest.Request.QueryString.Get("session_id"));

                APIServer.SendJsonResponse(
                    httpRequest.Response, new
                    {
                        Status = true,
                        ResponseCode = 200
                    }, 200
                 );

                return;
            }
            catch (Exception exception)
            {
                APIServer.SendJsonInternalServerErrorResponse(httpRequest.Response, "Error while trying to kill session", exception.Message);

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
                SessionManager.GetSession(httpRequest.Request.QueryString.Get("session_id")).ObjectController.Navigate(APIServer.GetParamerter(httpRequest.Request, "url"));

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

            // Determine the search type
            Netlenium.Types.SearchType searchType;
            switch (APIServer.GetParamerter(httpRequest.Request, "search_type").ToUpper())
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

            /// Determine the target
            string Target;
            if (APIServer.GetParamerter(httpRequest.Request, "target") == null)
            {
                Target = "DOCUMENT";
            }
            else
            {
                Target = APIServer.GetParamerter(httpRequest.Request, "target");
            }

            try
            {
                List<Netlenium.Driver.WebElement> Elements;

                switch (Target.ToUpper())
                {
                    case "DOCUMENT":
                        Elements = SessionManager.GetSession(httpRequest.Request.QueryString.Get("session_id")).
                            ObjectController.GetElements(searchType, APIServer.GetParamerter(httpRequest.Request, "value"));
                        break;

                    case "ELEMENT_SCOPE":
                        if (SessionManager.GetSession(httpRequest.Request.QueryString.Get("session_id")).ElementScope == null)
                        {
                            APIServer.SendJsonErrorResponse(
                                httpRequest.Response, ErrorTypes.TargetNotFound,
                                "The Element scope is not currently set or it does not exist", 400
                            );

                            return;
                        }

                        Elements = SessionManager.GetSession(httpRequest.Request.QueryString.Get("session_id")).
                            ElementScope.GetElements(searchType, APIServer.GetParamerter(httpRequest.Request, "value"));
                        break;

                    default:
                        APIServer.SendJsonErrorResponse(
                                httpRequest.Response, ErrorTypes.InvalidTargetType,
                                "The given target is not valid", 400
                            );

                        return;
                }

                if (APIServer.GetParamerter(httpRequest.Request, "index") == null)
                {
                    SessionManager.GetSession(httpRequest.Request.QueryString.Get("session_id")).ElementScope = Elements[0];
                }
                else
                {
                    SessionManager.GetSession(httpRequest.Request.QueryString.Get("session_id")).
                        ElementScope = Elements[Int32.Parse(APIServer.GetParamerter(httpRequest.Request, "index"))];
                }

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
            catch (Netlenium.Driver.MethodNotSupportedForDriver)
            {
                APIServer.SendJsonErrorResponse(
                    httpRequest.Response, ErrorTypes.UnsupportedMethod,
                    "The given method is not supported for the requested driver", 400
                );

                return;
            }
        }

        /// <summary>
        /// Sends keys to the current element scope
        /// </summary>
        /// <param name="httpRequest"></param>
        public static void SendKeys(HttpRequestEventArgs httpRequest)
        {
            if(CheckSession(httpRequest) == false)
            {
                return;
            }

            if(APIServer.GetParamerter(httpRequest.Request, "value") == null)
            {
                APIServer.SendJsonMissingParamerterResponse(httpRequest.Response, "value");
                return;
            }

            if(SessionManager.GetSession(httpRequest.Request.QueryString.Get("session_id")).ElementScope == null)
            {
                APIServer.SendJsonErrorResponse(httpRequest.Response, ErrorTypes.ElementScopeNotSet, "The element scope was not set", 400);
                return;
            }

            try
            {
                SessionManager.GetSession(httpRequest.Request.QueryString.Get("session_id")).ElementScope.SendKeys(APIServer.GetParamerter(httpRequest.Request, "value"));
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
            catch(Exception exception)
            {
                APIServer.SendJsonErrorResponse(httpRequest.Response, ErrorTypes.ElementInteractionError, exception.Message, 500);
                return;
            }
        }

        /// <summary>
        /// Gets the attribute of the element
        /// </summary>
        /// <param name="httpRequest"></param>
        public static void GetAttribute(HttpRequestEventArgs httpRequest)
        {
            if (CheckSession(httpRequest) == false)
            {
                return;
            }

            if (APIServer.GetParamerter(httpRequest.Request, "attribute_name") == null)
            {
                APIServer.SendJsonMissingParamerterResponse(httpRequest.Response, "attribute_name");
                return;
            }
            
            if (SessionManager.GetSession(httpRequest.Request.QueryString.Get("session_id")).ElementScope == null)
            {
                APIServer.SendJsonErrorResponse(httpRequest.Response, ErrorTypes.ElementScopeNotSet, "The element scope was not set", 400);
                return;
            }

            try
            {
                var AttributeValueCallback = SessionManager.GetSession(httpRequest.Request.QueryString.Get("session_id")).ElementScope.GetAttribute(APIServer.GetParamerter(httpRequest.Request, "attribute_name"));

                httpRequest.Response.StatusCode = 200;
                httpRequest.Response.Headers.Add("content-Type", "application/json");

                var Response = new
                {
                    Status = true,
                    ResponseCode = httpRequest.Response.StatusCode,
                    AttributeValue = AttributeValueCallback
                };

                APIServer.SendResponse(httpRequest.Response, JsonConvert.SerializeObject(Response));
                return;
            }
            catch (Exception exception)
            {
                APIServer.SendJsonErrorResponse(httpRequest.Response, ErrorTypes.ElementInteractionError, exception.Message, 500);
                return;
            }
        }

        /// <summary>
        /// Sets the attribute to the element
        /// </summary>
        /// <param name="httpRequest"></param>
        public static void SetAttribute(HttpRequestEventArgs httpRequest)
        {
            if (CheckSession(httpRequest) == false)
            {
                return;
            }

            if (APIServer.GetParamerter(httpRequest.Request, "attribute_name") == null)
            {
                APIServer.SendJsonMissingParamerterResponse(httpRequest.Response, "attribute_name");
                return;
            }

            if (APIServer.GetParamerter(httpRequest.Request, "value") == null)
            {
                APIServer.SendJsonMissingParamerterResponse(httpRequest.Response, "attribute_name");
                return;
            }

            if (SessionManager.GetSession(httpRequest.Request.QueryString.Get("session_id")).ElementScope == null)
            {
                APIServer.SendJsonErrorResponse(httpRequest.Response, ErrorTypes.ElementScopeNotSet, "The element scope was not set", 400);
                return;
            }

            try
            {
                SessionManager.GetSession(httpRequest.Request.QueryString.Get("session_id")).
                    ElementScope.SetAttribute(APIServer.GetParamerter(httpRequest.Request, "attribute_name"), APIServer.GetParamerter(httpRequest.Request, "value"));

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
            catch (Exception exception)
            {
                APIServer.SendJsonErrorResponse(httpRequest.Response, ErrorTypes.ElementInteractionError, exception.Message, 500);
                return;
            }
        }
    }
}
