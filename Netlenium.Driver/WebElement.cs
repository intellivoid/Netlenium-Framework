using System;
using System.Collections.Generic;

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
        private Types.Driver TargetDriver;

        /// <summary>
        /// The chrome element object
        /// </summary>
        private Chrome.Element ChromeElement;

        /// <summary>
        /// The GeckoFXLib element object
        /// </summary>
        private GeckoFXLib.Element GeckoFXLibElement;

        /// <summary>
        /// Constructs the WebElement with a Chrome Element
        /// </summary>
        /// <param name="ChromeElement"></param>
        public WebElement(Chrome.Element ChromeElement)
        {
            this.TargetDriver = Types.Driver.Chrome;
            this.ChromeElement = ChromeElement;
        }
        
        /// <summary>
        /// Constructs the WebElement with a GeckoFXLib Element
        /// </summary>
        /// <param name="GeckoFXLibElement"></param>
        public WebElement(GeckoFXLib.Element GeckoFXLibElement)
        {
            this.TargetDriver = Types.Driver.GeckoLib;
            this.GeckoFXLibElement = GeckoFXLibElement;
        }

        /// <summary>
        /// Returns the text content of the elementc
        /// </summary>
        public string Text
        {
            get
            {
                switch(TargetDriver)
                {
                    case Types.Driver.Chrome:
                        return this.ChromeElement.Text;

                    case Types.Driver.GeckoLib:
                        return this.GeckoFXLibElement.Text;

                    default:
                        throw new ElementPropertyNotSupportedForDriver();
                }
            }
        }

        /// <summary>
        /// Returns the value of a specified attribute on the element. 
        /// </summary>
        /// <param name="AttributeName"></param>
        /// <returns></returns>
        public string GetAttribute(string AttributeName)
        {
            switch(TargetDriver)
            {
                case Types.Driver.Chrome:
                    return this.ChromeElement.GetAttribute(AttributeName);

                case Types.Driver.GeckoLib:
                    return this.GeckoFXLibElement.GetAttribute(AttributeName);

                default:
                    throw new ElementMethodNotSupportedForDriver();
            }
        }

        /// <summary>
        /// Sets the value of an attribute on the specified element. If the attribute already exists, the value is updated; otherwise a new attribute is added with the specified name and value.
        /// </summary>
        /// <param name="AttributeName"></param>
        /// <param name="Value"></param>
        public void SetAttribute(string AttributeName, string Value)
        {
            switch(TargetDriver)
            {
                case Types.Driver.Chrome:
                    this.ChromeElement.SetAttribute(AttributeName, Value);
                    break;

                case Types.Driver.GeckoLib:
                    this.GeckoFXLibElement.SetAttribute(AttributeName, Value);
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
                        this.ChromeElement.Click();
                        break;
                    }
                    catch(Exception exception)
                    {
                        Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver", $"There was an error while trying to click the element; {exception.Message}");
                        throw new InvokeFailureException();
                    }

                case Types.Driver.GeckoLib:
                    try
                    {
                        this.GeckoFXLibElement.Click();
                        break;
                    }
                    catch (Exception exception)
                    {
                        Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver", $"There was an error while trying to click the element; {exception.Message}");
                        throw new InvokeFailureException();
                    }

                default:
                    throw new MethodNotSupportedForDriver();
            }
        }

        public List<WebElement> GetElements(Types.SearchType SearchType, string Input)
        {
            List<WebElement> WebElements = new List<WebElement>();

            switch (TargetDriver)
            {
                case Types.Driver.Chrome:
                    List<Chrome.Element> ChromeResults = ChromeElement.GetElements(SearchType, Input);

                    foreach (Chrome.Element ChromeElement in ChromeResults)
                    {
                        WebElements.Add(new WebElement(ChromeElement));
                    }

                    return WebElements;

                case Types.Driver.GeckoLib:
                    List<GeckoFXLib.Element> GeckoFXLibResults = GeckoFXLibElement.GetElements(SearchType, Input);

                    foreach (GeckoFXLib.Element GeckoFXLibElement in GeckoFXLibResults)
                    {
                        WebElements.Add(new WebElement(GeckoFXLibElement));
                    }

                    return WebElements;

                default:
                    throw new MethodNotSupportedForDriver();
            }
        }
        
    }
}
