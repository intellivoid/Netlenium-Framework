namespace Netlenium.Driver
{
    /// <summary>
    /// Controls the Web Browser
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// If set to true, when using the Start() Method, the Web Browser will start in
        /// headless mode
        /// </summary>
        bool Headless { get; set; }
        
        /// <summary>
        /// Netlenium General logging
        /// </summary>
        bool GeneralLoggingEnabled { get; set; }

        /// <summary>
        /// Netlenium Verbose Logging
        /// </summary>
        bool VerboseLoggingEnabled { get; set; }
        
        /// <summary>
        /// Logging directly from the Driver/WebBrowser
        /// </summary>
        bool DriverLoggingEnabled { get; set; }
        
        /// <summary>
        /// Verbose Logging directly from the Driver/WebBrowser
        /// </summary>
        bool DriverVerboseLoggingEnabled { get; set; }
        
        /// <summary>
        /// The Target platform that the Driver Manager will try to install Drivers for
        /// </summary>
        PlatformType TargetPlatform { get; set; }
    
        /// <summary>
        /// Actions to invoke the Web Browser
        /// </summary>
        IActions Actions { get; }

        /// <summary>
        /// Starts the Web Browser & Driver Services
        /// </summary>
        void Start();

        /// <summary>
        /// Shutdown the active services and kills the Web Browser
        /// </summary>
        void Stop();

        /// <summary>
        /// Restarts the active services and restarts the Web Browser in a clean session
        /// </summary>
        void Restart();
    }
}