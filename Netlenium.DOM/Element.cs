using OpenQA.Selenium;

namespace Netlenium.DOM
{
    public class Element
    {
        private IWebElement _SeleniumElement;

        private Types.Driver SelectedDriver;

        /// <summary>
        /// Constructs a Selenium Element
        /// </summary>
        /// <param name="Element"></param>
        /// <param name="SelectedDriver"></param>
        public Element(IWebElement Element, Types.Driver SelectedDriver)
        {
            this.SelectedDriver = SelectedDriver;
            this._SeleniumElement = Element;
        }

        
    }
}
