using Gecko;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Netlenium.Driver.GeckoFXLib
{
    public class Controller
    {
        private GeckoWebBrowser _GeckoWebBrowser;

        private Forms.WebView _WebView;

        public bool DocumentReady;

        public Controller()
        {
            Xpcom.Initialize("Firefox");
        }

        public void Initialize(bool Hide = true)
        {
            this._WebView = new Forms.WebView();
            this._GeckoWebBrowser = this._WebView.GeckoWebBrowser;
            //this._WebView.Show();
        }

        public string ExecuteJS(string Code)
        {
            string result = string.Empty;
            AutoJSContext context;
            context = new AutoJSContext(_GeckoWebBrowser.Window);
            context.EvaluateScript(Code, (nsISupports)_GeckoWebBrowser.Window.DomWindow, out result);
            return result;
        }

        public void Navigate(string URL)
        {
            this._WebView.DocumentReady = false;
            this._GeckoWebBrowser.Navigate(URL);

            while (true)
            {
                Application.DoEvents();
                if(this._WebView.DocumentReady == true)
                {
                    if (this._GeckoWebBrowser.IsBusy == false)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Moves back a single entry in the browser's history
        /// </summary>
        public void GoBack()
        {
            this._GeckoWebBrowser.GoBack();
        }

        /// <summary>
        /// Moves a single "item" forward in the browser's history.
        /// </summary>
        public void GoForward()
        {
            this._GeckoWebBrowser.GoForward();
        }

        public List<Element> GetElements(Types.SearchType SearchType, string Input)
        {
            List<Element> Elements = new List<Element>();

            switch(SearchType)
            {
                case Types.SearchType.ClassName:

                    foreach (GeckoElement FoundElement in _GeckoWebBrowser.Document.GetElementsByClassName(Input))
                    {
                        Elements.Add(new Element(FoundElement, this));
                    }

                    return Elements;

                case Types.SearchType.Name:

                    foreach (GeckoElement FoundElement in _GeckoWebBrowser.Document.GetElementsByName(Input))
                    {
                        Elements.Add(new Element(FoundElement, this));
                    }

                    return Elements;

                case Types.SearchType.TagName:

                    foreach (GeckoElement FoundElement in _GeckoWebBrowser.Document.GetElementsByTagName(Input))
                    {
                        Elements.Add(new Element(FoundElement, this));
                    }

                    return Elements;

                case Types.SearchType.Id:

                    Elements.Add(new Element(_GeckoWebBrowser.Document.GetElementById(Input), this));
                    return Elements;

                default:
                    throw new SearchTypeNotSupportedException();
            }
        }
    }
}
