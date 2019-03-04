using System;
using System.Runtime.Serialization;

namespace Netlenium.Driver
{
    [Serializable]
    internal class InvokeFailureException : Exception
    {
        public InvokeFailureException()
        {
        }

        public InvokeFailureException(string message) : base(message)
        {
        }

        public InvokeFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvokeFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}