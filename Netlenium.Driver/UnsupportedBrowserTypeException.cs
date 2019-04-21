using System;
using System.Runtime.Serialization;

namespace Netlenium.Driver
{
    [Serializable]
    internal class UnsupportedBrowserTypeException : Exception
    {
        public UnsupportedBrowserTypeException()
        {
        }

        public UnsupportedBrowserTypeException(string message) : base(message)
        {
        }

        public UnsupportedBrowserTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnsupportedBrowserTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}