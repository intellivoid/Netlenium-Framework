using System;
using System.Runtime.Serialization;

namespace Netlenium.Driver.GeckoFXLib
{
    /// <summary>
    /// Raises when the given search method is not supported for GeckoFX
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
        /// Public Constructor with stack trace
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SearchTypeNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Public Constructor with serialization information
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SearchTypeNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}