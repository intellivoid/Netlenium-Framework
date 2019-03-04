using System;
using System.Runtime.Serialization;

namespace Netlenium.Driver
{
    [Serializable]
    internal class PropertyNotAvailableForSelectedDriver : Exception
    {
        public PropertyNotAvailableForSelectedDriver()
        {
        }

        public PropertyNotAvailableForSelectedDriver(string message) : base(message)
        {
        }

        public PropertyNotAvailableForSelectedDriver(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PropertyNotAvailableForSelectedDriver(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}