namespace Netlenium.WebDriver.Chrome
{
    /// <summary>
    /// Represents the type-safe options for setting settings for emulating a
    /// mobile device in the Chrome browser.
    /// </summary>
    public class ChromeMobileEmulationDeviceSettings
    {
        private string userAgent = string.Empty;
        private long width;
        private long height;
        private double pixelRatio;
        private bool enableTouchEvents = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromeMobileEmulationDeviceSettings"/> class.
        /// </summary>
        public ChromeMobileEmulationDeviceSettings()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromeMobileEmulationDeviceSettings"/> class.
        /// </summary>
        /// <param name="userAgent">The user agent string to be used by the browser when emulating
        /// a mobile device.</param>
        public ChromeMobileEmulationDeviceSettings(string userAgent)
        {
            this.userAgent = userAgent;
        }

        /// <summary>
        /// Gets or sets the user agent string to be used by the browser when emulating
        /// a mobile device.
        /// </summary>
        public string UserAgent
        {
            get { return this.userAgent; }
            set { this.userAgent = value; }
        }

        /// <summary>
        /// Gets or sets the width in pixels to be used by the browser when emulating
        /// a mobile device.
        /// </summary>
        public long Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        /// <summary>
        /// Gets or sets the height in pixels to be used by the browser when emulating
        /// a mobile device.
        /// </summary>
        public long Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        /// <summary>
        /// Gets or sets the pixel ratio to be used by the browser when emulating
        /// a mobile device.
        /// </summary>
        public double PixelRatio
        {
            get { return this.pixelRatio; }
            set { this.pixelRatio = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether touch events should be enabled by
        /// the browser when emulating a mobile device. Defaults to <see langword="true"/>.
        /// </summary>
        public bool EnableTouchEvents
        {
            get { return this.enableTouchEvents; }
            set { this.enableTouchEvents = value; }
        }
    }
}
