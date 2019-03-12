namespace Netlenium
{
    public class DriverConfiguration
    {
        /// <summary>
        /// Starts the browser in headless mode
        /// </summary>
        public bool Headless
        {
            get; set;
        }
        
        /// <summary>
        /// The target platform that the Framework will try
        /// to execute the drivers for
        /// </summary>
        public Types.Platform TargetPlatform
        {
            get; set;
        }

        /// <summary>
        /// Public Consturctor
        /// </summary>
        public DriverConfiguration()
        {
            Headless = true;
            TargetPlatform = Configuration.CurrentPlatform;
        }
    }
}
