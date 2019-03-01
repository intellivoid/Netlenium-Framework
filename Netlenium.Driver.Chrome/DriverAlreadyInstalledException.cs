using System;
using System.Runtime.Serialization;

namespace Netlenium.Driver.Chrome
{
    [Serializable]
    internal class DriverAlreadyInstalledException : Exception
    {
        public DriverAlreadyInstalledException()
        {
        }

        public DriverAlreadyInstalledException(string message) : base(message)
        {
        }

        public DriverAlreadyInstalledException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DriverAlreadyInstalledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}