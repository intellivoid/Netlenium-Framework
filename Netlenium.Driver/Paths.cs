using System;
using System.IO;

namespace Netlenium.Driver
{
    public class Paths
    {
        /// <summary>
        /// The Netlenium Application Data Directory
        /// </summary>
        public static string NetleniumApplicationDirectory
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
        /// Driver installations in the Netlenium Application Directory
        /// </summary>
        public static string DriverDirectory
        {
            get
            {
                var directoryPath = $"{NetleniumApplicationDirectory}{Path.DirectorySeparatorChar}Drivers";

                if (Directory.Exists(directoryPath) == false)
                {
                    Directory.CreateDirectory(directoryPath);
                }

                return directoryPath;
            }
        }
        
        /// <summary>
        /// Gets the temporary directory from the Netlenium Application directory
        /// </summary>
        public static string TemporaryDirectory
        {
            get
            {
                var directoryPath = $"{NetleniumApplicationDirectory}{Path.DirectorySeparatorChar}tmp";

                if (Directory.Exists(directoryPath) == false)
                {
                    Directory.CreateDirectory(directoryPath);
                }

                return directoryPath;
            }
        }
        
        /// <summary>
        /// Gets the directory dedicated for storing log files
        /// </summary>
        public static string LoggingDirectory
        {
            get
            {
                var directoryPath = $"{NetleniumApplicationDirectory}{Path.DirectorySeparatorChar}logging";

                if (Directory.Exists(directoryPath) == false)
                {
                    Directory.CreateDirectory(directoryPath);
                }

                return directoryPath;
            }
        }
    }
}