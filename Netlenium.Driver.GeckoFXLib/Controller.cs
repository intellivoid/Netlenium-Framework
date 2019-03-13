﻿using Gecko;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
        public GeckoWebBrowser _GeckoWebBrowser;

        /// <summary>
        /// The web view UI, if Hide Paramerter is set to false in Initialize() this won't be shown.
        /// </summary>
        private Forms.WebView _WebView;

        /// <summary>
        /// Indication if the Document is ready or not
        /// </summary>
        public bool DocumentReady;
        
        /// <summary>
        /// The current Driver Installation Details
        /// </summary>
        private DriverInstallationDetails DriverInstallation;

        private DriverConfiguration DriverConfiguration;

        /// <summary>
        /// Constructs the controller
        /// </summary>
        public Controller(DriverConfiguration DriverConfiguration, DriverInstallationDetails DriverInstalation)
        {
            this.DriverInstallation = DriverInstalation;
            this.DriverConfiguration = DriverConfiguration;
        }

        /// <summary>
        /// Initializes the GeckoFX Web Client
        /// </summary>
        public void Initialize()
        {
            Xpcom.Initialize(DriverInstallation.DriverPath);
            this._WebView = new Forms.WebView();
            this._GeckoWebBrowser = this._WebView.GeckoWebBrowser;
            if(DriverConfiguration.Headless == false)
            {
                this._WebView.Show();
            }
        }

        /// <summary>
        /// Quits the driver and unreleases used resources
        /// </summary>
        public void Quit()
        {
            _WebView.Close();
            _GeckoWebBrowser.Dispose();
        }

        /// <summary>
        /// the current title of the document
        /// </summary>
        public string DocumentTitle
        {
            get
            {
                return _GeckoWebBrowser.DocumentTitle;
            }
        }

        /// <summary>
        /// The current URL
        /// </summary>
        public string URL
        {
            get
            {
                return _GeckoWebBrowser.Url.ToString();
            }
        }

        /// <summary>
        /// Executes Javascript Code, throws an exception if the code failed to be executed
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public string ExecuteJS(string Code)
        {
            string result = string.Empty;
            AutoJSContext context;
            context = new AutoJSContext(_GeckoWebBrowser.Window);
            context.EvaluateScript(Code, (nsISupports)_GeckoWebBrowser.Window.DomWindow, out result);
            return result;
        }
        
        /// <summary>
        /// Navigates to the given URL
        /// </summary>
        /// <param name="URL"></param>
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

        /// <summary>
        /// Fetches elements in the current loaded document
        /// </summary>
        /// <param name="SearchType">ClassName/Name/TagName/Id</param>
        /// <param name="Input"></param>
        /// <returns></returns>
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
