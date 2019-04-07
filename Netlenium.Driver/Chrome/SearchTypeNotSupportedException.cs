using System;
using System.Runtime.Serialization;

namespace Netlenium.Driver.Chrome
{
    /// <summary>
    /// Raises when the search type given to the driver is not supported
    /// </summary>
    [Serializable]
    internal class SearchTypeNotSupportedException : Exception
    {
        /// <summary>
        /// Public Constructor without context
        /// </summary>
        public SearchTypeNotSupportedException()
        {
        }

        /// <summary>
        /// Public Constructor with message
        /// </summary>
        /// <param name="message"></param>
        public SearchTypeNotSupportedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Public Constructor with Stack Trace
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SearchTypeNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Public Constructor with Serialization Information
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SearchTypeNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}