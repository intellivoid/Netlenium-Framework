using System;
using System.Runtime.Serialization;

namespace Netlenium.Driver.Chrome
{
    [Serializable]
    internal class DriverAlreadyUpToDate : Exception
    {
        public DriverAlreadyUpToDate()
        {
        }

        public DriverAlreadyUpToDate(string message) : base(message)
        {
        }

        public DriverAlreadyUpToDate(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DriverAlreadyUpToDate(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}