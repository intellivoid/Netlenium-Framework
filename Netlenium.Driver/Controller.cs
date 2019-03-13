using System;
using System.Collections.Generic;

namespace Netlenium.Driver
{
    public class Controller
    {
        /// <summary>
        /// The driver type that this controller is currently operating as
        /// </summary>
        private Types.Driver _DriverType;

        /// <summary>
        /// The chrome driver controller
        /// </summary>
        private Chrome.Controller _ChromeController;

        private GeckoFXLib.Controller _GeckoController;
        
        /// <summary>
        /// Controller Constructor
        /// </summary>
        /// <param name="DriverType"></param>
        public Controller(Types.Driver DriverType, DriverConfiguration DriverConfiguration)
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Constructing Driver Instance");
            switch(DriverType)
            {
                case Types.Driver.Chrome:
                    Manager.Chrome.Initialize(DriverConfiguration.TargetPlatform);
                    DriverInstallationDetails ChromeInstallationDetails = Manager.Chrome.CheckInstallation(DriverConfiguration.TargetPlatform);
                    this._ChromeController = new Chrome.Controller(DriverConfiguration, ChromeInstallationDetails);
                    this._DriverType = DriverType;
                    break;

                case Types.Driver.GeckoLib:
                    DriverInstallationDetails GeckoFX32InstallationDetails = Manager.Chrome.CheckInstallation(DriverConfiguration.TargetPlatform);
                    this._GeckoController = new GeckoFXLib.Controller(DriverConfiguration, GeckoFX32InstallationDetails);
                    this._DriverType = DriverType;
                    break;
            }
        }

        /// <summary>
        /// Initializes the driver
        /// </summary>
        /// <param name="Hide">Hides the WebView, throws a warning if it's not available for the driver</param>
        public void Initialize(bool Hide = false)
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Initializing Driver");
            switch (_DriverType)
            {
                case Types.Driver.Chrome:
                    this._ChromeController.Initialize();
                    break;

                case Types.Driver.GeckoLib:
                    this._GeckoController.Initialize();
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
            switch(_DriverType)
            {
                case Types.Driver.Chrome:
                    _ChromeController.Quit();
                    Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Success");
                    break;

                case Types.Driver.GeckoLib:
                    _GeckoController.Quit();
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
                switch(_DriverType)
                {
                    case Types.Driver.Chrome:
                        return _ChromeController.DocumentTitle;

                    case Types.Driver.GeckoLib:
                        return _GeckoController.DocumentTitle;

                    default:
                        throw new PropertyNotAvailableForSelectedDriver();
                }
            }
        }

        /// <summary>
        /// The current URL
        /// </summary>
        public string URL
        {
            get
            {
                switch(_DriverType)
                {
                    case Types.Driver.Chrome:
                        return _ChromeController.URL;

                    case Types.Driver.GeckoLib:
                        return _GeckoController.URL;

                    default:
                        throw new PropertyNotAvailableForSelectedDriver();
                }
            }
        }

        /// <summary>
        /// Executes Javascript Code
        /// </summary>
        /// <param name="Code"></param>
        /// <returns>Execution Results</returns>
        public string ExecuteJS(string Code)
        {
            switch (_DriverType)
            {
                case Types.Driver.Chrome:
                    try
                    {

                        return this._ChromeController.ExecuterJS(Code);
                    }
                    catch(Exception exception)
                    {
                        throw new JavascriptExecutionException(exception.Message);
                    }

                case Types.Driver.GeckoLib:
                    try
                    {
                        return this._GeckoController.ExecuteJS(Code);
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
        /// <param name="URL"></param>
        public void Navigate(string URL)
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", $"Navigating to {URL}");

            switch (_DriverType)
            {
                case Types.Driver.Chrome:
                    this._ChromeController.Naviagte(URL);
                    break;

                case Types.Driver.GeckoLib:
                    this._GeckoController.Navigate(URL);
                    break;

                default:
                    Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver", "The method Navigate() is not supported for the selected driver");
                    throw new MethodNotSupportedForDriver();
            }

            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", $"Navigated");
        }

        /// <summary>
        /// Moves back a single entry in the browser's history
        /// </summary>
        public void GoBack()
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", $"Navigating Back");

            switch (_DriverType)
            {
                case Types.Driver.Chrome:
                    this._ChromeController.GoBack();
                    break;

                case Types.Driver.GeckoLib:
                    this._GeckoController.GoBack();
                    break;

                default:
                    Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver", "The method GoBack() is not supported for the selected driver");
                    throw new MethodNotSupportedForDriver();
            }
            
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", $"Navigated");
        }

        /// <summary>
        /// Moves a single "item" forward in the browser's history.
        /// </summary>
        public void GoForward()
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", $"Navigating Forward");

            switch (_DriverType)
            {
                case Types.Driver.Chrome:
                    this._ChromeController.GoForward();
                    break;

                case Types.Driver.GeckoLib:
                    this._GeckoController.GoForward();
                    break;

                default:
                    Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver", "The method GoForward() is not supported for the selected driver");
                    throw new MethodNotSupportedForDriver();
            }

            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", $"Navigated");
        }

        /// <summary>
        /// Returns a live ElementCollection of elements with the given search type name and input
        /// </summary>
        /// <param name="SearchType"></param>
        /// <param name="Input"></param>
        /// <returns></returns>
        public List<WebElement> GetElements(Types.SearchType SearchType, string Input)
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", $"Getting Elements by {SearchType.ToString()} ({Input})");
            List<WebElement> WebElements = new List<WebElement>();

            switch (_DriverType)
            {
                case Types.Driver.Chrome:
                    List<Chrome.Element> ChromeResults = _ChromeController.GetElements(SearchType, Input);

                    foreach (Chrome.Element ChromeElement in ChromeResults)
                    {
                        WebElements.Add(new WebElement(ChromeElement));
                    }

                    Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", $"Returned {WebElements.Count} element(s)");
                    return WebElements;

                case Types.Driver.GeckoLib:
                    List<GeckoFXLib.Element> GeckoFXLibResults = _GeckoController.GetElements(SearchType, Input);

                    foreach(GeckoFXLib.Element GeckoFXLibElement in GeckoFXLibResults)
                    {
                        WebElements.Add(new WebElement(GeckoFXLibElement));
                    }

                    Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", $"Returned {WebElements.Count} element(s)");
                    return WebElements;

                default:
                    Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver", "The method GetElements() is not supported for the selected driver");
                    throw new MethodNotSupportedForDriver();
            }
        }
    }
}
