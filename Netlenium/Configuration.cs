using System;
using System.IO;

namespace Netlenium
{
    /// <summary>
    /// Configuration Class for Netlenium
    /// </summary>
    public class Configuration
    {
        public static Types.Platform CurrentPlatform
        {
            get
            {
                var p = (int)Environment.OSVersion.Platform;
                
                if(p == 4 || p == 6 || p == 128)
                {
                    return Environment.Is64BitOperatingSystem ? Types.Platform.Linux64 : Types.Platform.Linux32;
                }

                return Types.Platform.Win32;
            }
        }

        /// <summary>
        /// The Netlenium Application Data Directory
        /// </summary>
        public static string ApplicationDataDirectory
        {
            get
            {
                var directoryPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}{Path.DirectorySeparatorChar}Netlenium";

                if (Directory.Exists(directoryPath) == false)
                {
                    Directory.CreateDirectory(directoryPath);
                }

                return directoryPath;
            }
        }

        /// <summary>
        /// Driver files in the Netlenium application data directory
        /// </summary>
        public static string DriverDirectory
        {
            get
            {
                var directoryPath = $"{ApplicationDataDirectory}{Path.DirectorySeparatorChar}Drivers";

                if (Directory.Exists(directoryPath) == false)
                {
                    Directory.CreateDirectory(directoryPath);
                }

                return directoryPath;
            }
        }

        /// <summary>
        /// The runtime directory for Python Programs
        /// </summary>
        public static string RuntimeDirectory
        {
            get
            {
                var directoryPath = $"{ApplicationDataDirectory}{Path.DirectorySeparatorChar}Runtime";

                if (Directory.Exists(directoryPath) == false)
                {
                    Directory.CreateDirectory(directoryPath);
                }

                return directoryPath;
            }
        }

        /// <summary>
        /// Gets the temporary directory from the Netlenium Application Data directory
        /// </summary>
        public static string TemporaryDirectory
        {
            get
            {
                var directoryPath = $"{ApplicationDataDirectory}{Path.DirectorySeparatorChar}tmp";

                if (Directory.Exists(directoryPath) == false)
                {
                    Directory.CreateDirectory(directoryPath);
                }

                return directoryPath;
            }
        }

        public static string LoggingDirectory
        {
            get
            {
                var directoryPath = $"{ApplicationDataDirectory}{Path.DirectorySeparatorChar}logs";

                if (Directory.Exists(directoryPath) == false)
                {
                    Directory.CreateDirectory(directoryPath);
                }

                return directoryPath;
            }
        }
    }
}
