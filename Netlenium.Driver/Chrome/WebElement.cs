using System.Collections.Generic;
using System.Net;

namespace Netlenium.Driver.Chrome
{
    public class WebElement : IWebElement
    {

        private readonly WebDriver.IWebElement webElement;
        
        public string Text => webElement.Text;

        public WebElement(WebDriver.IWebElement webElement)
        {
            this.webElement = webElement;
        }
        
        public void Click()
        {
            webElement.Click();
        }

        public void SendKeys(string text)
        {
            webElement.SendKeys(text);
        }

        public void GetAttribute(string attributeName)
        {
            webElement.GetAttribute(attributeName);
        }

        public void SetAttribute(string attributeName, string value)
        {
            throw new System.NotImplementedException();
        }

        public List<IWebElement> GetElements(ElementType elementType, string value)
        {
            throw new System.NotImplementedException();
        }

        public IWebElement GetElement(ElementType elementType, string value)
        {
            throw new System.NotImplementedException();
        }
    }
}