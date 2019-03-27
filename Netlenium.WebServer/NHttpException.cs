using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Netlenium.WebServer
{
    [Serializable]
    public class Netlenium.WebServerException : Exception
    {
        public Netlenium.WebServerException()
        {
        }

        public Netlenium.WebServerException(string message)
            : base(message)
        {
        }

        public Netlenium.WebServerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected Netlenium.WebServerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
