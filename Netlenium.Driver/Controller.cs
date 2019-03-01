using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Controller(Types.Driver DriverType)
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Constructing Driver Instance");
            switch(DriverType)
            {
                case Types.Driver.Chrome:
                    this._ChromeController = new Chrome.Controller();
                    this._DriverType = DriverType;
                    break;

                case Types.Driver.GeckoLib:
                    this._GeckoController = new GeckoFXLib.Controller();
                    this._DriverType = DriverType;
                    break;
            }
        }

        /// <summary>
        /// Initializes the driver
        /// </summary>
        public void Initialize()
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
        /// Executes Javascript Code
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public string ExecuteJS(string Code)
        {
            switch (_DriverType)
            {
                case Types.Driver.Chrome:
                    return this._ChromeController.ExecuterJS(Code);

                case Types.Driver.GeckoLib:
                    return this._GeckoController.ExecuteJS(Code);

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
