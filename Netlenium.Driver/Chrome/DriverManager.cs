using System;
using System.IO;
using System.Net;
using Ionic.Zip;
using Netlenium.Driver.WebAPI.Google;

namespace Netlenium.Driver.Chrome
{
    /// <summary>
    /// Chrome Driver Manager
    /// </summary>
    public class DriverManager : IDriverManager
    {
        /// <inheritdoc />
        /// <summary>
        /// The current platform the Driver Manager is targeting
        /// </summary>
        public PlatformType TargetPlatform { get; set; }

        /// <summary>
        /// Public Constructor
        /// </summary>
        public DriverManager()
        {
            TargetPlatform = PlatformType.AutoDetect;
        }

        /// <summary>
        /// Resolves the current platform if the TargetPlatform is set to AutoDetect
        /// </summary>
        private void ResolvePlatform()
        {
            if (TargetPlatform == PlatformType.AutoDetect)
            {
                TargetPlatform = Utilities.CurrentPlatform;
            }
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Gets the latest version of the driver
        /// </summary>
        /// <exception cref="T:Netlenium.Driver.DriverManagerException"></exception>
        public string LatestVersion
        {
            get
            {
                try
                {
                    var httpWebClient = new WebClient();
                    return httpWebClient.DownloadString("https://chromedriver.storage.googleapis.com/LATEST_RELEASE");
                }
                catch (Exception e)
                {
                    throw new DriverManagerException($"Cannot fetch the latest version, {e.Message}");
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Determines if the driver is installed on the machine or not
        /// </summary>
        public bool IsInstalled => File.Exists(DriverExecutablePath) && File.Exists($"{DriverPath}{Path.DirectorySeparatorChar}current_version");

        /// <inheritdoc />
        /// <summary>
        /// Returns the current installation version if the driver is installed
        /// </summary>
        /// <exception cref="T:Netlenium.Driver.DriverManagerException"></exception>
        public string CurrentInstallationVersion
        {
            get
            {
                if (IsInstalled == false)
                {
                    throw new DriverManagerException("The driver installation was not found");
                }
                
                return $"{DriverPath}{Path.DirectorySeparatorChar}current_version";
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the path of the driver files and executable controller
        /// </summary>
        /// <exception cref="T:Netlenium.Driver.UnsupportedPlatformException"></exception>
        public string DriverPath
        {
            get
            {
                ResolvePlatform();
                
                switch (TargetPlatform)
                {
                    case PlatformType.Windows:
                        return $"{Paths.DriverDirectory}{Path.DirectorySeparatorChar}chrome_win32";
                    
                    case PlatformType.Linux32:
                        return $"{Paths.DriverDirectory}{Path.DirectorySeparatorChar}linux32";
                    
                    case PlatformType.Linux64:
                        return $"{Paths.DriverDirectory}{Path.DirectorySeparatorChar}linux64";
                    
                    case PlatformType.AutoDetect:
                        throw new UnsupportedPlatformException("Target platform cannot be AutoDetect");
                    
                    default:
                        throw new UnsupportedPlatformException($"This driver is not supported for {TargetPlatform}");
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the Executable Name for the Driver
        /// </summary>
        /// <exception cref="T:Netlenium.Driver.UnsupportedPlatformException"></exception>
        public string DriverExecutableName
        {
            get
            {
                ResolvePlatform();

                switch (TargetPlatform)
                {
                    case PlatformType.Windows:
                        return "chromedriver.exe";
                    
                    case PlatformType.Linux32:
                        return "chromedriver";
                    
                    case PlatformType.Linux64:
                        return "chromedriver";

                    case PlatformType.AutoDetect:
                        throw new UnsupportedPlatformException("Target platform cannot be AutoDetect");
                    
                    default:
                        throw new UnsupportedPlatformException($"This driver is not supported for {TargetPlatform}");
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the full path of the executable file for the driver
        /// </summary>
        public string DriverExecutablePath => $"{DriverPath}{Path.DirectorySeparatorChar}{DriverExecutableName}";

        /// <inheritdoc />
        /// <summary>
        /// Downloads the latest driver 
        /// </summary>
        public void InstallLatestDriver()
        {
            Content resource;
            var cacheLatestVersion = LatestVersion;

            ResolvePlatform();
            
            switch (TargetPlatform)
            {
                case PlatformType.Windows:
                    resource = Storage.FetchResource($"{cacheLatestVersion}/chromedriver_win32.zip");
                    break;
                
                case PlatformType.Linux32:
                    resource = Storage.FetchResource($"{cacheLatestVersion}/chromedriver_linux32.zip");
                    break;
                
                case PlatformType.Linux64:
                    resource = Storage.FetchResource($"{cacheLatestVersion}/chromedriver_linux64.zip");
                    break;
                
                case PlatformType.AutoDetect:
                    throw new UnsupportedPlatformException("Target platform cannot be AutoDetect");
                    
                default:
                    throw new UnsupportedPlatformException($"This driver is not supported for {TargetPlatform}");
            }

            if (File.Exists($"{Paths.TemporaryDirectory}{Path.DirectorySeparatorChar}chromedriver_tmp.zip"))
            {
                try
                {
                    File.Delete($"{Paths.TemporaryDirectory}{Path.DirectorySeparatorChar}chromedriver_tmp.zip");
                }
                catch (Exception)
                {
                    throw new DriverManagerException("Cannot delete the already existing temporary download archive");
                }     
            }

            var httpWebClient = new WebClient();
            httpWebClient.DownloadFile(resource.AccessLocation.ToString(), $"{Paths.TemporaryDirectory}{Path.DirectorySeparatorChar}chromedriver_tmp.zip");
            
            var zip  = ZipFile.Read($"{Paths.TemporaryDirectory}{Path.DirectorySeparatorChar}chromedriver_tmp.zip");
            
            foreach (var entry in zip)
            {
                if (entry.FileName != DriverExecutableName) continue;
                if (entry.IsDirectory) continue;
                entry.Extract($"{Paths.TemporaryDirectory}", ExtractExistingFileAction.OverwriteSilently);
                break;
            }

            zip.Dispose();
            
            UninstallDriver();

            if (Directory.Exists(DriverPath) == false)
            {
                Directory.CreateDirectory(DriverPath);
            }

            File.WriteAllText($"{DriverPath}{Path.DirectorySeparatorChar}current_version", cacheLatestVersion);
            File.Copy($"{Paths.TemporaryDirectory}{Path.DirectorySeparatorChar}{DriverExecutableName}", DriverExecutablePath);
            File.Delete($"{Paths.TemporaryDirectory}{Path.DirectorySeparatorChar}{DriverExecutableName}");
            
            if (!(TargetPlatform == PlatformType.Linux32 || TargetPlatform == PlatformType.Linux64)) return;

            try
            {
                Utilities.GiveExecutablePermissions(DriverExecutablePath);
            }
            catch (Exception e)
            {
                throw new DriverManagerException($"Failed to give executable permissions to driver, {e.Message}");
            }
            
        }

        /// <inheritdoc />
        /// <summary>
        /// Uninstalls the driver from the machine
        /// </summary>
        /// <param name="force"></param>
        /// <exception cref="T:Netlenium.Driver.DriverManagerException"></exception>
        public void UninstallDriver(bool force = true)
        {
            if (force == false)
            {
                if (IsInstalled == false)
                {
                    throw new DriverManagerException("The driver cannot be uninstalled because it's arleady installed");
                } 
            }

            try
            {
                File.Delete($"{DriverPath}{Path.DirectorySeparatorChar}current_version");
            }
            catch (Exception e)
            {
                if (force == false)
                {
                    throw new DriverManagerException($"Cannot remove the driver installation version details, {e.Message}");
                }
            }

            try
            {
                File.Delete(DriverExecutablePath);
            }
            catch (Exception e)
            {
                if (force == false)
                {
                    throw new DriverManagerException($"Cannot remove the driver executable file, {e.Message}");
                }
            }

            try
            {
                Directory.Delete(DriverPath, true);
            }
            catch (Exception e)
            {
                if (force == false)
                {
                    throw new DriverManagerException($"Cannot delete driver data directory, {e.Message}");
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Determines if the driver is installed, if so then it will check if it's out of date
        /// </summary>
        public void Initialize()
        {
            if (IsInstalled == false)
            {
                InstallLatestDriver();
                return;
            }

            if (CurrentInstallationVersion == LatestVersion) return;
            
            UninstallDriver();
            InstallLatestDriver();
        }

    }
}