using System;
using System.Collections.Generic;
using System.Linq;

namespace Netlenium.Driver
{
    /// <summary>
    /// WebElement
    /// </summary>
    public class WebElement
    {
        /// <summary>
        /// The target driver that this element uses
        /// </summary>
        private Types.Driver TargetDriver { get; }

        /// <summary>
        /// The chrome element object
        /// </summary>
        private Chrome.Element ChromeElement { get; }


        /// <summary>
        /// Constructs the WebElement with a Chrome Element
        /// </summary>
        /// <param name="chromeElement"></param>
        public WebElement(Chrome.Element chromeElement)
        {
            TargetDriver = Types.Driver.Chrome;
            ChromeElement = chromeElement;
        }

        /// <summary>
        /// The Text-Contents from the Element
        /// </summary>
        public string Text
        {
            get
            {
                switch (TargetDriver)
                {
                    case Types.Driver.Chrome:
                        return ChromeElement.Text;
                        
                    default:
                        throw new ElementPropertyNotSupportedForDriver();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not this element is displayed.
        /// </summary>
        public bool Visible
        {
            get
            {
                switch(TargetDriver)
                {
                    case Types.Driver.Chrome:
                        return ChromeElement.Visible;
                        
                    default:
                        throw new ElementPropertyNotSupportedForDriver();
                }
            }
        }

        /// <summary>
        /// Returns the value of a specified attribute on the element. 
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public string GetAttribute(string attributeName)
        {
            switch(TargetDriver)
            {
                case Types.Driver.Chrome:
                    return ChromeElement.GetAttribute(attributeName);

                default:
                    throw new ElementMethodNotSupportedForDriver();
            }
        }

        /// <summary>
        /// Sets the value of an attribute on the specified element. If the attribute already exists, the value is updated; otherwise a new attribute is added with the specified name and value.
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        public void SetAttribute(string attributeName, string value)
        {
            switch(TargetDriver)
            {
                case Types.Driver.Chrome:
                    ChromeElement.SetAttribute(attributeName, value);
                    break;
                   
                default:
                    throw new ElementMethodNotSupportedForDriver();
            }
        }

        /// <summary>
        /// Simulates a mouse click on an element
        /// </summary>
        public void Click()
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Invoking Click Event on Element");

            switch(TargetDriver)
            {
                case Types.Driver.Chrome:
                    try
                    {
                        ChromeElement.Click();
                        break;
                    }
                    catch(Exception exception)
                    {
                        Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver", $"There was an error while trying to click the element; {exception.Message}");
                        throw new InvokeFailureException(exception.Message);
                    }

                default:
                    throw new ElementMethodNotSupportedForDriver();
            }
        }

        /// <summary>
        /// Simulates typing into the event
        /// </summary>
        /// <param name="keys"></param>
        public void SendKeys(string keys)
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Simulating typing to element");
            switch(TargetDriver)
            {
                case Types.Driver.Chrome:
                    ChromeElement.SendKeys(keys);
                    break;

                default:
                    Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver", "The method SendKeys() on this element is not supported for the given Driver.");
                    throw new ElementMethodNotSupportedForDriver();

            }

            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver", "Success");
        }

        /// <summary>
        /// Returns a collection of all elements in the document with the specified search method
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<WebElement> GetElements(Types.SearchType searchType, string input)
        {
            var webElements = new List<WebElement>();

            switch (TargetDriver)
            {
                case Types.Driver.Chrome:
                    var chromeResults = ChromeElement.GetElements(searchType, input);
                    webElements.AddRange(chromeResults.Select(chromeElement => new WebElement(chromeElement)));

                    return webElements;

                default:
                    throw new MethodNotSupportedForDriver();
            }
        }

        /// <summary>
        /// <returns></returns>
        /// Returns the first element of all elements in the document with the specified search method
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="input"></param>
        public WebElement GetElement(Types.SearchType searchType, string input)
        {
            switch(TargetDriver)
            {
                case Types.Driver.Chrome:
                    var chromeResults = ChromeElement.GetElements(searchType, input);

                    if(chromeResults.Count > 1)
                    {
                        return new WebElement(chromeResults[0]);
                    }

                    throw new NoElementsFoundException();

                default:
                    throw new MethodNotSupportedForDriver();
            }
        }
        
    }
}
