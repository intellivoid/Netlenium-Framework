using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Netlenium.Driver.Chrome
{
    public class Element
    {
        private IWebElement _Element;

        /// <summary>
        /// 
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

        public string Text
        {
            get
            {
                return this._Element.Text;
            }
        }

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

        public void SetAttribute(string AttributeName, string Value)
        {
            _DriverController._Driver.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", this._Element, AttributeName, Value);
        }

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
