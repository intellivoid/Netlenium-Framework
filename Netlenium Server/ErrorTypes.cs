using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string MethodNotFound
        {
            get { return "METHOD_NOT_FOUND"; }
        }

        /// <summary>
        /// The request method used is not allowed
        /// </summary>
        public string MethodNotAllowed
        {
            get { return "METHOD_NOT_ALLOWED";  }
        }

        /// <summary>
        /// The given driver is unsupported either by Netlenium or the Netlenium Web Server
        /// </summary>
        public string UnsupportedDriver
        {
            get { return "UNSUPPORTED_DRIVER";  }
        }

        /// <summary>
        /// The given search type is invalid
        /// </summary>
        public string InvalidSearchType
        {
            get { return "INVALID_SEARCH_TYPE";  }
        }

        /// <summary>
        /// Unexpected Internal Server Error
        /// </summary>
        public string InternalServerError
        {
            get { return "INTERNAL_SERVER_ERROR";  }
        }
        
    }
}
