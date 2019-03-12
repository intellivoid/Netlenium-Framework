using Ionic.Zip;
using System;
using System.IO;
using System.Net;

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
                WebClient HTTPWebClient = new WebClient();
                return HTTPWebClient.DownloadString(Properties.Resources.Chrome_LatestReleaseAPI);
            }
        }

        /// <summary>
        /// Checks the installation details of the Driver
        /// </summary>
        /// <param name="TargetPlatform"></param>
        /// <returns></returns>
        public static DriverInstallationDetails CheckInstallation(Types.Platform TargetPlatform = Types.Platform.AutoDetect)
        {
            DriverInstallationDetails Results = new DriverInstallationDetails();

            if(TargetPlatform == Types.Platform.AutoDetect)
            {
                TargetPlatform = Configuration.CurrentPlatform;
            }

            Results.IsInstalled = true;
            Results.TargetPlatform = TargetPlatform;
            Results.DriverType = Types.Driver.Chrome;

            switch(TargetPlatform)
            {
                case Types.Platform.Win32:
                    Results.DriverPath = $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome_win32";
                    Results.DriverExecutableName = "chromedriver.exe";
                    Results.DriverExecutable = $"{Results.DriverPath}{Path.DirectorySeparatorChar}{Results.DriverExecutableName}";

                    if (Directory.Exists(Results.DriverPath) == false)
                    {
                        Results.IsInstalled = false;
                    }

                    if (File.Exists(Results.DriverExecutable) == false)
                    {
                        Results.IsInstalled = false;
                    }

                    if (File.Exists($"{Results.DriverPath}{Path.DirectorySeparatorChar}current_version") == false)
                    {
                        Results.IsInstalled = false;
                    }

                    break;

                case Types.Platform.Linux32:
                    Results.DriverPath = $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome_linux32";
                    Results.DriverExecutableName = "chromedriver";
                    Results.DriverExecutable = $"{Results.DriverPath}{Path.DirectorySeparatorChar}{Results.DriverExecutableName}";

                    if (Directory.Exists(Results.DriverPath) == false)
                    {
                        Results.IsInstalled = false;
                    }

                    if (File.Exists(Results.DriverExecutable) == false)
                    {
                        Results.IsInstalled = false;
                    }

                    if (File.Exists($"{Results.DriverPath}{Path.DirectorySeparatorChar}current_version") == false)
                    {
                        Results.IsInstalled = false;
                    }

                    break;

                case Types.Platform.Linux64:
                    Results.DriverPath = $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome_linux64";
                    Results.DriverExecutableName = "chromedriver";
                    Results.DriverExecutable = $"{Results.DriverPath}{Path.DirectorySeparatorChar}{Results.DriverExecutableName}";

                    if (Directory.Exists(Results.DriverPath) == false)
                    {
                        Results.IsInstalled = false;
                    }

                    if (File.Exists(Results.DriverExecutable) == false)
                    {
                        Results.IsInstalled = false;
                    }

                    if (File.Exists($"{Results.DriverPath}{Path.DirectorySeparatorChar}current_version") == false)
                    {
                        Results.IsInstalled = false;
                    }

                    break;

                default:
                    throw new PlatformNotSupportedException();
            }

            if (File.Exists($"{Results.DriverPath}{Path.DirectorySeparatorChar}current_version") == false)
            {
                Results.Version = null;
            }
            else
            {
                Results.Version = File.ReadAllText($"{Results.DriverPath}{Path.DirectorySeparatorChar}current_version");
            }
            

            return Results;
        }

        /// <summary>
        /// Uninstalls the driver from the system
        /// </summary>
        /// <param name="TargetPlatform"></param>
        public static void UninstallDriver(Types.Platform TargetPlatform = Types.Platform.AutoDetect)
        {
            if (TargetPlatform == Types.Platform.AutoDetect)
            {
                TargetPlatform = Configuration.CurrentPlatform;
            }

            DriverInstallationDetails InstallationDetails = CheckInstallation(TargetPlatform);

            if(InstallationDetails.IsInstalled == false)
            {
                throw new DriverUninstallationException("No installed driver was detected therefore the uninstallation process could not be completed.");
            }

            try
            {
                File.Delete(InstallationDetails.DriverExecutable);
            }
            catch(Exception exception)
            {
                throw new DriverUninstallationException($"Cannot delete the Driver, {exception.Message}");
            }

            try
            {
                File.Delete($"{InstallationDetails.DriverPath}{Path.DirectorySeparatorChar}current_version");
            }
            catch(Exception exception)
            {
                throw new DriverUninstallationException($"Cannot delete the current version file, {exception.Message}");
            }

            try
            {
                Directory.Delete(InstallationDetails.DriverPath, true);
            }
            catch(Exception exception)
            {
                throw new DriverUninstallationException($"Cannot delete the driver resources directory, {exception.Message}");
            }
        }

        /// <summary>
        /// Installs the driver from a .zip file
        /// </summary>
        /// <param name="DriverZipFile"></param>
        /// <param name="TargetPlatform"></param>
        public static void InstallDriver(string DriverZipFile, string Version, Types.Platform TargetPlatform = Types.Platform.AutoDetect)
        {
            if (TargetPlatform == Types.Platform.AutoDetect)
            {
                TargetPlatform = Configuration.CurrentPlatform;
            }

            DriverInstallationDetails InstallationDetails = CheckInstallation(TargetPlatform);
            
            string TemporaryExtractedExecutable = string.Empty;
            string TemporaryExecutableName = string.Empty;

            switch(TargetPlatform)
            {
                case Types.Platform.Win32:
                    TemporaryExtractedExecutable = $"{Configuration.TemporaryDirectory}{Path.DirectorySeparatorChar}chromedriver.exe";
                    TemporaryExecutableName = "chromedriver.exe";
                    break;

                case Types.Platform.Linux32:
                    TemporaryExtractedExecutable = $"{Configuration.TemporaryDirectory}{Path.DirectorySeparatorChar}chromedriver";
                    TemporaryExecutableName = "chromedriver";
                    break;

                case Types.Platform.Linux64:
                    TemporaryExtractedExecutable = $"{Configuration.TemporaryDirectory}{Path.DirectorySeparatorChar}chromedriver";
                    TemporaryExecutableName = "chromedriver";
                    break;

                default:
                    throw new PlatformNotSupportedException();
            }

            if (File.Exists(TemporaryExtractedExecutable) == true)
            {
                File.Delete(TemporaryExtractedExecutable);
            }
             
            ZipFile Zip = ZipFile.Read(DriverZipFile);

            foreach (ZipEntry Entry in Zip)
            {
                if (Entry.FileName == TemporaryExecutableName)
                {
                    Entry.Extract(DriverZipFile, ExtractExistingFileAction.OverwriteSilently);
                    break;
                }
            }

            Zip.Dispose();

            if (InstallationDetails.IsInstalled == true)
            {
                UninstallDriver(TargetPlatform);
            }

            if (Directory.Exists(InstallationDetails.DriverPath) == false)
            {
                Directory.CreateDirectory(InstallationDetails.DriverPath);
            }

            File.Move(TemporaryExtractedExecutable, InstallationDetails.DriverExecutable);
            File.WriteAllText($"{InstallationDetails.DriverPath}{Path.DirectorySeparatorChar}current_version", Version);
            File.Delete(TemporaryExtractedExecutable);
        }

        /// <summary>
        /// Downloads and installs the latest driver form the internet
        /// </summary>
        /// <param name="TargetPlatform"></param>
        public static void InstallLatestDriver(Types.Platform TargetPlatform = Types.Platform.AutoDetect)
        {
            if (TargetPlatform == Types.Platform.AutoDetect)
            {
                TargetPlatform = Configuration.CurrentPlatform;
            }

            string CurrentLatestVersion = LatestVersion;
            
            string TemporaryZipFile = string.Empty;
            string DownloadURL = string.Empty;

            switch (TargetPlatform)
            {
                case Types.Platform.Win32:
                    TemporaryZipFile = $"{Configuration.TemporaryDirectory}{Path.DirectorySeparatorChar}chromedriver_win32.zip";
                    DownloadURL = Properties.Resources.Chrome_Win32API.Replace("%VERSION%", CurrentLatestVersion);
                    break;

                case Types.Platform.Linux32:
                    TemporaryZipFile = $"{Configuration.TemporaryDirectory}{Path.DirectorySeparatorChar}chromedriver_linux32.zip";
                    DownloadURL = Properties.Resources.Chrome_Linux32API.Replace("%VERSION%", CurrentLatestVersion);
                    break;

                case Types.Platform.Linux64:
                    TemporaryZipFile = $"{Configuration.TemporaryDirectory}{Path.DirectorySeparatorChar}chromedriver_linux64.zip";
                    DownloadURL = Properties.Resources.Chrome_Linux64API.Replace("%VERSION%", CurrentLatestVersion);
                    break;

                default:
                    throw new PlatformNotSupportedException();
            }

            if(File.Exists(TemporaryZipFile) == true)
            {
                File.Delete(TemporaryZipFile);
            }

            WebClient WebClient = new WebClient();
            WebClient.DownloadFile(DownloadURL, TemporaryZipFile);

            InstallDriver(TemporaryZipFile, CurrentLatestVersion, TargetPlatform);
            File.Delete(TemporaryZipFile);
        }
        /// <summary>
        /// Initializes the driver by installing it if it's not installed, and updating it if it's outdated
        /// </summary>
        /// <param name="TargetPlatform"></param>
        public static void Initialize(Types.Platform TargetPlatform = Types.Platform.AutoDetect)
        {
            if (TargetPlatform == Types.Platform.AutoDetect)
            {
                TargetPlatform = Configuration.CurrentPlatform;
            }

            DriverInstallationDetails InstallationDetails = CheckInstallation(TargetPlatform);

            if(InstallationDetails.IsInstalled == false)
            {
                InstallLatestDriver(TargetPlatform);
            }
            else
            {
                string CurrentLatestVersion = LatestVersion;
                if(CurrentLatestVersion != InstallationDetails.Version)
                {
                    UninstallDriver(TargetPlatform);
                    InstallLatestDriver(TargetPlatform;)
                }
            }
        }
    }

}
