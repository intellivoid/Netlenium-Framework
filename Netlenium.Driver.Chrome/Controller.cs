using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Netlenium.Driver.Chrome
{
    /// <summary>
    /// Chrome Controller Class
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Primary Driver Controller
        /// </summary>
        public ChromeDriver _Driver;

        /// <summary>
        /// Container for executing Javascript Calls
        /// </summary>
        private IJavaScriptExecutor _JavascriptExecuter;

        /// <summary>
        /// Handles Selenium interactions
        /// </summary>
        private Actions _DriverAction;

        /// <summary>
        /// The configuration that targets this driver
        /// </summary>
        private DriverConfiguration Configuration;

        /// <summary>
        /// The driver installation details
        /// </summary>
        private DriverInstallationDetails DriverInstallation;
        
        /// <summary>
        /// Constructs the chrome controller and configures the chrome driver
        /// </summary>
        public Controller(DriverConfiguration DriverConfiguration, DriverInstallationDetails DriverInstallation)
        {
            Configuration = DriverConfiguration;
            this.DriverInstallation = DriverInstallation;
        }

        /// <summary>
        /// Initializes the Chrome Driver
        /// </summary>
        public void Initialize()
        {
            if(Configuration.Headless == true)
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("headless");
                options.AddArguments("window-size=1200x600");

                this._Driver = new ChromeDriver(DriverInstallation.DriverPath, options);
            }
            else
            {
                this._Driver = new ChromeDriver(DriverInstallation.DriverPath);
            }
            
            this._JavascriptExecuter = (IJavaScriptExecutor)this._Driver;
            this._DriverAction = new Actions(this._Driver);

        }

        /// <summary>
        /// Quits the driver and unreleases used resources
        /// </summary>
        public void Quit()
        {
            _Driver.Quit();
        }

        /// <summary>
        /// the current title of the document
        /// </summary>
        public string DocumentTitle
        {
            get
            {
                return _Driver.Title;
            }
        }

        /// <summary>
        /// The current URL
        /// </summary>
        public string URL
        {
            get
            {
                return _Driver.Url;
            }
        }

        /// <summary>
        /// Executes Javascript Code
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public string ExecuterJS(string Code)
        {
            return Convert.ToString(this._JavascriptExecuter.ExecuteScript(Code));
        }

        /// <summary>
        /// Navigates the given URL (Blocks until the navigation has been completed)
        /// </summary>
        /// <param name="URL"></param>
        public void Naviagte(string URL)
        {
            this._Driver.Navigate().GoToUrl(URL);
        }

        /// <summary>
        /// Moves back a single entry in the browser's history
        /// </summary>
        public void GoBack()
        {
            this._Driver.Navigate().Back();
        }

        /// <summary>
        /// Moves a single "item" forward in the browser's history.
        /// </summary>
        public void GoForward()
        {
            this._Driver.Navigate().Forward();
        }

        /// <summary>
        /// Moves to the given IWebElement
        /// </summary>
        /// <param name="Element"></param>
        public void MoveTo(IWebElement Element)
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver.Chrome", $"Moving to element \"{Element.ToString()}\"");
            try
            {
                _DriverAction.MoveToElement(Element);
            }
            catch (Exception exception)
            {
                Logging.WriteEntry(Types.LogType.Warning, "Netlenium.Driver.Chrome", $"Cannot move to element; {exception.Message}");
            }
        }

        /// <summary>
        /// Returns a live ElementCollection of elements with the given search type name and input
        /// </summary>
        /// <param name="SearchType"></param>
        /// <param name="Input"></param>
        /// <returns></returns>
        public List<Element> GetElements(Types.SearchType SearchType, string Input)
        {
            List<Element> Elements = new List<Element>();

            switch (SearchType)
            {
                case Types.SearchType.ClassName:

                    foreach(IWebElement FoundElement in this._Driver.FindElements(By.ClassName(Input)))
                    {
                        Elements.Add(new Element(FoundElement, this));
                    }

                    return Elements;

                case Types.SearchType.CssSelector:

                    foreach (IWebElement FoundElement in this._Driver.FindElements(By.CssSelector(Input)))
                    {
                        Elements.Add(new Element(FoundElement, this));
                    }

                    return Elements;

                case Types.SearchType.Id:
                    foreach (IWebElement FoundElement in this._Driver.FindElements(By.Id(Input)))
                    {
                        Elements.Add(new Element(FoundElement, this));
                    }

                    return Elements;

                case Types.SearchType.TagName:
                    foreach (IWebElement FoundElement in this._Driver.FindElements(By.TagName(Input)))
                    {
                        Elements.Add(new Element(FoundElement, this));
                    }

                    return Elements;

                case Types.SearchType.Name:
                    foreach (IWebElement FoundElement in this._Driver.FindElements(By.Name(Input)))
                    {
                        Elements.Add(new Element(FoundElement, this));
                    }

                    return Elements;

                default:
                    throw new SearchTypeNotSupportedException();
            }
        }
    }
}
