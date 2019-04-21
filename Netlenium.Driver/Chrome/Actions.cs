using System.Collections.Generic;
using System.Linq;
using Netlenium.Driver.WebDriver;
using Netlenium.Driver.WebDriver.Remote;

namespace Netlenium.Driver.Chrome
{
    /// <inheritdoc />
    /// <summary>
    /// Chrome Actions
    /// </summary>
    public class Actions : IActions
    {
        /// <summary>
        /// The Remote Web Driver that controls Chrome
        /// </summary>
        private RemoteWebDriver RemoteWebDriver { get; set; }

        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="remoteWebDriver"></param>
        public Actions(RemoteWebDriver remoteWebDriver)
        {
            RemoteWebDriver = remoteWebDriver;
        }

        /// <inheritdoc />
        /// <summary>
        /// Go back one page in the history.
        /// </summary>
        public void GoBack()
        {
            RemoteWebDriver.Navigate().Back();
        }

        /// <inheritdoc />
        /// <summary>
        /// Go forward one page in the history.
        /// </summary>
        public void GoForward()
        {
            RemoteWebDriver.Navigate().Forward();
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns a collection of live IWebElement objects from the current client page
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="T:Netlenium.Driver.ElementTypeNotSupportedException"></exception>
        public List<IWebElement> GetElements(ElementType elementType, string value)
        {
            
            switch (elementType)
            {
                case ElementType.Id:
                    return new List<IWebElement>(
                        RemoteWebDriver.FindElements(By.Id(value)).Select(webElement => new WebElement(webElement))
                    );
                
                case ElementType.Name:
                    return new List<IWebElement>(
                        RemoteWebDriver.FindElements(By.Name(value)).Select(webElement => new WebElement(webElement))
                    );
                
                case ElementType.TagName:
                    return new List<IWebElement>(
                        RemoteWebDriver.FindElements(By.TagName(value)).Select(webElement => new WebElement(webElement))
                    );
                
                case ElementType.ClassName:
                    return new List<IWebElement>(
                        RemoteWebDriver.FindElements(By.ClassName(value)).Select(webElement => new WebElement(webElement))
                    );
                
                case ElementType.CssSelector:
                    return new List<IWebElement>(
                        RemoteWebDriver.FindElements(By.CssSelector(value)).Select(webElement => new WebElement(webElement))
                    );
                
                default:
                    throw new ElementTypeNotSupportedException("This controller does not support the search with this Element Type");
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the first element found which is a live IWebElement object from the current client page
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="!:NotImplementedException"></exception>
        public IWebElement GetElement(ElementType elementType, string value)
        {
            switch (elementType)
            {
                case ElementType.Id:
                    return new WebElement(RemoteWebDriver.FindElement(By.Id(value)));;
                
                case ElementType.Name:
                    return new WebElement(RemoteWebDriver.FindElement(By.Name(value)));
                
                case ElementType.TagName:
                    return new WebElement(RemoteWebDriver.FindElement(By.TagName(value)));
                
                case ElementType.ClassName:
                    return new WebElement(RemoteWebDriver.FindElement(By.ClassName(value)));
                
                case ElementType.CssSelector:
                    return new WebElement(RemoteWebDriver.FindElement(By.CssSelector(value)));
                
                default:
                    throw new ElementTypeNotSupportedException("This controller does not support the search with this Element Type");
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///  loads a specified URL into a controlled client page
        /// </summary>
        /// <param name="url"></param>
        public void Navigate(string url)
        {
            RemoteWebDriver.Navigate().GoToUrl(url);
        }
    }
}
