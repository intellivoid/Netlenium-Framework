using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Netlenium.Manager
{
    public class GeckoFX32
    {
        /// <summary>
        /// Fetches the Assembly's executing directory
        /// </summary>
        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        /// <summary>
        /// Checks the current installation state of the driver
        /// </summary>
        /// <param name="TargetPlatform"></param>
        /// <returns></returns>
        public static DriverInstallationDetails CheckInstallation(Types.Platform TargetPlatform = Types.Platform.AutoDetect)
        {
            DriverInstallationDetails Results = new DriverInstallationDetails();

            if (TargetPlatform == Types.Platform.AutoDetect)
            {
                TargetPlatform = Configuration.CurrentPlatform;
            }

            Results.IsInstalled = true;
            Results.TargetPlatform = TargetPlatform;
            Results.DriverType = Types.Driver.Chrome;

            switch (TargetPlatform)
            {
                case Types.Platform.Win32:
                    Results.DriverPath = $"{AssemblyDirectory}{Path.DirectorySeparatorChar}xulrunner_win32";
                    Results.DriverExecutableName = "xul.dll";
                    Results.DriverExecutable = $"{Results.DriverPath}{Path.DirectorySeparatorChar}{Results.DriverExecutableName}";

                    if (Directory.Exists(Results.DriverPath) == false)
                    {
                        Results.IsInstalled = false;
                    }

                    if (File.Exists(Results.DriverExecutable) == false)
                    {
                        Results.IsInstalled = false;
                    }

                    Results.Version = "60.0.26";

                    break;

                default:
                    throw new PlatformNotSupportedException();
            }

            return Results;
        }
    }
}
