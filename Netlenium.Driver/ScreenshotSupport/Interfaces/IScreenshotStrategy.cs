using System;
using ImageMagick;
using Netlenium.Driver.WebDriver;

namespace Netlenium.Driver.ScreenshotSupport.Interfaces
{
    public interface IScreenshotStrategy
    {
        /// <summary>
        ///     Make a screenshot. Create or change.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        [CLSCompliant(false)]
        IMagickImage MakeScreenshot(IWebDriver driver);
    }
}