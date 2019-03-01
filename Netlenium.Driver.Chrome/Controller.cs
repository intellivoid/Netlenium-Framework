using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
        public ChromeDriver _Driver;

        private IJavaScriptExecutor _JavascriptExecuter;

        /// <summary>
        /// Constructs the chrome controller and configures the chrome driver
        /// </summary>
        public Controller()
        {
            // Update the driver
            if(Driver.IsInstalled == false)
            {
                Driver.Install();
            }
            else
            {
                if(Driver.IsOutdated == true)
                {
                    Driver.Update();
                }
            }
        }

        /// <summary>
        /// Initializes the Chrome Driver
        /// </summary>
        public void Initialize()
        {
            this._Driver = new ChromeDriver(Driver.DriverDirectory);
            this._JavascriptExecuter = (IJavaScriptExecutor)this._Driver;
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
        /// Finds the requested elements on the page
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
