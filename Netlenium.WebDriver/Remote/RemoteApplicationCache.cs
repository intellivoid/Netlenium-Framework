using System;
using System.Globalization;
using Netlenium.WebDriver.Html5;

namespace Netlenium.WebDriver.Remote
{
    /// <summary>
    /// Defines the interface through which the user can manipulate application cache.
    /// </summary>
    public class RemoteApplicationCache : IApplicationCache
    {
        private RemoteWebDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteApplicationCache"/> class.
        /// </summary>
        /// <param name="driver">The <see cref="RemoteWebDriver"/> for which the application cache will be managed.</param>
        public RemoteApplicationCache(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// Gets the current state of the application cache.
        /// </summary>
        public AppCacheStatus Status
        {
            get
            {
                Response commandResponse = this.driver.InternalExecute(DriverCommand.GetAppCacheStatus, null);
                Type appCacheStatusType = typeof(AppCacheStatus);
                int statusValue = Convert.ToInt32(commandResponse.Value, CultureInfo.InvariantCulture);
                if (!Enum.IsDefined(appCacheStatusType, statusValue))
                {
                    // This is a protocol error. The returned value should be a number
                    // and should be within the range of values specified.
                    throw new InvalidOperationException("Value returned from remote end is not a number or is not in the specified range of values. Actual value was " + commandResponse.Value.ToString());
                }

                AppCacheStatus status = (AppCacheStatus)Enum.ToObject(appCacheStatusType, commandResponse.Value);
                return status;
            }
        }
    }
}
