using System;
using System.Runtime.Serialization;

namespace Netlenium.Driver
{
    [Serializable]
    public class ElementTypeNotSupportedException : Exception
    {
        public ElementTypeNotSupportedException()
        {
        }

        public ElementTypeNotSupportedException(string message) : base(message)
        {
        }

        public ElementTypeNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ElementTypeNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}