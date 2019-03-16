using System;
using System.Collections.Generic;
using System.Linq;

namespace Netlenium.Driver
{
    public class Controller
    {
        /// <summary>
        /// The driver type that this controller is currently operating as
        /// </summary>
        private Types.Driver DriverType { get; }

        /// <summary>
        /// The chrome driver controller
        /// </summary>
        private Chrome.Controller ChromeController { get; set; }

        /// <summary>
        /// The GeckoFX Core Lib Controller
        /// </summary>
        private GeckoFXLib.Controller GeckoController { get; set; }

        /// <summary>
        /// Controller Constructor
        /// </summary>
        /// <param name="driverConfiguration"></param>
        public Controller(DriverConfiguration driverConfiguration)
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Constructing Driver Instance");
            switch(driverConfiguration.TargetDriver)
            {
                case Types.Driver.Chrome:
                    
                    Manager.Chrome.Initialize(driverConfiguration.TargetPlatform);
                    var chromeInstallationDetails = Manager.Chrome.CheckInstallation(driverConfiguration.TargetPlatform);
                    ChromeController = new Chrome.Controller(driverConfiguration, chromeInstallationDetails);
                    DriverType = driverConfiguration.TargetDriver;
                    break;

                case Types.Driver.GeckoLib:
                    
                    var geckoFx32InstallationDetails = Manager.GeckoFX32.CheckInstallation(driverConfiguration.TargetPlatform);
                    GeckoController = new GeckoFXLib.Controller(driverConfiguration, geckoFx32InstallationDetails);
                    DriverType = driverConfiguration.TargetDriver;
                    break;
                    
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Initializes the driver
        /// </summary>
        /// <param name="hide">Hides the WebView, throws a warning if it's not available for the driver</param>
        public void Initialize(bool hide = false)
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Initializing Driver");
            switch (DriverType)
            {
                case Types.Driver.Chrome:
                    ChromeController.Initialize();
                    break;

                case Types.Driver.GeckoLib:
                    GeckoController.Initialize();
                    break;

                default:
                    throw new MethodNotSupportedForDriver();
            }
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Initialized");
        }

        /// <summary>
        /// Quits the driver and unreleases used resources
        /// </summary>
        public void Quit()
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Quitting Driver, unreleasing used resources");
            switch(DriverType)
            {
                case Types.Driver.Chrome:
                    ChromeController.Quit();
                    ChromeController.Dispose();
                    ChromeController = null;
                    Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Success");
                    break;

                case Types.Driver.GeckoLib:
                    GeckoController.Quit();
                    GeckoController.Dispose();
                    GeckoController = null;
                    Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Success");
                    break;

                default:
                    Logging.WriteEntry(Types.LogType.Error, "Neltneium.Driver", "The given method Quit() is not supported for the given driver");
                    throw new MethodNotSupportedForDriver();
            }
        }

        /// <summary>
        /// the current title of the document
        /// </summary>
        public string DocumentTitle
        {
            get
            {
                switch(DriverType)
                {
                    case Types.Driver.Chrome:
                        return ChromeController.DocumentTitle;

                    case Types.Driver.GeckoLib:
                        return GeckoController.DocumentTitle;

                    default:
                        throw new PropertyNotAvailableForSelectedDriver();
                }
            }
        }

        /// <summary>
        /// The current URL
        /// </summary>
        public string Url
        {
            get
            {
                switch(DriverType)
                {
                    case Types.Driver.Chrome:
                        return ChromeController.Url;

                    case Types.Driver.GeckoLib:
                        return GeckoController.URL;

                    default:
                        throw new PropertyNotAvailableForSelectedDriver();
                }
            }
        }

        /// <summary>
        /// Executes Javascript Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Execution Results</returns>
        public string ExecuteJs(string code)
        {
            switch (DriverType)
            {
                case Types.Driver.Chrome:
                    try
                    {

                        return ChromeController.ExecuterJs(code);
                    }
                    catch(Exception exception)
                    {
                        throw new JavascriptExecutionException(exception.Message);
                    }

                case Types.Driver.GeckoLib:
                    try
                    {
                        return GeckoController.ExecuteJS(code);
                    }
                    catch(Exception exception)
                    {
                        throw new JavascriptExecutionException(exception.Message);
                    }

                default:
                    throw new MethodNotSupportedForDriver();
            }
        }

        /// <summary>
        /// Navigates to the request URL (Blocks until the request has been completed)
        /// </summary>
        /// <param name="url"></param>
        public void Navigate(string url)
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", $"Navigating to {url}");

            switch (DriverType)
            {
                case Types.Driver.Chrome:
                    ChromeController.Naviagte(url);
                    break;

                case Types.Driver.GeckoLib:
                    GeckoController.Navigate(url);
                    break;

                default:
                    Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver", "The method Navigate() is not supported for the selected driver");
                    throw new MethodNotSupportedForDriver();
            }

            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Navigated");
        }

        /// <summary>
        /// Moves back a single entry in the browser's history
        /// </summary>
        public void GoBack()
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Navigating Back");

            switch (DriverType)
            {
                case Types.Driver.Chrome:
                    ChromeController.GoBack();
                    break;

                case Types.Driver.GeckoLib:
                    GeckoController.GoBack();
                    break;

                default:
                    Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver", "The method GoBack() is not supported for the selected driver");
                    throw new MethodNotSupportedForDriver();
            }
            
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Navigated");
        }

        /// <summary>
        /// Moves a single "item" forward in the browser's history.
        /// </summary>
        public void GoForward()
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Navigating Forward");

            switch (DriverType)
            {
                case Types.Driver.Chrome:
                    ChromeController.GoForward();
                    break;

                case Types.Driver.GeckoLib:
                    GeckoController.GoForward();
                    break;

                default:
                    Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver", "The method GoForward() is not supported for the selected driver");
                    throw new MethodNotSupportedForDriver();
            }

            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Navigated");
        }

        /// <summary>
        /// Returns a live ElementCollection of elements with the given search type name and input
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<WebElement> GetElements(Types.SearchType searchType, string input)
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", $"Getting Elements by {searchType} ({input})");
            var webElements = new List<WebElement>();

            switch (DriverType)
            {
                case Types.Driver.Chrome:
                    
                    var chromeResults = ChromeController.GetElements(searchType, input);

                    webElements.AddRange(chromeResults.Select(chromeElement => new WebElement(chromeElement)));

                    Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", $"Returned {webElements.Count} element(s)");
                    return webElements;

                case Types.Driver.GeckoLib:
                    
                    var geckoFxLibResults = GeckoController.GetElements(searchType, input);

                    webElements.AddRange(geckoFxLibResults.Select(geckoFxLibElement => new WebElement(geckoFxLibElement)));

                    Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", $"Returned {webElements.Count} element(s)");
                    return webElements;

                default:

                    Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver", "The method GetElements() is not supported for the selected driver");
                    throw new MethodNotSupportedForDriver();
            }
        }

        /// <summary>
        /// Returns a live Element object with the given search type namd and input
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NoElementsFoundException"></exception>
        public WebElement GetElement(Types.SearchType searchType, string input)
        {
            var results = GetElements(searchType, input);
            
            if (results.Count > 0)
            {
                return results[0];
            }
            
            throw new NoElementsFoundException();
        }
    }
}
