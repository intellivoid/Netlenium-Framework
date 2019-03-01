using System;
using System.Runtime.Serialization;

namespace Netlenium.Driver
{
    [Serializable]
    internal class ElementPropertyNotSupportedForDriver : Exception
    {
        public ElementPropertyNotSupportedForDriver()
        {
        }

        public ElementPropertyNotSupportedForDriver(string message) : base(message)
        {
        }

        public ElementPropertyNotSupportedForDriver(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ElementPropertyNotSupportedForDriver(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}