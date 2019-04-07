using System;
using System.Runtime.Serialization;

namespace Netlenium.Manager
{
    /// <summary>
    /// Raises when there is an issue with the OS Permissions
    /// </summary>
    [Serializable]
    internal class PermissionsErrorException : Exception
    {
        /// <summary>
        /// Public Constructor without context
        /// </summary>
        public PermissionsErrorException()
        {
        }

        /// <summary>
        /// Public Constructor with message
        /// </summary>
        /// <param name="message"></param>
        public PermissionsErrorException(string message) : base(message)
        {
        }

        /// <summary>
        /// Public Constructor with Serialization Information
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public PermissionsErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}