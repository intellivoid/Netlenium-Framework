using Gecko;
using Gecko.WebIDL;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Netlenium.Driver.GeckoFXLib
{
    public class Element
    {
        /// <summary>
        /// Element Object
        /// </summary>
        private GeckoElement _Element;

        /// <summary>
        /// Driver Controller
        /// </summary>
        private Controller _DriverController;

        /// <summary>
        /// Constructs a GeckoElement
        /// </summary>
        /// <param name="Element"></param>
        /// <param name="DriverController"></param>
        public Element(GeckoElement Element, Controller DriverController)
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
                return this._Element.TextContent;
            }
            set
            {
                this._Element.TextContent = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not this element is displayed.
        /// </summary>
        public bool Visible
        {
            get
            {
                if(_Element != null)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// returns the value of a specified attribute on the element
        /// </summary>
        /// <param name="AttributeName"></param>
        /// <returns></returns>
        public string GetAttribute(string AttributeName)
        {
            return this._Element.GetAttribute(AttributeName);
        }

        /// <summary>
        /// Sets the value of an attribute on the specified element. If the attribute already exists, the value is updated; otherwise a new attribute is added with the specified name and value.
        /// </summary>
        /// <param name="AttributeName"></param>
        /// <param name="Value"></param>
        public void SetAttribute(string AttributeName, string Value)
        {
            this._Element.SetAttribute(AttributeName, Value);
        }

        /// <summary>
        /// Simulates a mouse click on an element.
        /// </summary>
        public void Click()
        {
            DomEventArgs ev = _DriverController._GeckoWebBrowser.Document.CreateEvent("MouseEvent");
            var webEvent = new Event(_Element.Window, ev.DomEvent as nsISupports);

            webEvent.InitEvent("mousedown", true, true);
            _Element.GetEventTarget().DispatchEvent(ev);

            webEvent.InitEvent("mouseup", true, true);
            _Element.GetEventTarget().DispatchEvent(ev);

            webEvent.InitEvent("click", true, true);
            _Element.GetEventTarget().DispatchEvent(ev);
        }

        /// <summary>
        /// Simulates typing into the event
        /// </summary>
        /// <param name="Text"></param>
        public void SendKeys(string Text)
        {
            GeckoHtmlElement GeckoHTMLElement = (GeckoHtmlElement)_Element;
            GeckoHTMLElement.Focus();

            foreach (char Key in Text)
            {
                try
                {
                    SendKeyEvent("keypress", Key.ToString(), false, false, false);
                }
                catch(Exception exception)
                {
                    Logging.WriteEntry(Types.LogType.Warning, "Netlenium.Driver.GeckoFXLib", $"Cannot simulate keypress for \"{Key}\", {exception.Message}");
                }
            }

        }

        /// <summary>
        /// Simulates a key press event
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="alt"></param>
        /// <param name="ctrl"></param>
        /// <param name="shift"></param>
        private void SendKeyEvent(string type, string key, bool alt, bool ctrl, bool shift)
        {
            // Escape for JS.
            key = key.Replace("\\", "\\\\");
            var instance = Xpcom.CreateInstance<nsITextInputProcessor>("@mozilla.org/text-input-processor;1");
            using (var context = new AutoJSContext(_DriverController._GeckoWebBrowser.Window))
            {
                var result = context.EvaluateScript(
                    $@"new KeyboardEvent('', {{ key: '{key}', code: '', keyCode: 0, ctrlKey : {(ctrl ? "true" : "false")}, shiftKey : {(shift ? "true" : "false")}, altKey : {(alt ? "true" : "false")} }});");
                instance.BeginInputTransaction((mozIDOMWindow)((GeckoWebBrowser)_DriverController._GeckoWebBrowser).Document.DefaultView.DomWindow, new KeyEventCallback());
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
        /// <param name="SearchType"></param>
        /// <param name="Input"></param>
        /// <returns></returns>
        public List<Element> GetElements(Types.SearchType SearchType, string Input)
        {
            List<Element> Elements = new List<Element>();

            switch (SearchType)
            {
                case Types.SearchType.ClassName:

                    foreach (GeckoElement FoundElement in _Element.GetElementsByTagName(Input))
                    {
                        Elements.Add(new Element(FoundElement, this._DriverController));
                    }

                    GeckoHtmlElement EMTL = (GeckoHtmlElement)_Element;
                    

                    return Elements;

                default:
                    throw new SearchTypeNotSupportedException();
            }
        }
    }
}
