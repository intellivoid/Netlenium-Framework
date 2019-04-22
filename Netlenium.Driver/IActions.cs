using System.Collections.Generic;
using System.Drawing;

namespace Netlenium.Driver
{
    /// <summary>
    /// Actions that can be invoked in the Browser's client page
    /// </summary>
    public interface IActions
    {
        /// <summary>
        /// loads a specified URL into a controlled client page
        /// </summary>
        /// <param name="url"></param>
        void Navigate(string url);

        /// <summary>
        /// Go back one page in the history.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Go forward one page in the history.
        /// </summary>
        void GoForward();
        
        /// <summary>
        /// Refreshes the client page
        /// </summary>
        void Refresh();

        /// <summary>
        /// Captures an image of the client page
        /// </summary>
        /// <returns></returns>
        Image Capture();

        /// <summary>
        /// Returns a collection of live IWebElement objects from the current client page
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        List<IWebElement> GetElements(ElementType elementType, string value);
        
        /// <summary>
        /// Returns the first element found which is a live IWebElement object from the current client page
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IWebElement GetElement(ElementType elementType, string value);

    }
}
