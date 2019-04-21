using System.Collections.Generic;
using System.IO;
using Netlenium.Driver.WebDriver.Chrome;
using Netlenium.Driver.WebDriver.Remote;

namespace Netlenium.Driver.Chrome
{
    internal class Controller : IController
    {
        /// <summary>
        /// Actions for controlling the Web Browser
        /// </summary>
        private IActions actions;

        /// <summary>
        /// Manages the driver installation for the target platform
        /// </summary>
        private IDriverManager DriverManager { get; set; }
        
        /// <summary>
        /// Options that are passed on to the Chrome Driver
        /// </summary>
        private ChromeOptions DriverOptions { get; set; }
        
        /// <summary>
        /// The Driver Service process manager
        /// </summary>
        private ChromeDriverService DriverService { get; set; }
        
        /// <summary>
        /// Remote Driver Client for controlling the Driver Service
        /// </summary>
        private RemoteWebDriver RemoteDriver { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Indicates if the Browser is intended to start Headless
        /// (Only effective before starting the Browser)
        /// </summary>
        public bool Headless { get; set; }
        
        /// <inheritdoc />
        /// <summary>
        /// Indicates if General Logging regarding Netlenium is displayed onto the Command Line
        /// </summary>
        public bool GeneralLoggingEnabled { get; set; }
        
        /// <inheritdoc />
        /// <summary>
        /// Indicates if Verbose Logging regarding Netlenium is displayed onto the Command Line
        /// </summary>
        public bool VerboseLoggingEnabled { get; set; }
        
        /// <inheritdoc />
        /// <summary>
        /// Driver output to be displayed to the Command Line
        /// (Only effective before starting the Browser)
        /// </summary>
        public bool DriverLoggingEnabled { get; set; }
        
        /// <inheritdoc />
        /// <summary>
        /// Driver verbose output to be displayed to the Command Line
        /// (Only effective before starting the Browser)
        /// </summary>
        public bool DriverVerboseLoggingEnabled { get; set; }
        
        /// <inheritdoc />
        /// <summary>
        /// The platform that the Driver Manager will try to target
        /// </summary>
        public PlatformType TargetPlatform { get; set; }

        /// <summary>
        /// Sets the current options for Chrome Driver
        /// </summary>
        /// <param name="options"></param>
        private void SetOptions(Dictionary<string, string> options)
        {
            DriverOptions = new ChromeOptions();

            foreach (var option in options)
            {
                DriverOptions.AddArgument(option.Value == string.Empty ? option.Key : $"{option.Key}={option.Value}");
            }
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Actions for controlling the Browser
        /// </summary>
        IActions IController.Actions => actions;

        /// <inheritdoc />
        /// <summary>
        /// Starts the Chrome Driver and the Chrome Web Browser
        /// </summary>
        public void Start()
        {
            DriverManager = new DriverManager {TargetPlatform = TargetPlatform};
            DriverManager.Initialize();
            DriverService = ChromeDriverService.CreateDefaultService(DriverManager.DriverPath, DriverManager.DriverExecutableName);

            var options = new Dictionary<string, string>();

            if (Headless)
            {
                options.Add("headless", string.Empty);
                options.Add("window-size", "1200x600");
            }

            if (DriverLoggingEnabled == false)
            {
                options.Add("log-level", "0");
                options.Add("silent", string.Empty);
                DriverService.SuppressInitialDiagnosticInformation = true;
            }
            else
            {
                if (DriverVerboseLoggingEnabled)
                {
                    options.Add("log-level", "1");
                    DriverService.EnableVerboseLogging = true;
                }
                else
                {
                    options.Add("log-level", "2");
                    DriverService.EnableVerboseLogging = false;
                }
                
                DriverService.SuppressInitialDiagnosticInformation = false;
            }
            
            SetOptions(options);
            DriverService.LogPath = $"{Paths.LoggingDirectory}{Path.DirectorySeparatorChar}chrome_driver.log";
            DriverService.Start();
            RemoteDriver = new RemoteWebDriver(DriverService.ServiceUrl, DriverOptions);
            
            actions = new Actions(RemoteDriver);

        }

        /// <inheritdoc />
        /// <summary>
        /// Stops the Driver Session and kills the Web Browser
        /// </summary>
        public void Stop()
        {
            if (DriverService == null)
            {
                throw new ControllerException("This controller has not initialized any driver/service");
            }
            
            RemoteDriver.Dispose();
            DriverService.Dispose();
        }

        /// <inheritdoc />
        /// <summary>
        /// Restarts the Driver Session and re-opens the Browser to a new clean state
        /// </summary>
        public void Restart()
        {
            Stop();
            Start();
        }
    }
}
