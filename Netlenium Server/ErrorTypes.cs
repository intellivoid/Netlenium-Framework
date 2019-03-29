namespace Netlenium_Server
{
    /// <summary>
    /// Error Types returned by the server to simplify the exception
    /// </summary>
    public class ErrorTypes
    {
        /// <summary>
        /// The request is missing a required parameter
        /// </summary>
        public static string MissingParamerter
        {
            get { return "MISSING_PARAMETER"; }
        }

        /// <summary>
        /// The session the client is trying to access is unavailable or the client does not have access to it
        /// </summary>
        public static string UnauthorizedSession
        {
            get { return "UNAUTHORIZED_SESSION";  }
        }

        /// <summary>
        /// The given method was not found
        /// </summary>
        public static string MethodNotFound
        {
            get { return "METHOD_NOT_FOUND"; }
        }

        /// <summary>
        /// The request method used is not allowed
        /// </summary>
        public static string MethodNotAllowed
        {
            get { return "METHOD_NOT_ALLOWED";  }
        }

        /// <summary>
        /// The given driver is unsupported either by Netlenium or the Netlenium Web Server
        /// </summary>
        public static string UnsupportedDriver
        {
            get { return "UNSUPPORTED_DRIVER";  }
        }

        /// <summary>
        /// The given method is unsupported for the selected driver
        /// </summary>
        public static string UnsupportedMethod
        {
            get { return "UNSUPPORTED_METHOD";  }
        }

        /// <summary>
        /// The given search type is invalid
        /// </summary>
        public static string InvalidSearchType
        {
            get { return "INVALID_SEARCH_TYPE";  }
        }

        /// <summary>
        /// The given target to search in is invalid
        /// </summary>
        public static string InvalidTargetType
        {
            get { return "INVALID_TARGET_TYPE";  }
        }

        /// <summary>
        /// The given target was not found
        /// </summary>
        public static string TargetNotFound
        {
            get { return "TARGET_NOT_FOUND";  }
        }

        /// <summary>
        /// The element scope was not set
        /// </summary>
        public static string ElementScopeNotSet
        {
            get { return "ELEMENT_SCOPE_NOT_SET";  }
        }

        /// <summary>
        /// There was an error while trying to interact with the element
        /// </summary>
        public static string ElementInteractionError
        {
            get { return "ELEMENT_INTERACTION_ERROR";  }
        }

        /// <summary>
        /// Unexpected Internal Server Error
        /// </summary>
        public static string InternalServerError
        {
            get { return "INTERNAL_SERVER_ERROR";  }
        }
        
    }
}
