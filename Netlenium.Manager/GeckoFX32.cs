using System;
using System.IO;
using System.Reflection;
using Netlenium.Types;

namespace Netlenium.Manager
{
    /// <summary>
    /// GeckoFX Driver Manager
    /// </summary>
    public class GeckoFx32
    {
        /// <summary>
        /// Fetches the Assembly's executing directory
        /// </summary>
        private static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        /// <summary>
        /// Checks the current installation state of the driver
        /// </summary>
        /// <param name="targetPlatform"></param>
        /// <returns></returns>
        public static DriverInstallationDetails CheckInstallation(Platform targetPlatform = Platform.AutoDetect)
        {
            var results = new DriverInstallationDetails();

            if (targetPlatform == Platform.AutoDetect)
            {
                targetPlatform = Configuration.CurrentPlatform;
            }

            results.IsInstalled = true;
            results.TargetPlatform = targetPlatform;
            results.DriverType = Driver.Chrome;

            switch (targetPlatform)
            {
                case Platform.Win32:
                    results.DriverPath = $"{AssemblyDirectory}{Path.DirectorySeparatorChar}xulrunner_win32";
                    results.DriverExecutableName = "xul.dll";
                    results.DriverExecutable = $"{results.DriverPath}{Path.DirectorySeparatorChar}{results.DriverExecutableName}";

                    if (Directory.Exists(results.DriverPath) == false)
                    {
                        results.IsInstalled = false;
                    }

                    if (File.Exists(results.DriverExecutable) == false)
                    {
                        results.IsInstalled = false;
                    }

                    results.Version = "60.0.26";

                    break;

                case Platform.AutoDetect:
                    throw new PlatformNotSupportedException();
                    
                case Platform.Linux32:
                    throw new PlatformNotSupportedException();
                    
                case Platform.Linux64:
                    throw new PlatformNotSupportedException();
                    
                default:
                    throw new PlatformNotSupportedException();
            }

            return results;
        }
    }
}
