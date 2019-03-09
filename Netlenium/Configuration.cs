using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netlenium
{
    /// <summary>
    /// Configuration Class for Netlenium
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Indicates if the Library is running in Mono
        /// </summary>
        /// <returns></returns>
        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        /// <summary>
        /// The Netlenium Application Data Directory
        /// </summary>
        public static string ApplicationDataDirectory
        {
            get
            {
                string DirectoryPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}{Path.DirectorySeparatorChar}Netlenium";

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


    }
}
