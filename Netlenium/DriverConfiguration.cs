namespace Netlenium
{
    /// <summary>
    /// Driver Configuration Class
    /// </summary>
    public class DriverConfiguration
    {
        /// <summary>
        /// Starts the browser in headless mode
        /// </summary>
        public bool Headless
        {
            get; set;
        }
        
        /// <summary>
        /// The target platform that the Framework will try
        /// to execute the drivers for
        /// </summary>
        public Types.Platform TargetPlatform { get; set; }

        /// <summary>
        /// The target driver that will be initialized
        /// </summary>
        public Types.Driver TargetDriver { get; set; }

        /// <summary>
        /// If set to True, general logging messages from the Framework will be displayed in the CLI
        /// </summary>
        public bool FrameworkLogging
        {
            get => Logging.Enabled;
            set => Logging.Enabled = value;
        }

        /// <summary>
        /// If set to True, alongside general framework logging messages; verbose messages will also be displayed in the CLI
        /// </summary>
        public bool FrameworkVerboseLogging
        {
            get => Logging.VerboseLogging;
            set => Logging.VerboseLogging = value;
        }

        /// <summary>
        /// If set to true, driver logs will be displayed in the CLI
        /// </summary>
        public bool DriverLogging { get; set; }
        
        /// <summary>
        /// If set to true, verbose messages will also be displayed if DriverLogging is set to True
        /// </summary>
        public bool DriverVerboseLogging { get; set; }

        /// <summary>
        /// Proxy configuration 
        /// </summary>
        public ProxyConfiguration Proxy { get; set; }
        
        /// <summary>
        /// Public Consturctor
        /// </summary>
        public DriverConfiguration()
        {
            Headless = true;
            TargetPlatform = Configuration.CurrentPlatform;
            Proxy = new ProxyConfiguration
            {
                Enabled = false,
                UseAuthentication = false,
                IP = "127.0.0.1",
                Port = 8080,
                Username = "anonymous",
                Password = "anonymous"
            };
        }
    }
}
