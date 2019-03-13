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
                int p = (int)Environment.OSVersion.Platform;
                if((p == 4) || (p == 6) || (p == 128))
                {
                    return Types.Platform.Linux32;
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
                string DirectoryPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}{Path.DirectorySeparatorChar}Netlenium";

                if (Directory.Exists(DirectoryPath) == false)
                {
                    Directory.CreateDirectory(DirectoryPath);
                }

                return DirectoryPath;
            }
        }

        /// <summary>
        /// Driver files in the Netlenium application data directory
        /// </summary>
        public static string DriverDirectory
        {
            get
            {
                string DirectoryPath = $"{ApplicationDataDirectory}{Path.DirectorySeparatorChar}Drivers";

                if (Directory.Exists(DirectoryPath) == false)
                {
                    Directory.CreateDirectory(DirectoryPath);
                }

                return DirectoryPath;
            }
        }

        /// <summary>
        /// The runtime directory for Python Programs
        /// </summary>
        public static string RuntimeDirectory
        {
            get
            {
                string DirectoryPath = $"{ApplicationDataDirectory}{Path.DirectorySeparatorChar}Runtime";

                if (Directory.Exists(DirectoryPath) == false)
                {
                    Directory.CreateDirectory(DirectoryPath);
                }

                return DirectoryPath;
            }
        }

        /// <summary>
        /// Gets the temporary directory from the Netlenium Application Data directory
        /// </summary>
        public static string TemporaryDirectory
        {
            get
            {
                string DirectoryPath = $"{ApplicationDataDirectory}{Path.DirectorySeparatorChar}tmp";

                if (Directory.Exists(DirectoryPath) == false)
                {
                    Directory.CreateDirectory(DirectoryPath);
                }

                return DirectoryPath;
            }

        }
    }
}
