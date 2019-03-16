﻿namespace Netlenium
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
        /// If set to True, general logging messages will be displayed in the CLI
        /// </summary>
        public bool GeneralLogging
        {
            get => Logging.GeneralLogging;
            set => Logging.GeneralLogging = value;
        }

        /// <summary>
        /// If set to True, alongside general logging messages; debugging messages will also be displayed in the CLI
        /// </summary>
        public bool DebugLogging
        {
            get => Logging.DebugLogging;
            set => Logging.DebugLogging = value;
        }

        /// <summary>
        /// Public Consturctor
        /// </summary>
        public DriverConfiguration()
        {
            Headless = true;
            TargetPlatform = Configuration.CurrentPlatform;
        }
    }
}
