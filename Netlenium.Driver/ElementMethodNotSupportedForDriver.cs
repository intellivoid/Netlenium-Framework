using System;
using System.Runtime.Serialization;

namespace Netlenium.Driver
{
    /// <summary>
    /// Raised when the element type is not supported for the selected driver
    /// </summary>
    [Serializable]
    internal class ElementMethodNotSupportedForDriver : Exception
    {
        /// <summary>
        /// Public Constructor without any context
        /// </summary>
        public ElementMethodNotSupportedForDriver()
        {
        }

        /// <summary>
        /// Public Constructor with Message
        /// </summary>
        /// <param name="message"></param>
        public ElementMethodNotSupportedForDriver(string message) : base(message)
        {
        }

        /// <summary>
        /// Public Constructor with Inner Exception stack trace
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ElementMethodNotSupportedForDriver(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Public Cos tructor for Serialized Objects
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ElementMethodNotSupportedForDriver(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}