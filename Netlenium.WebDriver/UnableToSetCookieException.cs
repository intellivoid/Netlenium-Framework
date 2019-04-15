using System;
using System.Runtime.Serialization;

namespace Netlenium.WebDriver
{
    /// <inheritdoc />
    /// <summary>
    /// The exception that is thrown when the user is unable to set a cookie.
    /// </summary>
    [Serializable]
    public class UnableToSetCookieException : WebDriverException
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Netlenium.WebDriver.UnableToSetCookieException" /> class with
        /// a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnableToSetCookieException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Netlenium.WebDriver.UnableToSetCookieException" /> class with
        /// a specified error message and a reference to the inner exception that is the
        /// cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or <see langword="null" /> if no inner exception is specified.</param>
        public UnableToSetCookieException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Netlenium.WebDriver.UnableToSetCookieException" /> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized
        /// object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual
        /// information about the source or destination.</param>
        protected UnableToSetCookieException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
