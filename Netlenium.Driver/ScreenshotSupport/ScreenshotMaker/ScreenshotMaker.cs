﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ImageMagick;
using Netlenium.Driver.WebDriver;
using Netlenium.Driver.WebDriver.Support.Extensions;
using Netlenium.Driver.ScreenshotSupport.Helpers;
using Netlenium.Driver.ScreenshotSupport.Interfaces;

// ReSharper disable InconsistentNaming

namespace Netlenium.Driver.ScreenshotSupport.ScreenshotMaker
{
    /// <summary>
    ///     <para>The default core strategy.</para>
    ///     <para>
    ///         This class takes a screenshot and has additional settings, such as hiding scrollbars, hiding elements when
    ///         taking a screenshot, and others.
    ///     </para>
    /// </summary>
    public class ScreenshotMaker : IScreenshotStrategy
    {
        [CLSCompliant(false)]
        public IMagickImage MakeScreenshot(IWebDriver driver)
        {
            SetDriver(driver).HideElements().HideScrollBars();
            var screenshot = driver.TakeScreenshot();
            var ms = new MemoryStream(screenshot.AsByteArray);
            return new MagickImage(ms);
        }

        /// <summary>
        ///     <para>Sets which elements will be hidden from the DOM before taking the screenshot.</para>
        ///     <para>Elements will be hidden if they are in the viewport.</para>
        ///     <para>After taking the screenshot, the hidden elements will become visible again.</para>
        ///     <para>Be careful, if any of the Bys finds more than one element, they all will be hidden.</para>
        /// </summary>
        /// <param name="Bys">
        ///     Bys collection, how to find the elements that need to be hidden from the DOM until screenshot was
        ///     taken.
        /// </param>
        /// <returns></returns>
        public ScreenshotMaker SetElementsToHide(IEnumerable<By> Bys)
        {
            _elementsToRemoveBys = Bys.ToList();
            return this;
        }

        /// <summary>
        ///     <para>While taking a screenshot, the scrollbars will be hidden.</para>
        ///     <para>After taking the screenshot, the scrollbars will be visible again.</para>
        /// </summary>
        public ScreenshotMaker RemoveScrollBarsWhileShooting()
        {
            _scrollBarsNeedToBeHidden = true;
            return this;
        }

        #region Private fields

        private List<By> _elementsToRemoveBys;
        private List<Netlenium.Driver.WebDriver.IWebElement> _hiddenElements;
        private Dictionary<Netlenium.Driver.WebDriver.IWebElement, string> _scrollBarsOverflows;
        private IWebDriver _driver;
        private bool _scrollBarsNeedToBeHidden;

        #endregion

        #region Override

        #endregion

        #region Privates

        private ScreenshotMaker SetDriver(IWebDriver driver)
        {
            if (_driver != null && _driver.GetHashCode() == driver.GetHashCode()) return this;
            _driver = driver;
            return this;
        }

        private ScreenshotMaker HideElements()
        {
            if (_elementsToRemoveBys != null && _elementsToRemoveBys.Count > 0)
                _elementsToRemoveBys.ForEach(by =>
                {
                    var elements = _driver.GetElementsFromDOM(by).ToList();
                    if (elements.Count > 0)
                        elements.ForEach(element =>
                        {
                            var visibleState = _driver.IsElementInViewPort(element);
                            if (visibleState)
                            {
                                if (_hiddenElements == null) _hiddenElements = new List<Netlenium.Driver.WebDriver.IWebElement>();
                                _driver.SetElementHidden(element);
                                if (!_hiddenElements.Contains(element)) _hiddenElements.Add(element);
                            }
                        });
                });

            return this;
        }

        private ScreenshotMaker RestoreHiddenElements()
        {
            _hiddenElements?.ForEach(_driver.SetElementVisible);
            _hiddenElements?.Clear();
            return this;
        }

        private ScreenshotMaker HideScrollBars()
        {
            if (_scrollBarsNeedToBeHidden)
            {
                if (_scrollBarsOverflows == null) _scrollBarsOverflows = new Dictionary<Netlenium.Driver.WebDriver.IWebElement, string>();

                var eles = _driver.GetAllElementsWithScrollbars();
                eles.ForEach(ele =>
                {
                    if (!_scrollBarsOverflows.ContainsKey(ele))
                    {
                        var overflow =
                            _driver.ExecuteJavaScript<string>("return $(arguments[0]).css(\"overflow\");", ele);
                        _scrollBarsOverflows.Add(ele, overflow);
                        _driver.HideScrollBar(ele);
                    }
                });
            }

            return this;
        }

        private ScreenshotMaker RestoreScrollBars()
        {
            if (_scrollBarsNeedToBeHidden)
                if (_scrollBarsOverflows != null && _scrollBarsOverflows.Count != 0)
                {
                    foreach (var pair in _scrollBarsOverflows) _driver.ShowScrollBar(pair.Key, pair.Value);
                    _scrollBarsOverflows.Clear();
                }

            return this;
        }

        internal ScreenshotMaker RestoreAll()
        {
            RestoreHiddenElements().RestoreScrollBars();
            return this;
        }

        #endregion
    }
}