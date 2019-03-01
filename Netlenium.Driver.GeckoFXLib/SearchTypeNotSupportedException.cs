using System;
using System.Runtime.Serialization;

namespace Netlenium.Driver.GeckoFXLib
{
    [Serializable]
    internal class SearchTypeNotSupportedException : Exception
    {
        public SearchTypeNotSupportedException()
        {
        }

        public SearchTypeNotSupportedException(string message) : base(message)
        {
        }

        public SearchTypeNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SearchTypeNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}