using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Netlenium.Driver.WebDriver;
using Netlenium.Driver.WebDriver.Support.Extensions;

// ReSharper disable InconsistentNaming

namespace Netlenium.Driver.ScreenshotSupport.Helpers
{
    public static class JQueryHelper
    {
        internal static void CheckJQueryOnPage(this IWebDriver driver)
        {
            var script = SHResources.SetJQuery;
            try
            {
                _ = driver.ExecuteJavaScript<long>("return $(document).outerHeight()");
            }
            catch (WebDriverException)
            {
                driver.ExecuteJavaScript(script);
                var sw = new Stopwatch();
                sw.Start();
                do
                {
                    try
                    {
                        _ = driver.ExecuteJavaScript<long>("return $(document).outerHeight()");
                        return;
                    }
                    catch (WebDriverException) 
                    {
                        Thread.Sleep(10);
                    }
                    finally
                    {
                        sw.Stop();
                    }
                } while (sw.Elapsed.TotalSeconds <= 5);
            }
        }

        internal static bool IsElementInViewPort(this IWebDriver driver, By by)
        {
            var element = driver.GetElementFromDOM(by);
            if (element == null) return false;
            var result = driver.ExecuteJavaScript<bool>(SHResources.GetElementVisibleState, element);
            return result;
        }

        internal static bool IsElementInViewPort(this IWebDriver driver, Netlenium.Driver.WebDriver.IWebElement element)
        {
            var result = driver.ExecuteJavaScript<bool>(SHResources.GetElementVisibleState, element);
            return result;
        }

        internal static void ScrollToElement(this IWebDriver driver, By by)
        {
            var element = driver.GetElementFromDOM(by, true);
            driver.ExecuteJavaScript(SHResources.ScrollToElement, element);
        }

        internal static Netlenium.Driver.WebDriver.IWebElement GetElementFromDOM(this IWebDriver driver, By by, bool throwEx = false)
        {
            try
            {
                var ele = driver.FindElement(by);
                return ele;
            }
            catch (NoSuchElementException)
            {
                if (throwEx) throw;
                return null;
            }
        }

        internal static IEnumerable<Netlenium.Driver.WebDriver.IWebElement> GetElementsFromDOM(this IWebDriver driver, By by, bool throwEx = false)
        {
            try
            {
                var ele = driver.FindElements(by);
                return ele;
            }
            catch (NoSuchElementException)
            {
                if (throwEx) throw;
                return null;
            }
        }

        internal static void SetElementHidden(this IWebDriver driver, Netlenium.Driver.WebDriver.IWebElement element)
        {
            driver.ExecuteJavaScript(SHResources.HideElementFromDOM, element);
        }


        internal static void SetElementVisible(this IWebDriver driver, Netlenium.Driver.WebDriver.IWebElement element)
        {
            driver.ExecuteJavaScript(SHResources.ShowElementInDOM, element);
        }

        internal static string GetElementAbsoluteXPath(this IWebDriver driver, Netlenium.Driver.WebDriver.IWebElement element)
        {
            return driver.ExecuteJavaScript<string>(SHResources.GetElementAbsoluteXPath, element);
        }

        internal static void ShowScrollBar(this IWebDriver driver, Netlenium.Driver.WebDriver.IWebElement element, string value)
        {
            driver.ExecuteJavaScript(SHResources.ShowScrollBar, element, value);
        }

        internal static List<Netlenium.Driver.WebDriver.IWebElement> GetAllElementsWithScrollbars(this IWebDriver driver)
        {
            IReadOnlyCollection<Netlenium.Driver.WebDriver.IWebElement> arr = new List<Netlenium.Driver.WebDriver.IWebElement>();
            try
            {
                arr = driver.ExecuteJavaScript<IReadOnlyCollection<Netlenium.Driver.WebDriver.IWebElement>>(SHResources
                    .GetAllElementsWithScrollBars);
            }
            catch (WebDriverException ex) when (ex.Message.Contains("Script returned a value"))
            {
                // nothing to do, elements with scrollbar not exists
            }

            return arr?.ToList();
        }

        internal static void HideScrollBar(this IWebDriver driver, Netlenium.Driver.WebDriver.IWebElement element)
        {
            driver.ExecuteJavaScript(SHResources.RemoveScrollBar, element);
        }


        internal static Netlenium.Driver.WebDriver.IWebElement GetElementWithActiveScrollBar(this IWebDriver driver)
        {
            var allElementsWithScrollbar = driver.GetAllElementsWithScrollbars();
            allElementsWithScrollbar = allElementsWithScrollbar.Where(o => o.Displayed).ToList();
            if (allElementsWithScrollbar.Count == 0) return driver.GetDocumentScrollingElement();
            var element =
                driver.ExecuteJavaScript<Netlenium.Driver.WebDriver.IWebElement>(SHResources.GetElementWithActiveScrollbar,
                    allElementsWithScrollbar);
            if (element == null || element.TagName.ToLower() == "body" ||
                element.TagName.ToLower() == "html") element = driver.GetDocumentScrollingElement();
            return element;
        }

        internal static Netlenium.Driver.WebDriver.IWebElement GetDocumentScrollingElement(this IWebDriver driver)
        {
            return driver.ExecuteJavaScript<Netlenium.Driver.WebDriver.IWebElement>("return document.scrollingElement");
        }

        internal static int GetElementScrollBarHeight(this IWebDriver driver, Netlenium.Driver.WebDriver.IWebElement element)
        {
            return int.Parse(driver
                .ExecuteJavaScript<long>("return arguments[0].scrollHeight", element).ToString());
        }

        internal static void ScrollTo(this IWebDriver driver, Netlenium.Driver.WebDriver.IWebElement element, int position)
        {
            driver.ExecuteJavaScript("$(arguments[0]).scrollTop(arguments[1])", element, position);
        }

        internal static int GetCurrentScrollLocation(this IWebDriver driver, Netlenium.Driver.WebDriver.IWebElement element)
        {
            var value = driver.ExecuteJavaScript<long>("return $(arguments[0]).scrollTop()", element);
            return (int) value;
        }
    }
}