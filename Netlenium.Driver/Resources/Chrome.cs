using System.IO;

namespace Netlenium.Driver.Resources
{
    public class Chrome
    {
        /// <summary>
        /// Gets the resources path for the Chrome Driver
        /// </summary>
        /// <param name="Platform"></param>
        /// <returns></returns>
        public static string GetResourcesPath(Types.Platform Platform)
        {
            switch (Platform)
            {
                case Types.Platform.Win32:
                    return $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chromedriver_win32";

                case Types.Platform.Linux32:
                    return $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chromedriver_linux32";

                case Types.Platform.Linux64:
                    return $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chromedriver_linux64";

                default:
                    throw new UnsupportedPlatformException();
            }
        }

        /// <summary>
        /// Gets the name of the executable file for the driver
        /// </summary>
        /// <param name="Platform"></param>
        /// <returns></returns>
        public static string GetExecutableName(Types.Platform Platform)
        {
            switch (Platform)
            {
                case Types.Platform.Win32:
                    return "chromedriver.exe";

                case Types.Platform.Linux32:
                    return "chromedriver";

                case Types.Platform.Linux64:
                    return "chromedriver";

                default:
                    throw new UnsupportedPlatformException();
            }
        }

        /// <summary>
        /// Gets the full path of the executable file for the driver (including executable name)
        /// </summary>
        /// <param name="Platform"></param>
        /// <returns></returns>
        public static string GetExecutablePath(Types.Platform Platform)
        {
            return $"{GetResourcesPath(Platform)}{Path.DirectorySeparatorChar}{GetExecutableName(Platform)}";
        }

        /// <summary>
        /// Returns the installed version of the Driver
        /// </summary>
        /// <param name="Platform"></param>
        /// <returns></returns>
        public static string GetInstalledVersion(Types.Platform Platform)
        {
            string VersionFilePath = $"{GetResourcesPath(Platform)}{Path.DirectorySeparatorChar}current_version";

            if (File.Exists(VersionFilePath) == false)
            {
                throw new VersionFileNotFoundException();
            }

            return File.ReadAllText(VersionFilePath);
        }

    }
}
