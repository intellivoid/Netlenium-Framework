using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Netlenium.Driver.Chrome
{
    /// <summary>
    /// Chrome (iWebElement) Element Class
    /// </summary>
    public class Element
    {
        /// <summary>
        /// The IWebElement from Chrome
        /// </summary>
        private readonly IWebElement _element;

        /// <summary>
        /// Driver Controller
        /// </summary>
        private readonly Controller _driverController;

        /// <summary>
        /// Constructs a selenium-type element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="driverController"></param>
        public Element(IWebElement element, Controller driverController)
        {
            _element = element;
            _driverController = driverController;
        }

        /// <summary>
        /// The Text-Contents from the Element
        /// </summary>
        public string Text
        {
            get
            {
                _driverController.MoveTo(_element);
                return _element.Text;
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not this element is displayed.
        /// </summary>
        public bool Visible => _element.Displayed;

        /// <summary>
        /// returns the value of a specified attribute on the element
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public string GetAttribute(string attributeName)
        {
            try
            {
                return _element.GetAttribute(attributeName);
            }
            catch(Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Sets the value of an attribute on the specified element. If the attribute already exists, the value is updated; otherwise a new attribute is added with the specified name and value.
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        public void SetAttribute(string attributeName, string value)
        {
            _driverController.RemoteDriver.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", _element, attributeName, value);
        }

        /// <summary>
        /// simulates a mouse click on an element.
        /// </summary>
        public void Click()
        {
            _driverController.MoveTo(_element);
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver.Chrome", $"Clicking Element \"{_element}\"");
            _element.Click();
        }

        /// <summary>
        /// Simulates typing into the event
        /// </summary>
        /// <param name="text"></param>
        public void SendKeys(string text)
        {
            _driverController.MoveTo(_element);

            try
            {
                _element.SendKeys(text);
            }
            catch (ElementNotVisibleException)
            {
                Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver.Chrome", $"Cannot send keys to elemet \"{_element}\" because the element is not visible");
                throw;
            }
            catch (ElementNotInteractableException)
            {
                Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver.Chrome", $"Cannot send keys to elemet \"{_element}\" because the element is not interactable");
                throw;
            }
            catch(Exception exception)
            {
                Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver.Chrome", $"Cannot send keys to elemet \"{_element}\", {exception.Message}");
                throw;
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

                    elements.AddRange(_element.FindElements(By.ClassName(input)).Select(
                        foundElement => new Element(foundElement, _driverController))
                    );

                    return elements;

                case Types.SearchType.CssSelector:

                    elements.AddRange(_element.FindElements(By.CssSelector(input)).Select(
                        foundElement => new Element(foundElement, _driverController))
                    );

                    return elements;

                case Types.SearchType.Id:
                    
                    elements.AddRange(_element.FindElements(By.Id(input)).Select(
                        foundElement => new Element(foundElement, _driverController))
                    );

                    return elements;

                case Types.SearchType.TagName:

                    elements.AddRange(_element.FindElements(By.TagName(input)).Select(
                        foundElement => new Element(foundElement, _driverController))
                    );

                    return elements;

                case Types.SearchType.Name:

                    elements.AddRange(_element.FindElements(By.Name(input)).Select(
                        foundElement => new Element(foundElement, _driverController))
                    );

                    return elements;

                default:
                    throw new SearchTypeNotSupportedException();
            }
        }
    }
}
