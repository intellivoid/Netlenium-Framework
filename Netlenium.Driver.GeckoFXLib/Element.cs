using Gecko;
using Gecko.WebIDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gecko.WebIDL;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Gecko.DOM;
using System.Diagnostics;

namespace Netlenium.Driver.GeckoFXLib
{
    public class Element
    {
        private GeckoElement _Element;

        private Controller _DriverController;

        public Element(GeckoElement Element, Controller DriverController)
        {
            this._Element = Element;
            this._DriverController = DriverController;
        }

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

        public string GetAttribute(string AttributeName)
        {
            return this._Element.GetAttribute(AttributeName);
        }

        public void SetAttribute(string AttributeName, string Value)
        {
            this._Element.SetAttribute(AttributeName, Value);
        }

        /// <summary>
        /// Simulates a mouse click on an element.
        /// </summary>
        public void Click()
        {
            GeckoHtmlElement GeckoHTMLElement = (GeckoHtmlElement)_Element;

            GeckoHTMLElement.Click();
            //DomEventArgs ev = _DriverController._GeckoWebBrowser.Document.CreateEvent("MouseEvent");
            //var webEvent = new Event(_DriverController._GeckoWebBrowser.Window.DomWindow, ev.DomEvent as nsISupports);

            //webEvent.InitEvent("mousedown", true, true);
            //_Element.GetEventTarget().DispatchEvent(ev);

            //webEvent.InitEvent("mouseup", true, true);
            //_Element.GetEventTarget().DispatchEvent(ev);

            //webEvent.InitEvent("click", true, true);
            //_Element.GetEventTarget().DispatchEvent(ev);
        }

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

        private void SendKeyEvent(string type, string key, bool alt, bool ctrl, bool shift)
        {
            var WebBrowser = _DriverController._GeckoWebBrowser;
            // Escape for JS.
            key = key.Replace("\\", "\\\\");
            var instance = Xpcom.CreateInstance<nsITextInputProcessor>("@mozilla.org/text-input-processor;1");
            using (var context = new AutoJSContext(_DriverController._GeckoWebBrowser.Window))
            {
                var result = context.EvaluateScript(
                    $@"new KeyboardEvent('', {{ key: '{key}', code: '', keyCode: 0, ctrlKey : {(ctrl ? "true" : "false")}, shiftKey : {(shift ? "true" : "false")}, altKey : {(alt ? "true" : "false")} }});");
                instance.BeginInputTransaction((mozIDOMWindow)((GeckoWebBrowser)WebBrowser).Document.DefaultView.DomWindow, new KeyEventCallback());
                instance.Keydown((nsIDOMEvent)result.ToObject(), 0, 1);
                instance.Keyup((nsIDOMEvent)result.ToObject(), 0, 1);
            }

            Marshal.ReleaseComObject(instance);
        }

        public class KeyEventCallback : nsITextInputProcessorCallback
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
                    

                    return Elements;

                default:
                    throw new SearchTypeNotSupportedException();
            }
        }
    }
}
