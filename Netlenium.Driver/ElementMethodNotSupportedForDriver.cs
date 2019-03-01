using System;
using System.Runtime.Serialization;

namespace Netlenium.Driver
{
    [Serializable]
    internal class ElementMethodNotSupportedForDriver : Exception
    {
        public ElementMethodNotSupportedForDriver()
        {
        }

        public ElementMethodNotSupportedForDriver(string message) : base(message)
        {
        }

        public ElementMethodNotSupportedForDriver(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ElementMethodNotSupportedForDriver(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}