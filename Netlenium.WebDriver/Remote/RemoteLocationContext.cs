using System;
using System.Collections.Generic;
using System.Globalization;
using Netlenium.WebDriver.Html5;

namespace Netlenium.WebDriver.Remote
{
    /// <summary>
    /// Defines the interface through which the user can manipulate browser location.
    /// </summary>
    public class RemoteLocationContext : ILocationContext
    {
        private RemoteWebDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteLocationContext"/> class.
        /// </summary>
        /// <param name="driver">The <see cref="RemoteWebDriver"/> for which the application cache will be managed.</param>
        public RemoteLocationContext(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// Gets or sets a value indicating the physical location of the browser.
        /// </summary>
        public Location PhysicalLocation
        {
            get
            {
                Response commandResponse = this.driver.InternalExecute(DriverCommand.GetLocation, null);
                Dictionary<string, object> location = commandResponse.Value as Dictionary<string, object>;
                if (location != null)
                {
                    return new Location(double.Parse(location["latitude"].ToString(), CultureInfo.InvariantCulture), double.Parse(location["longitude"].ToString(), CultureInfo.InvariantCulture), double.Parse(location["altitude"].ToString(), CultureInfo.InvariantCulture));
                }

                return null;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "value cannot be null");
                }

                Dictionary<string, object> loc = new Dictionary<string, object>();
                loc.Add("latitude", value.Latitude);
                loc.Add("longitude", value.Longitude);
                loc.Add("altitude", value.Altitude);

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("location", loc);
                this.driver.InternalExecute(DriverCommand.SetLocation, parameters);
            }
        }
    }
}
