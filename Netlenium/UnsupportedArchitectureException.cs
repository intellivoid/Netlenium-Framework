using System;
using System.Runtime.Serialization;

namespace Netlenium
{
    [Serializable]
    internal class UnsupportedArchitectureException : Exception
    {
        public UnsupportedArchitectureException()
        {
        }

        public UnsupportedArchitectureException(string message) : base(message)
        {
        }

        public UnsupportedArchitectureException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnsupportedArchitectureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}