using Netlenium.WebDriver;
using Netlenium.WebDriver.Chrome;
using Netlenium.WebDriver.Interactions;
using Netlenium.WebDriver.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Netlenium.Driver.Chrome
{
    /// <summary>
    /// Chrome Controller Class
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Primary Remote Driver Controller
        /// </summary>
        public RemoteWebDriver RemoteDriver;

        /// <summary>
        /// Container for executing Javascript Calls
        /// </summary>
        private IJavaScriptExecutor JavascriptExecuter { get; set; }

        /// <summary>
        /// Handles Selenium interactions
        /// </summary>
        private Actions _driverAction;

        /// <summary>
        /// The configuration that targets this driver
        /// </summary>
        private DriverConfiguration Configuration { get; }

        /// <summary>
        /// The driver installation details
        /// </summary>
        private  DriverInstallationDetails DriverInstallation { get; }

        /// <summary>
        /// Chrome driver service
        /// </summary>
        public ChromeDriverService DriverService { get; set; }

        /// <summary>
        /// Chrome Options
        /// </summary>
        private ChromeOptions DriverOptions { get; set; }
        
        /// <summary>
        /// Constructs the chrome controller and configures the chrome driver
        /// </summary>
        public Controller(DriverConfiguration driverConfiguration, DriverInstallationDetails driverInstallation)
        {
            Configuration = driverConfiguration;
            DriverInstallation = driverInstallation;
        }

        /// <summary>
        /// Adds an argument
        /// </summary>
        /// <param name="paramerter"></param>
        private void AddArgument(string paramerter)
        {
            Logging.WriteVerboseEntry("Netlenium.Driver.Chrome", $"Adding Argument to Chrome \"{paramerter}\"");
            DriverOptions.AddArgument(paramerter);
        }
        
        /// <summary>
        /// Initializes the Chrome Driver
        /// </summary>
        public void Initialize()
        {
            Logging.WriteVerboseEntry("Netlenium.Driver.Chrome", "Creating ChromeOptions Object");
            DriverOptions = new ChromeOptions();
            Logging.WriteVerboseEntry("Netlenium.Driver.Chrome", $"Creating Chrome Driver Service using {DriverInstallation.DriverExecutableName} from {DriverInstallation.DriverPath}");
            DriverService = ChromeDriverService.CreateDefaultService(DriverInstallation.DriverPath, DriverInstallation.DriverExecutableName);
            
            if (Configuration.Headless)
            {
                AddArgument("headless");
                AddArgument("window-size=1200x600");
            }

            if (Configuration.DriverLogging)
            {
                if (Configuration.DriverVerboseLogging)
                {
                    AddArgument("log-level=1");
                    Logging.WriteVerboseEntry("Netlenium.Driver.Chrome", "Enabling Verbose Logging from Driver Service");
                    DriverService.EnableVerboseLogging = true;
                }
                else
                {
                    AddArgument("log-level=2");
                    Logging.WriteVerboseEntry("Netlenium.Driver.Chrome", "Disabling Verbose Logging from Driver Service");
                    DriverService.EnableVerboseLogging = false;
                }
                
                Logging.WriteVerboseEntry("Netlenium.Driver.Chrome", "Initial Diagnostic Information will be suppressed");
                DriverService.SuppressInitialDiagnosticInformation = false;
            }
            else
            {
                AddArgument("log-level=0");
                AddArgument("silent");
                Logging.WriteVerboseEntry("Netlenium.Driver.Chrome", "Initial Diagnostic Information will be not be suppressed");
                DriverService.SuppressInitialDiagnosticInformation = true;
            }

            DriverService.LogPath = $"{Netlenium.Configuration.LoggingDirectory}{Path.DirectorySeparatorChar}chrome_debugging.log";
            Logging.WriteVerboseEntry("Netlenium.Driver.Chrome", $"Chrome Driver Service log path has been set to \"{DriverService.LogPath}\"");
            
            Logging.WriteVerboseEntry("Netlenium.Driver.Chrome", "Starting DriverService");
            DriverService.Start();
            
            Logging.WriteVerboseEntry("Netlenium.Driver.Chrome", $"Creating new Remote WebDriver client to connect to \"{DriverService.ServiceUrl}\"");
            RemoteDriver = new RemoteWebDriver(DriverService.ServiceUrl, DriverOptions);
            
            Logging.WriteVerboseEntry("Netlenium.Driver.Chrome", "Attaching Driver Javascript Execution");
            JavascriptExecuter = RemoteDriver;
            Logging.WriteVerboseEntry("Netlenium.Driver.Chrome", "Attaching Driver Actions");
            _driverAction = new Actions(RemoteDriver);

        }

        /// <summary>
        /// Quits the driver and unreleases used resources
        /// </summary>
        public void Quit()
        {
            RemoteDriver.Quit();
            DriverService.Dispose();
        }

        /// <summary>
        /// the current title of the document
        /// </summary>
        public string DocumentTitle => RemoteDriver.Title;

        /// <summary>
        /// The current URL
        /// </summary>
        public string Url => RemoteDriver.Url;

        /// <summary>
        /// Executes Javascript Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string ExecuterJs(string code)
        {
            return Convert.ToString(JavascriptExecuter.ExecuteScript(code));
        }

        /// <summary>
        /// Navigates the given URL (Blocks until the navigation has been completed)
        /// </summary>
        /// <param name="url"></param>
        public void Naviagte(string url)
        {
            RemoteDriver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Moves back a single entry in the browser's history
        /// </summary>
        public void GoBack()
        {
            RemoteDriver.Navigate().Back();
        }

        /// <summary>
        /// Moves a single "item" forward in the browser's history.
        /// </summary>
        public void GoForward()
        {
            RemoteDriver.Navigate().Forward();
        }

        /// <summary>
        /// Moves to the given IWebElement
        /// </summary>
        /// <param name="element"></param>
        public void MoveTo(IWebElement element)
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver.Chrome", $"Moving to element \"{element}\"");
            try
            {
                _driverAction.MoveToElement(element);
            }
            catch (Exception exception)
            {
                Logging.WriteEntry(Types.LogType.Warning, "Netlenium.Driver.Chrome", $"Cannot move to element; {exception.Message}");
            }
        }

        /// <summary>
        /// Returns a live ElementCollection of elements with the given search type name and input
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<Element> GetElements(Types.SearchType searchType, string input)
        {
            var elements = new List<Element>();

            switch (searchType)
            {
                case Types.SearchType.ClassName:

                    elements.AddRange(RemoteDriver.FindElements(By.ClassName(input)).Select(
                        foundElement => new Element(foundElement, this))
                    );
                    return elements;

                case Types.SearchType.CssSelector:

                    elements.AddRange(RemoteDriver.FindElements(By.CssSelector(input)).Select(
                        foundElement => new Element(foundElement, this))
                    );

                    return elements;

                case Types.SearchType.Id:
                    
                    elements.AddRange(RemoteDriver.FindElements(By.Id(input)).Select(
                        foundElement => new Element(foundElement, this))
                    );

                    return elements;

                case Types.SearchType.TagName:
                    
                    elements.AddRange(RemoteDriver.FindElements(By.TagName(input)).Select(
                        foundElement => new Element(foundElement, this))
                    );

                    return elements;

                case Types.SearchType.Name:
                    
                    elements.AddRange(RemoteDriver.FindElements(By.Name(input)).Select(
                        foundElement => new Element(foundElement, this))
                    );

                    return elements;

                default:
                    
                    throw new SearchTypeNotSupportedException();
            }
        }
        
        /// <summary>
        /// Disposes of the controller
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
