using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netlenium.Driver
{
    /// <summary>
    /// Driver Client for controlling a Browser/WebDriver
    /// </summary>
    public class Client
    {
        /// <summary>
        /// The platform that the driver manager will try to download the required
        /// files for
        /// </summary>
        public PlatformType TargetPlatform { get; }

        /// <summary>
        /// The target driver that this client will control
        /// </summary>
        public BrowserType TargetBrowser { get; set; }

        /// <summary>
        /// General Logging about the Netlenium Driver will be printed out to the command-line
        /// </summary>
        public bool GeneralLoggingEnabled { get; set; }

        /// <summary>
        /// Verbose Logging about the Netlenium Driver will be printed out to the command-line
        /// </summary>
        public bool VerboseLoggingEnabled { get; set; }

        /// <summary>
        /// Output data from the driver/browser will be displayed in the command-line
        /// </summary>
        public bool DriverLoggingEnabled { get; set; }

        /// <summary>
        /// Verbose data (if available) from the driver/browser will be displayed in the command-line
        /// </summary>
        public bool DriverVerboseLoggingEnabled { get; set; }
        
        /// <summary>
        /// Controls the driver interface
        /// </summary>
        private IController DriverController { get; set; }
        
        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="targetBrowser">The browser that this Client will utilize</param>
        public Client(BrowserType targetBrowser)
        {
            TargetBrowser = targetBrowser;
            TargetPlatform = PlatformType.AutoDetect;
            GeneralLoggingEnabled = true;
            VerboseLoggingEnabled = false;
            DriverLoggingEnabled = false;
            DriverVerboseLoggingEnabled = false;
        }

        /// <summary>
        /// Starts the Driver/Browser Client session
        /// </summary>
        public void Start()
        {
            switch (TargetBrowser)
            {
                case BrowserType.Chrome:
                    DriverController = new Chrome.Controller();
                    break;
                
                default:
                    throw new UnsupportedBrowserTypeException();
            }
        }
        
        /// <summary>
        /// Stops the current Driver/Browser session
        /// </summary>
        public void Stop()
        {
        }

        /// <summary>
        /// Restarts the current session
        /// </summary>
        public void Restart()
        {

        }
    }
}
