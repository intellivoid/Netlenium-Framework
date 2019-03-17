using Gecko;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Netlenium.Types;

namespace Netlenium.Driver.GeckoFXLib
{
    /// <summary>
    /// GeckoFXLib Controller Class
    /// </summary>
    public class Controller
    {
        
        /// <summary>
        /// The private GeckoWebBrowser Control
        /// </summary>
        public GeckoWebBrowser GeckoWebBrowser;

        /// <summary>
        /// The web view UI, if Hide Paramerter is set to false in Initialize() this won't be shown.
        /// </summary>
        private Forms.WebView _webView;

        /// <summary>
        /// Indication if the Document is ready or not
        /// </summary>
        public bool DocumentReady;
        
        /// <summary>
        /// The current Driver Installation Details
        /// </summary>
        private readonly DriverInstallationDetails _driverInstallation;

        /// <summary>
        /// The Driver Configuration Details
        /// </summary>
        private readonly DriverConfiguration _driverConfiguration;

        /// <summary>
        /// Constructs the controller
        /// </summary>
        public Controller(DriverConfiguration driverConfiguration, DriverInstallationDetails driverInstalation)
        {
            _driverInstallation = driverInstalation;
            _driverConfiguration = driverConfiguration;
        }

        /// <summary>
        /// Initializes the GeckoFX Web Client
        /// </summary>
        public void Initialize()
        {
            Xpcom.Initialize(_driverInstallation.DriverPath);
            _webView = new Forms.WebView();
            GeckoWebBrowser = _webView.GeckoWebBrowser;
            if(_driverConfiguration.Headless == false)
            {
                _webView.Show();
            }
        }

        /// <summary>
        /// Quits the driver and unreleases used resources
        /// </summary>
        public void Quit()
        {
            _webView.Close();
            GeckoWebBrowser.Dispose();
        }

        /// <summary>
        /// the current title of the document
        /// </summary>
        public string DocumentTitle => GeckoWebBrowser.DocumentTitle;

        /// <summary>
        /// The current URL
        /// </summary>
        public string Url => GeckoWebBrowser.Url.ToString();

        /// <summary>
        /// Executes Javascript Code, throws an exception if the code failed to be executed
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string ExecuteJs(string code)
        {
            string result;
            var context = new AutoJSContext(GeckoWebBrowser.Window);
            context.EvaluateScript(code, GeckoWebBrowser.Window.DomWindow, out result);
            return result;
        }
        
        /// <summary>
        /// Navigates to the given URL
        /// </summary>
        /// <param name="url"></param>
        public void Navigate(string url)
        {
            _webView.DocumentReady = false;
            GeckoWebBrowser.Navigate(url);

            while (true)
            {
                Application.DoEvents();
                
                if (_webView.DocumentReady != true) continue;
                
                if (GeckoWebBrowser.IsBusy == false)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Moves back a single entry in the browser's history
        /// </summary>
        public void GoBack()
        {
            GeckoWebBrowser.GoBack();
        }

        /// <summary>
        /// Moves a single "item" forward in the browser's history.
        /// </summary>
        public void GoForward()
        {
            GeckoWebBrowser.GoForward();
        }

        /// <summary>
        /// Fetches elements in the current loaded document
        /// </summary>
        /// <param name="searchType">ClassName/Name/TagName/Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<Element> GetElements(SearchType searchType, string input)
        {
            var elements = new List<Element>();

            switch(searchType)
            {
                case SearchType.ClassName:

                    elements.AddRange(from GeckoElement foundElement in 
                        GeckoWebBrowser.Document.GetElementsByClassName(input) select new Element(foundElement, this));

                    return elements;

                case SearchType.Name:

                    elements.AddRange(from GeckoElement foundElement in 
                        GeckoWebBrowser.Document.GetElementsByName(input) select new Element(foundElement, this));

                    return elements;

                case SearchType.TagName:

                    elements.AddRange(from GeckoElement foundElement in 
                        GeckoWebBrowser.Document.GetElementsByTagName(input) select new Element(foundElement, this));

                    return elements;

                case SearchType.Id:

                    elements.Add(new Element(GeckoWebBrowser.Document.GetElementById(input), this));
                    return elements;

                case SearchType.CssSelector:
                    throw new SearchTypeNotSupportedException();
                    
                default:
                    throw new SearchTypeNotSupportedException();
            }
        }
        
        /// <summary>
        /// Disposes the controller
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
