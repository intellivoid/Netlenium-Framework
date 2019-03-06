using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Netlenium.Driver.Chrome
{
    public class Element
    {
        /// <summary>
        /// The IWebElement from Chrome
        /// </summary>
        private IWebElement _Element;

        /// <summary>
        /// Driver Controller
        /// </summary>
        private Controller _DriverController;

        /// <summary>
        /// Constructs a selenium-type element
        /// </summary>
        /// <param name="Element"></param>
        /// <param name="DriverController"></param>
        public Element(IWebElement Element, Controller DriverController)
        {
            this._Element = Element;
            this._DriverController = DriverController;
        }

        /// <summary>
        /// The Text-Contents from the Element
        /// </summary>
        public string Text
        {
            get
            {
                return this._Element.Text;
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not this element is displayed.
        /// </summary>
        public bool Visible
        {
            get
            {
                return this._Element.Displayed;
            }
        }

        /// <summary>
        /// returns the value of a specified attribute on the element
        /// </summary>
        /// <param name="AttributeName"></param>
        /// <returns></returns>
        public string GetAttribute(string AttributeName)
        {
            try
            {
                return this._Element.GetAttribute(AttributeName);
            }
            catch(Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Sets the value of an attribute on the specified element. If the attribute already exists, the value is updated; otherwise a new attribute is added with the specified name and value.
        /// </summary>
        /// <param name="AttributeName"></param>
        /// <param name="Value"></param>
        public void SetAttribute(string AttributeName, string Value)
        {
            _DriverController._Driver.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", this._Element, AttributeName, Value);
        }

        /// <summary>
        /// simulates a mouse click on an element.
        /// </summary>
        public void Click()
        {
            _DriverController.MoveTo(_Element);
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver.Chrome", $"Clicking Element \"{_Element.ToString()}\"");
            _Element.Click();
        }

        /// <summary>
        /// Simulates typing into the event
        /// </summary>
        /// <param name="Text"></param>
        public void SendKeys(string Text)
        {
            _DriverController.MoveTo(_Element);

            try
            {
                _Element.SendKeys(Text);
            }
            catch (ElementNotVisibleException exception)
            {
                Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver.Chrome", $"Cannot send keys to elemet \"{_Element.ToString()}\" because the element is not visible");
                throw exception;
            }
            catch (ElementNotInteractableException exception)
            {
                Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver.Chrome", $"Cannot send keys to elemet \"{_Element.ToString()}\" because the element is not interactable");
                throw exception;
            }
            catch(Exception exception)
            {
                Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver.Chrome", $"Cannot send keys to elemet \"{_Element.ToString()}\", {exception.Message}");
                throw exception;
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

                    foreach (IWebElement FoundElement in this._Element.FindElements(By.ClassName(Input)))
                    {
                        Elements.Add(new Element(FoundElement, _DriverController));
                    }

                    return Elements;

                case Types.SearchType.CssSelector:

                    foreach (IWebElement FoundElement in this._Element.FindElements(By.CssSelector(Input)))
                    {
                        Elements.Add(new Element(FoundElement, _DriverController));
                    }

                    return Elements;

                case Types.SearchType.Id:
                    foreach (IWebElement FoundElement in this._Element.FindElements(By.Id(Input)))
                    {
                        Elements.Add(new Element(FoundElement, _DriverController));
                    }

                    return Elements;

                case Types.SearchType.TagName:
                    foreach (IWebElement FoundElement in this._Element.FindElements(By.TagName(Input)))
                    {
                        Elements.Add(new Element(FoundElement, _DriverController));
                    }

                    return Elements;

                case Types.SearchType.Name:
                    foreach (IWebElement FoundElement in this._Element.FindElements(By.Name(Input)))
                    {
                        Elements.Add(new Element(FoundElement, _DriverController));
                    }

                    return Elements;

                default:
                    throw new SearchTypeNotSupportedException();
            }
        }
    }
}
