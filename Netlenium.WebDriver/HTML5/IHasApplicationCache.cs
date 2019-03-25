﻿namespace Netlenium.WebDriver.Html5
{
    /// <summary>
    /// Interface allowing the user to determine if the driver instance supports application cache.
    /// </summary>
    public interface IHasApplicationCache
    {
        /// <summary>
        /// Gets a value indicating whether manipulating the application cache is supported for this driver.
        /// </summary>
        bool HasApplicationCache { get; }

        /// <summary>
        /// Gets an <see cref="IApplicationCache"/> object for managing application cache.
        /// </summary>
        IApplicationCache ApplicationCache { get; }
    }
}
