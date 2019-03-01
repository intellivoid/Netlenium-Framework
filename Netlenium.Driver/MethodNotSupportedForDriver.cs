using System;

namespace Netlenium.Driver
{
    /// <summary>
    /// Raised when the action is not supported for the selected driver
    /// </summary>
    public class MethodNotSupportedForDriver : Exception
    {
        /// <summary>
        /// Initializes a new instance of the MethodNotSupportedForDriver class.
        /// </summary>
        public MethodNotSupportedForDriver(): base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MethodNotSupportedForDriver class with
        /// a specified error message.
        /// </summary>
        public MethodNotSupportedForDriver(string message): base(message)
        {
        }
    }
}
