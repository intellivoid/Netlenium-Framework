using System;
using ImageMagick;
using Netlenium.Driver.WebDriver;
using Netlenium.Driver.ScreenshotSupport.Interfaces;

namespace Netlenium.Driver.ScreenshotSupport.Decorators
{
    public abstract class BaseScreenshotDecorator : IScreenshotStrategy
    {
        #region Ctor

        protected BaseScreenshotDecorator(IScreenshotStrategy strategy)
        {
            NestedStrategy = strategy;
        }

        #endregion


        #region Props

        /// <summary>
        ///     Nested strategy, if have. Else is null.
        /// </summary>
        public IScreenshotStrategy NestedStrategy { get; }

        #endregion


        #region Abstract member

        [CLSCompliant(false)]
        public abstract IMagickImage MakeScreenshot(IWebDriver driver);

        #endregion
    }
}