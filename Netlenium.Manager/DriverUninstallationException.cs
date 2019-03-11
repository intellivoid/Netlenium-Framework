using System;
using System.Runtime.Serialization;

namespace Netlenium.Manager
{
    [Serializable]
    internal class DriverUninstallationException : Exception
    {
        public DriverUninstallationException()
        {
        }

        public DriverUninstallationException(string message) : base(message)
        {
        }

        public DriverUninstallationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DriverUninstallationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}