using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Remote;

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
        private IJavaScriptExecutor _javascriptExecuter;

        /// <summary>
        /// Handles Selenium interactions
        /// </summary>
        private Actions _driverAction;

        /// <summary>
        /// The configuration that targets this driver
        /// </summary>
        private readonly DriverConfiguration _configuration;

        /// <summary>
        /// The driver installation details
        /// </summary>
        private readonly DriverInstallationDetails _driverInstallation;

        /// <summary>
        /// Chrome driver service
        /// </summary>
        public ChromeDriverService DriverService { get; set; }
        
        /// <summary>
        /// Constructs the chrome controller and configures the chrome driver
        /// </summary>
        public Controller(DriverConfiguration driverConfiguration, DriverInstallationDetails driverInstallation)
        {
            _configuration = driverConfiguration;
            _driverInstallation = driverInstallation;
        }

        /// <summary>
        /// Initializes the Chrome Driver
        /// </summary>
        public void Initialize()
        {
            var driverOptions = new ChromeOptions();
            DriverService = ChromeDriverService.CreateDefaultService(_driverInstallation.DriverPath, _driverInstallation.DriverExecutableName);
            
            if (_configuration.Headless)
            {
                driverOptions.AddArgument("headless");
                driverOptions.AddArguments("window-size=1200x600");
            }

            if (_configuration.DriverLogging)
            {
                if (_configuration.DriverVerboseLogging)
                {
                    driverOptions.AddArgument("log-level=1");
                    DriverService.EnableVerboseLogging = true;
                }
                else
                {
                    driverOptions.AddArgument("log-level=2");
                    DriverService.EnableVerboseLogging = false;
                }
                
                DriverService.SuppressInitialDiagnosticInformation = false;
            }
            else
            {
                driverOptions.AddArgument("log-level=0");
                driverOptions.AddArgument("silent");
                DriverService.SuppressInitialDiagnosticInformation = true;
            }
            
            DriverService.Start();
            RemoteDriver = new RemoteWebDriver(DriverService.ServiceUrl, driverOptions);
            _javascriptExecuter = RemoteDriver;
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
            return Convert.ToString(_javascriptExecuter.ExecuteScript(code));
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
            List<Element> elements = new List<Element>();

            switch (searchType)
            {
                case Types.SearchType.ClassName:

                    foreach(IWebElement foundElement in RemoteDriver.FindElements(By.ClassName(input)))
                    {
                        elements.Add(new Element(foundElement, this));
                    }

                    return elements;

                case Types.SearchType.CssSelector:

                    foreach (IWebElement foundElement in RemoteDriver.FindElements(By.CssSelector(input)))
                    {
                        elements.Add(new Element(foundElement, this));
                    }

                    return elements;

                case Types.SearchType.Id:
                    foreach (IWebElement foundElement in RemoteDriver.FindElements(By.Id(input)))
                    {
                        elements.Add(new Element(foundElement, this));
                    }

                    return elements;

                case Types.SearchType.TagName:
                    foreach (IWebElement foundElement in RemoteDriver.FindElements(By.TagName(input)))
                    {
                        elements.Add(new Element(foundElement, this));
                    }

                    return elements;

                case Types.SearchType.Name:
                    foreach (IWebElement foundElement in RemoteDriver.FindElements(By.Name(input)))
                    {
                        elements.Add(new Element(foundElement, this));
                    }

                    return elements;

                default:
                    throw new SearchTypeNotSupportedException();
            }
        }
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
