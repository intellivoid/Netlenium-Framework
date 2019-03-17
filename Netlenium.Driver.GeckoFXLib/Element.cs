using Gecko;
using Gecko.WebIDL;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using Netlenium.Types;

namespace Netlenium.Driver.GeckoFXLib
{
    /// <summary>
    /// GeckoFX (GeckoElement) Element Class
    /// </summary>
    public class Element
    {
        /// <summary>
        /// Element Object
        /// </summary>
        private readonly GeckoElement _element;

        /// <summary>
        /// Driver Controller
        /// </summary>
        private readonly Controller _driverController;

        /// <summary>
        /// Constructs a GeckoElement
        /// </summary>
        /// <param name="element"></param>
        /// <param name="driverController"></param>
        public Element(GeckoElement element, Controller driverController)
        {
            _element = element;
            _driverController = driverController;
        }

        /// <summary>
        /// The Text-Contents from the Element
        /// </summary>
        public string Text
        {
            get => _element.TextContent;
            set => _element.TextContent = value;
        }

        /// <summary>
        /// Gets a value indicating whether or not this element is displayed.
        /// </summary>
        public bool Visible => _element != null;

        /// <summary>
        /// returns the value of a specified attribute on the element
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public string GetAttribute(string attributeName)
        {
            return _element.GetAttribute(attributeName);
        }

        /// <summary>
        /// Sets the value of an attribute on the specified element. If the attribute already exists, the value is updated; otherwise a new attribute is added with the specified name and value.
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        public void SetAttribute(string attributeName, string value)
        {
            _element.SetAttribute(attributeName, value);
        }

        /// <summary>
        /// Simulates a mouse click on an element.
        /// </summary>
        public void Click()
        {
            var ev = _driverController.GeckoWebBrowser.Document.CreateEvent("MouseEvent");
            // ReSharper disable once SuspiciousTypeConversion.Global
            var webEvent = new Event(_element.Window, (nsISupports) ev.DomEvent);

            webEvent.InitEvent("mousedown", true, true);
            _element.GetEventTarget().DispatchEvent(ev);

            webEvent.InitEvent("mouseup", true, true);
            _element.GetEventTarget().DispatchEvent(ev);

            webEvent.InitEvent("click", true, true);
            _element.GetEventTarget().DispatchEvent(ev);
        }

        /// <summary>
        /// Simulates typing into the event
        /// </summary>
        /// <param name="text"></param>
        public void SendKeys(string text)
        {
            var geckoHtmlElement = (GeckoHtmlElement)_element;
            geckoHtmlElement.Focus();

            foreach (var key in text)
            {
                try
                {
                    SendKeyEvent(key.ToString(), false, false, false);
                }
                catch(Exception exception)
                {
                    Logging.WriteEntry(LogType.Warning, "Netlenium.Driver.GeckoFXLib", $"Cannot simulate keypress for \"{key}\", {exception.Message}");
                }
            }

        }

        /// <summary>
        /// Simulates a key press event
        /// </summary>
        /// <param name="key"></param>
        /// <param name="alt"></param>
        /// <param name="ctrl"></param>
        /// <param name="shift"></param>
        private void SendKeyEvent(string key, bool alt, bool ctrl, bool shift)
        {
            // Escape for JS.
            key = key.Replace("\\", "\\\\");
            var instance = Xpcom.CreateInstance<nsITextInputProcessor>("@mozilla.org/text-input-processor;1");
            using (var context = new AutoJSContext(_driverController.GeckoWebBrowser.Window))
            {
                var result = context.EvaluateScript(
                    $@"new KeyboardEvent('', {{ key: '{key}', code: '', keyCode: 0, ctrlKey : {(ctrl ? "true" : "false")}, shiftKey : {(shift ? "true" : "false")}, altKey : {(alt ? "true" : "false")} }});");
                // ReSharper disable once SuspiciousTypeConversion.Global
                instance.BeginInputTransaction((mozIDOMWindow) _driverController.GeckoWebBrowser.Document.DefaultView.DomWindow, new KeyEventCallback());
                instance.Keydown((nsIDOMEvent)result.ToObject(), 0, 1);
                instance.Keyup((nsIDOMEvent)result.ToObject(), 0, 1);
            }

            Marshal.ReleaseComObject(instance);
        }

        /// <summary>
        /// Callback Processor for GeckoFX LibEventss
        /// </summary>
        private class KeyEventCallback : nsITextInputProcessorCallback
        {
            public bool OnNotify(nsITextInputProcessor aTextInputProcessor, nsITextInputProcessorNotification aNotification)
            {
                Debug.Print(aNotification.ToString());
                return true;
            }
        }

        /// <summary>
        /// Returns a live ElementCollection of elements with the given search type name and input
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<Element> GetElements(SearchType searchType, string input)
        {
            var elements = new List<Element>();

            switch (searchType)
            {
                case SearchType.ClassName:

                    elements.AddRange(_element.GetElementsByTagName(input).Select(
                        foundElement => new Element(foundElement, _driverController))
                    );
                    
                    return elements;

                case SearchType.Id:
                    throw new SearchTypeNotSupportedException();
                    
                case SearchType.CssSelector:
                    throw new SearchTypeNotSupportedException();
                    
                case SearchType.TagName:
                    throw new SearchTypeNotSupportedException();
                    
                case SearchType.Name:
                    throw new SearchTypeNotSupportedException();
                    
                default:
                    throw new SearchTypeNotSupportedException();
            }
        }
    }
}
