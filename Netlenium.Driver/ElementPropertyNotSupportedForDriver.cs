using System;
using System.Runtime.Serialization;

namespace Netlenium.Driver
{
    /// <summary>
    /// Raises when the Element Property is not supported for the selected driver
    /// </summary>
    [Serializable]
    internal class ElementPropertyNotSupportedForDriver : Exception
    {
        /// <summary>
        /// Public Constructor without any context
        /// </summary>
        public ElementPropertyNotSupportedForDriver()
        {
        }

        /// <summary>
        /// Public Constructor with Message
        /// </summary>
        /// <param name="message"></param>
        public ElementPropertyNotSupportedForDriver(string message) : base(message)
        {
        }

        /// <summary>
        /// Public Constructor with Inner Exception stack trace
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ElementPropertyNotSupportedForDriver(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Public Cos tructor for Serialized Objects
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ElementPropertyNotSupportedForDriver(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}