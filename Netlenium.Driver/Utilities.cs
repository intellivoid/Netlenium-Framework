using System;
using System.Diagnostics;
using System.IO;

namespace Netlenium.Driver
{
    /// <summary>
    /// General Utilities
    /// </summary>
    internal class Utilities
    {
        /// <summary>
        /// Gets the current platform the machine is running on
        /// </summary>
        public static PlatformType CurrentPlatform
        {
            get
            {
                var p = (int)Environment.OSVersion.Platform;
                
                if(p == 4 || p == 6 || p == 128)
                {
                    return Environment.Is64BitOperatingSystem ? PlatformType.Linux64 : PlatformType.Linux32;
                }

                return PlatformType.Windows;
            }
        }

        /// <summary>
        /// Gives executable permissions to the given file using chmod
        /// </summary>
        /// <param name="filePath"></param>
        public static void GiveExecutablePermissions(string filePath)
        {
            Process.Start("chmod", $"+x \"{filePath}\"");
        }
    }
}