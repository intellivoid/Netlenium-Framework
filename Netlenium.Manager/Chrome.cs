using Ionic.Zip;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using Netlenium.Types;

namespace Netlenium.Manager
{
    /// <summary>
    /// Chrome Driver Manager
    /// </summary>
    public class Chrome
    {
        /// <summary>
        /// Fetches the latest version number from the Internet
        /// </summary>
        public static string LatestVersion
        {
            get
            {
                WebClient httpWebClient = new WebClient();
                return httpWebClient.DownloadString(Properties.Resources.Chrome_LatestReleaseAPI);
            }
        }

        /// <summary>
        /// Checks the installation details of the Driver
        /// </summary>
        /// <param name="targetPlatform"></param>
        /// <returns></returns>
        public static DriverInstallationDetails CheckInstallation(Platform targetPlatform = Platform.AutoDetect)
        {
            DriverInstallationDetails results = new DriverInstallationDetails();

            if(targetPlatform == Platform.AutoDetect)
            {
                targetPlatform = Configuration.CurrentPlatform;
            }

            results.IsInstalled = true;
            results.TargetPlatform = targetPlatform;
            results.DriverType = Driver.Chrome;

            switch(targetPlatform)
            {
                case Platform.Win32:
                    results.DriverPath = $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome_win32";
                    results.DriverExecutableName = "chromedriver.exe";
                    results.DriverExecutable = $"{results.DriverPath}{Path.DirectorySeparatorChar}{results.DriverExecutableName}";

                    if (Directory.Exists(results.DriverPath) == false)
                    {
                        results.IsInstalled = false;
                    }

                    if (File.Exists(results.DriverExecutable) == false)
                    {
                        results.IsInstalled = false;
                    }

                    if (File.Exists($"{results.DriverPath}{Path.DirectorySeparatorChar}current_version") == false)
                    {
                        results.IsInstalled = false;
                    }

                    break;

                case Platform.Linux32:
                    results.DriverPath = $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome_linux32";
                    results.DriverExecutableName = "chromedriver";
                    results.DriverExecutable = $"{results.DriverPath}{Path.DirectorySeparatorChar}{results.DriverExecutableName}";

                    if (Directory.Exists(results.DriverPath) == false)
                    {
                        results.IsInstalled = false;
                    }

                    if (File.Exists(results.DriverExecutable) == false)
                    {
                        results.IsInstalled = false;
                    }

                    if (File.Exists($"{results.DriverPath}{Path.DirectorySeparatorChar}current_version") == false)
                    {
                        results.IsInstalled = false;
                    }

                    break;

                case Platform.Linux64:
                    results.DriverPath = $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome_linux64";
                    results.DriverExecutableName = "chromedriver";
                    results.DriverExecutable = $"{results.DriverPath}{Path.DirectorySeparatorChar}{results.DriverExecutableName}";

                    if (Directory.Exists(results.DriverPath) == false)
                    {
                        results.IsInstalled = false;
                    }

                    if (File.Exists(results.DriverExecutable) == false)
                    {
                        results.IsInstalled = false;
                    }

                    if (File.Exists($"{results.DriverPath}{Path.DirectorySeparatorChar}current_version") == false)
                    {
                        results.IsInstalled = false;
                    }

                    break;

                default:
                    throw new PlatformNotSupportedException();
            }

            results.Version = File.Exists($"{results.DriverPath}{Path.DirectorySeparatorChar}current_version") == false 
                ? null : File.ReadAllText($"{results.DriverPath}{Path.DirectorySeparatorChar}current_version");

            return results;
        }

        /// <summary>
        /// Uninstalls the driver from the system
        /// </summary>
        /// <param name="targetPlatform"></param>
        public static void UninstallDriver(Platform targetPlatform = Platform.AutoDetect)
        {
            if (targetPlatform == Platform.AutoDetect)
            {
                targetPlatform = Configuration.CurrentPlatform;
            }

            DriverInstallationDetails installationDetails = CheckInstallation(targetPlatform);

            if(installationDetails.IsInstalled == false)
            {
                throw new DriverUninstallationException("No installed driver was detected therefore the uninstallation process could not be completed.");
            }

            try
            {
                File.Delete(installationDetails.DriverExecutable);
            }
            catch(Exception exception)
            {
                throw new DriverUninstallationException($"Cannot delete the Driver, {exception.Message}");
            }

            try
            {
                File.Delete($"{installationDetails.DriverPath}{Path.DirectorySeparatorChar}current_version");
            }
            catch(Exception exception)
            {
                throw new DriverUninstallationException($"Cannot delete the current version file, {exception.Message}");
            }

            try
            {
                Directory.Delete(installationDetails.DriverPath, true);
            }
            catch(Exception exception)
            {
                throw new DriverUninstallationException($"Cannot delete the driver resources directory, {exception.Message}");
            }
        }

        /// <summary>
        /// Installs the driver from a .zip file
        /// </summary>
        /// <param name="driverZipFile"></param>
        /// <param name="version"></param>
        /// <param name="targetPlatform"></param>
        public static void InstallDriver(string driverZipFile, string version, Platform targetPlatform = Platform.AutoDetect)
        {
            if (targetPlatform == Platform.AutoDetect)
            {
                targetPlatform = Configuration.CurrentPlatform;
            }

            var installationDetails = CheckInstallation(targetPlatform);
            
            var temporaryExtractedExecutable = string.Empty;
            var temporaryExecutableName = string.Empty;

            switch(targetPlatform)
            {
                case Platform.Win32:
                    temporaryExtractedExecutable = $"{Configuration.TemporaryDirectory}{Path.DirectorySeparatorChar}chromedriver.exe";
                    temporaryExecutableName = "chromedriver.exe";
                    break;

                case Platform.Linux32:
                    temporaryExtractedExecutable = $"{Configuration.TemporaryDirectory}{Path.DirectorySeparatorChar}chromedriver";
                    temporaryExecutableName = "chromedriver";
                    break;

                case Platform.Linux64:
                    temporaryExtractedExecutable = $"{Configuration.TemporaryDirectory}{Path.DirectorySeparatorChar}chromedriver";
                    temporaryExecutableName = "chromedriver";
                    break;

                case Platform.AutoDetect:
                    break;
                    
                default:
                    throw new PlatformNotSupportedException();
            }

            if (File.Exists(temporaryExtractedExecutable))
            {
                File.Delete(temporaryExtractedExecutable);
            }
             
            var zip = ZipFile.Read(driverZipFile);
            
            foreach (var entry in zip)
            {
                if (entry.FileName != temporaryExecutableName) continue;
                if (entry.IsDirectory) continue;
                entry.Extract(Configuration.TemporaryDirectory, ExtractExistingFileAction.OverwriteSilently);
                break;
            }
            
            zip.Dispose();

            if (installationDetails.IsInstalled)
            {
                UninstallDriver(targetPlatform);
            }

            if (Directory.Exists(installationDetails.DriverPath) == false)
            {
                Directory.CreateDirectory(installationDetails.DriverPath);
            }
            
            File.Move(temporaryExtractedExecutable, installationDetails.DriverExecutable);
            File.WriteAllText($"{installationDetails.DriverPath}{Path.DirectorySeparatorChar}current_version", version);
            File.Delete(temporaryExtractedExecutable);

            if (!(targetPlatform == Platform.Linux32 || targetPlatform == Platform.Linux64)) return;
            
            try
            {
                Process.Start("chmod", $"+x \"{installationDetails.DriverExecutable}\"");
            }
            catch (Exception exception)
            {
                throw new PermissionsErrorException(exception.Message);
            }
        }

        /// <summary>
        /// Downloads and installs the latest driver form the internet
        /// </summary>
        /// <param name="targetPlatform"></param>
        public static void InstallLatestDriver(Platform targetPlatform = Platform.AutoDetect)
        {
            if (targetPlatform == Platform.AutoDetect)
            {
                targetPlatform = Configuration.CurrentPlatform;
            }

            var currentLatestVersion = LatestVersion;

            var temporaryZipFile = string.Empty;
            var downloadUrl = string.Empty;

            switch (targetPlatform)
            {
                case Platform.Win32:
                    temporaryZipFile = $"{Configuration.TemporaryDirectory}{Path.DirectorySeparatorChar}chromedriver_win32.zip";
                    downloadUrl = Properties.Resources.Chrome_Win32API.Replace("%VERSION%", currentLatestVersion);
                    break;

                case Platform.Linux32:
                    temporaryZipFile = $"{Configuration.TemporaryDirectory}{Path.DirectorySeparatorChar}chromedriver_linux32.zip";
                    downloadUrl = Properties.Resources.Chrome_Linux32API.Replace("%VERSION%", currentLatestVersion);
                    break;

                case Platform.Linux64:
                    temporaryZipFile = $"{Configuration.TemporaryDirectory}{Path.DirectorySeparatorChar}chromedriver_linux64.zip";
                    downloadUrl = Properties.Resources.Chrome_Linux64API.Replace("%VERSION%", currentLatestVersion);
                    break;

                case Platform.AutoDetect:
                    break;
                    
                default:
                    throw new PlatformNotSupportedException();
            }

            if(File.Exists(temporaryZipFile))
            {
                File.Delete(temporaryZipFile);
            }

            var webClient = new WebClient();
            webClient.DownloadFile(downloadUrl, temporaryZipFile);

            InstallDriver(temporaryZipFile, currentLatestVersion, targetPlatform);
            File.Delete(temporaryZipFile);
        }
        /// <summary>
        /// Initializes the driver by installing it if it's not installed, and updating it if it's outdated
        /// </summary>
        /// <param name="targetPlatform"></param>
        public static void Initialize(Platform targetPlatform = Platform.AutoDetect)
        {
            if (targetPlatform == Platform.AutoDetect)
            {
                targetPlatform = Configuration.CurrentPlatform;
            }

            var installationDetails = CheckInstallation(targetPlatform);

            if(installationDetails.IsInstalled == false)
            {
                InstallLatestDriver(targetPlatform);
            }
            else
            {
                var currentLatestVersion = LatestVersion;
                
                if (currentLatestVersion == installationDetails.Version) return;
                
                UninstallDriver(targetPlatform);
                InstallLatestDriver(targetPlatform);
            }
        }
    }

}
