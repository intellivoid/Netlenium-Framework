using System;
using System.Runtime.Serialization;

namespace Netlenium.Manager
{
    /// <summary>
    /// Raises when there was an error during the driver uninstallation process
    /// </summary>
    [Serializable]
    internal class DriverUninstallationException : Exception
    {
        /// <summary>
        /// Public Constructor without context
        /// </summary>
        public DriverUninstallationException()
        {
        }

        /// <summary>
        /// Public Constructor with message
        /// </summary>
        /// <param name="message"></param>
        public DriverUninstallationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Public Constructor with stack trace
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DriverUninstallationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Public Constructor with Serialization Information
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected DriverUninstallationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}