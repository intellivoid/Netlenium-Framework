using System;
using System.Diagnostics;
using Netlenium.Driver.WebDriver;
using Netlenium.Driver.WebDriver.Remote;
using Netlenium.Driver.WebDriver.Support.Extensions;

namespace Netlenium.Driver.ScreenshotSupport.Helpers
{
    internal static class IeDriverHelper
    {
        private const string Script = "return document.scrollingElement";

        public static void CheckIeDriver(this IWebDriver driver)
        {
            var browser = ((RemoteWebDriver) driver).Capabilities.GetCapability("browserName").ToString().ToLower();
            if (!browser.Contains("ie") && !browser.Contains("internet explorer")) return;
            var scrollingElement = driver.ExecuteJavaScript<Netlenium.Driver.WebDriver.IWebElement>(Script);
            if (scrollingElement != null) return;
            driver.ExecuteJavaScript(SHResources.ScrollingElement);
            var sw = Stopwatch.StartNew();
            while (sw.Elapsed.TotalSeconds <= 10)
            {
                scrollingElement = driver.ExecuteJavaScript<Netlenium.Driver.WebDriver.IWebElement>(Script);
                if (scrollingElement != null) return;
            }

            throw new Exception("Cant get scrolling element at document.");
        }
    }
}