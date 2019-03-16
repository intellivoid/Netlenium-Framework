using System;
using System.Runtime.Serialization;

namespace Netlenium.Driver
{
    /// <summary>
    /// Raised when the element type is not supported for the selected driver
    /// </summary>
    [Serializable]
    internal class JavascriptExecutionException : Exception
    {
        /// <summary>
        /// Public Constructor without any context
        /// </summary>
        public JavascriptExecutionException()
        {
        }

        /// <summary>
        /// Public Constructor with Message
        /// </summary>
        /// <param name="message"></param>
        public JavascriptExecutionException(string message) : base(message)
        {
        }

        /// <summary>
        /// Public Constructor with Inner Exception stack trace
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public JavascriptExecutionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Public Constructor for Serialized Objects
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected JavascriptExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}