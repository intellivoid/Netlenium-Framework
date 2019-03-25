using System;

namespace Netlenium.WebDriver.Remote
{
    /// <summary>
    /// Provides a way to start a server that understands remote commands
    /// </summary>
    public interface ICommandServer : IDisposable
    {
        /// <summary>
        /// Starts the server.
        /// </summary>
        void Start();
    }
}
