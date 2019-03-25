using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace Netlenium.WebDriver.Remote
{
    /// <summary>
    /// Defines the interface through which the user can manipulate the browser window.
    /// </summary>
    internal class RemoteWindow : IWindow
    {
        private RemoteWebDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteWindow"/> class.
        /// </summary>
        /// <param name="driver">Instance of the driver currently in use</param>
        public RemoteWindow(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// Gets or sets the position of the browser window relative to the upper-left corner of the screen.
        /// </summary>
        /// <remarks>When setting this property, it should act as the JavaScript window.moveTo() method.</remarks>
        public Point Position
        {
            get
            {
                Response commandResponse;
                if (this.driver.IsSpecificationCompliant)
                {
                    commandResponse = this.driver.InternalExecute(DriverCommand.GetWindowRect, null);
                }
                else
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("windowHandle", "current");
                    commandResponse = this.driver.InternalExecute(DriverCommand.GetWindowPosition, parameters);
                }

                Dictionary<string, object> rawPosition = (Dictionary<string, object>)commandResponse.Value;
                int x = Convert.ToInt32(rawPosition["x"], CultureInfo.InvariantCulture);
                int y = Convert.ToInt32(rawPosition["y"], CultureInfo.InvariantCulture);
                return new Point(x, y);
            }

            set
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("x", value.X);
                parameters.Add("y", value.Y);
                if (this.driver.IsSpecificationCompliant)
                {
                    this.driver.InternalExecute(DriverCommand.SetWindowRect, parameters);
                }
                else
                {
                    parameters.Add("windowHandle", "current");
                    this.driver.InternalExecute(DriverCommand.SetWindowPosition, parameters);
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the outer browser window, including title bars and window borders.
        /// </summary>
        /// <remarks>When setting this property, it should act as the JavaScript window.resizeTo() method.</remarks>
        public Size Size
        {
            get
            {
                Response commandResponse;
                if (this.driver.IsSpecificationCompliant)
                {
                    commandResponse = this.driver.InternalExecute(DriverCommand.GetWindowRect, null);
                }
                else
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("windowHandle", "current");
                    commandResponse = this.driver.InternalExecute(DriverCommand.GetWindowSize, parameters);
                }

                Dictionary<string, object> rawPosition = (Dictionary<string, object>)commandResponse.Value;
                int height = Convert.ToInt32(rawPosition["height"], CultureInfo.InvariantCulture);
                int width = Convert.ToInt32(rawPosition["width"], CultureInfo.InvariantCulture);
                return new Size(width, height);
            }

            set
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("width", value.Width);
                parameters.Add("height", value.Height);
                if (this.driver.IsSpecificationCompliant)
                {
                    this.driver.InternalExecute(DriverCommand.SetWindowRect, parameters);
                }
                else
                {
                    parameters.Add("windowHandle", "current");
                    this.driver.InternalExecute(DriverCommand.SetWindowSize, parameters);
                }
            }
        }

        /// <summary>
        /// Maximizes the current window if it is not already maximized.
        /// </summary>
        public void Maximize()
        {
            Dictionary<string, object> parameters = null;
            if (!this.driver.IsSpecificationCompliant)
            {
                parameters = new Dictionary<string, object>();
                parameters.Add("windowHandle", "current");
            }

            this.driver.InternalExecute(DriverCommand.MaximizeWindow, parameters);
        }

        /// <summary>
        /// Minimizes the current window if it is not already maximized.
        /// </summary>
        public void Minimize()
        {
            Dictionary<string, object> parameters = null;
            this.driver.InternalExecute(DriverCommand.MinimizeWindow, parameters);
        }

        /// <summary>
        /// Sets the current window to full screen if it is not already in that state.
        /// </summary>
        public void FullScreen()
        {
            Dictionary<string, object> parameters = null;
            this.driver.InternalExecute(DriverCommand.FullScreenWindow, parameters);
        }
    }
}
