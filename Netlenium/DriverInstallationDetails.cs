namespace Netlenium
{
    /// <summary>
    /// Driver Installation Details Object
    /// </summary>
    public class DriverInstallationDetails
    {
        /// <summary>
        /// Indicates if the Driver is Installed or not
        /// </summary>
        public bool IsInstalled
        {
            get; set;
        }

        /// <summary>
        /// The driver type
        /// </summary>
        public Types.Driver DriverType
        {
            get; set;
        }

        /// <summary>
        /// Indicates the target platform of this Driver Installation
        /// </summary>
        public Types.Platform TargetPlatform
        {
            get; set;
        }

        /// <summary>
        /// The version of the installed driver
        /// </summary>
        public string Version
        {
            get; set;
        }

        /// <summary>
        /// The driver's resources path
        /// </summary>
        public string DriverPath
        {
            get; set;
        }

        /// <summary>
        /// The executable name of the driver
        /// </summary>
        public string DriverExecutableName
        {
            get; set;
        }

        /// <summary>
        /// The full path of the driver's executable file, including the name
        /// </summary>
        public string DriverExecutable
        {
            get; set;
        }
    }
}
