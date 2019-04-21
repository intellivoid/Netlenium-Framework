using System;
using System.Runtime.Serialization;

namespace Netlenium.Driver
{
    [Serializable]
    public class DriverManagerException : Exception
    {
        public DriverManagerException()
        {
        }

        public DriverManagerException(string message) : base(message)
        {
        }

        public DriverManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DriverManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}