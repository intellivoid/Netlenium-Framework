using System.Collections.Generic;

namespace Netlenium.Driver
{
    public interface IWebElement
    {
        /// <summary>
        /// The Text Contents within the Element
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Invokes a simulated Click Event on the Element
        /// </summary>
        void Click();

        /// <summary>
        /// Invokes a simulated series of Keystrokes on the Element
        /// </summary>
        /// <param name="text"></param>
        void SendKeys(string text);

        /// <summary>
        /// Gets an attribute value from the Element
        /// </summary>
        /// <param name="attributeName"></param>
        void GetAttribute(string attributeName);

        /// <summary>
        /// Sets an attribute value to the element
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        void SetAttribute(string attributeName, string value);

        
        /// <summary>
        /// Returns a collection of live IWebElement objects from this IWebElement
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        List<IWebElement> GetElements(ElementType elementType, string value);
        
        /// <summary>
        /// Returns the first element found which is a live IWebElement object from this IWebElement
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IWebElement GetElement(ElementType elementType, string value);
    }
}