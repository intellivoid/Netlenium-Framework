using System;
using ImageMagick;
using Netlenium.Driver.WebDriver;

namespace Netlenium.Driver.ScreenshotSupport.Interfaces
{
    public interface ICuttingStrategy
    {
        [CLSCompliant(false)]
        IMagickImage Cut(IWebDriver driver, IMagickImage magickImage);
    }
}