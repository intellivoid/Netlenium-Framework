namespace Netlenium
{
    /// <summary>
    /// Browser Proxy Configuration
    /// </summary>
    public class ProxyConfiguration
    {
        /// <summary>
        /// Enable the use of a proxy before starting the browser
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// The scheme that's used with this proxy
        /// </summary>
        public Types.Scheme Scheme { get; set; }

        /// <summary>
        /// Remote IP Address of the proxy
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Remote Port of the proxy
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Enables the use of authentication
        /// </summary>
        public bool UseAuthentication { get; set; }

        /// <summary>
        /// The username of the authentication method that will be used
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password of the authentication method that will be used
        /// </summary>
        public string Password { get; set; }
    }
}
