using Ionic.Zip;
using System.IO;
using System.Net;

namespace Netlenium.Driver.Chrome
{
    /// <summary>
    /// Driver Manager for Chrome
    /// </summary>
    class Driver
    {
        /// <summary>
        /// Indicates if the Chrome Driver is installed or not
        /// </summary>
        public static bool IsInstalled
        {
            get
            {
                string ChromeDriverDirectory = $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome";
                string ChromeDriverCurrentVersionFile = $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome{Path.DirectorySeparatorChar}current_version";
                string ChromeDriverExecutable = $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome{Path.DirectorySeparatorChar}chromedriver.exe";

                if (Directory.Exists(ChromeDriverDirectory) == false)
                {
                    return false;
                }

                if(File.Exists(ChromeDriverCurrentVersionFile) == false)
                {
                    return false;
                }

                if (File.Exists(ChromeDriverExecutable) == false)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// The current version of the chrome driver that's been installed
        /// </summary>
        public static string CurrentVersion
        {
            get
            {
                string ChromeDriverCurrentVersionFile = $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome{Path.DirectorySeparatorChar}current_version";

                if (File.Exists(ChromeDriverCurrentVersionFile) == false)
                {
                    throw new FileNotFoundException("The file which contains the current version information for Chrome Driver was not found.");
                }

                return File.ReadAllText(ChromeDriverCurrentVersionFile);
            }
        }

        /// <summary>
        /// Returns the latest version number from vendor
        /// </summary>
        public static string LatestVersion
        {
            get
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Properties.Resources.ChromeDriverUpdateURL);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Determines if the driver is outdated
        /// </summary>
        public static bool IsOutdated
        {
            get
            {

                if (CurrentVersion == LatestVersion)
                {
                    return false;
                }

                return true;

            }
        }

        /// <summary>
        /// Returns the location of the Chrome Driver executable file
        /// </summary>
        public static string DriverDirectory
        {
            get
            {
                if (IsInstalled == false)
                {
                    throw new DriverNotInstalledException();
                }

                return $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome";
            }
        }

        /// <summary>
        /// Installs the Chrome Driver if not already installed
        /// </summary>
        public static void Install()
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver.Chrome", "Preparing installation of Chrome Driver");

            if(IsInstalled == true)
            {
                Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver.Chrome", "The Installation failed because the Chrome Driver is already installed");
                throw new DriverAlreadyInstalledException();
            }

            string LatestVersionStr = LatestVersion;
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver.Chrome", $"The latest version of the Chrome Driver for Win32 is {LatestVersionStr}");

            string TemporaryDownloadDirectory = $"{Configuration.ApplicationDataDirectory}{Path.DirectorySeparatorChar}tmp";
            string TemporaryDownload = $"{TemporaryDownloadDirectory}{Path.DirectorySeparatorChar}chrome_driver.zip";
            string TemporaryExtractedFile = $"{TemporaryDownloadDirectory}{Path.DirectorySeparatorChar}chromedriver.exe";
            WebClient WebClient = new WebClient();

            if (Directory.Exists(TemporaryDownloadDirectory) == false)
            {
                Directory.CreateDirectory(TemporaryDownloadDirectory);
            }

            if(File.Exists(TemporaryDownload) == true)
            {
                File.Delete(TemporaryDownload);
            }

            if(File.Exists(TemporaryExtractedFile) == true)
            {
                File.Delete(TemporaryExtractedFile);
            }

            string DownloadURL = $"https://chromedriver.storage.googleapis.com/{LatestVersionStr}/chromedriver_win32.zip";
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver.Chrome", $"Downloading Chrome Driver from {DownloadURL}");
            WebClient.DownloadFile(DownloadURL, TemporaryDownload);

            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver.Chrome", "Extracting chromedriver.exe from chrome_driver.zip");
            ZipFile Zip = ZipFile.Read(TemporaryDownload);

            foreach (ZipEntry Entry in Zip)
            {
                if (Entry.FileName == "chromedriver.exe")
                {
                    Entry.Extract(TemporaryDownloadDirectory, ExtractExistingFileAction.OverwriteSilently);
                    break;
                }
            }

            Zip.Dispose();

            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver.Chrome", "Installing chromedriver.exe ...");
            string ChromeDriverDirectory = $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome";
            string ChromeDriverCurrentVersionFile = $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome{Path.DirectorySeparatorChar}current_version";
            string ChromeDriverExecutable = $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome{Path.DirectorySeparatorChar}chromedriver.exe";

            if (Directory.Exists(ChromeDriverDirectory) == false)
            {
                Directory.CreateDirectory(ChromeDriverDirectory);
            }
            
            File.Move(TemporaryExtractedFile, ChromeDriverExecutable);
            File.WriteAllText(ChromeDriverCurrentVersionFile, LatestVersionStr);
            File.Delete(TemporaryDownload);
            File.Delete(TemporaryExtractedFile);
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver.Chrome", $"Installed");
        }

        /// <summary>
        /// Updates the driver to the latest version
        /// </summary>
        /// <param name="SkipVersionCheck"></param>
        public static void Update(bool SkipVersionCheck = false)
        {
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver.Chrome", "Preparing to update Chrome Driver");
            if (IsInstalled == false)
            {
                Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver.Chrome", "The Chrome Driver cannot be updated because it isn't installed");
                throw new DriverNotInstalledException();
            }

            if(SkipVersionCheck == false)
            {
                if (IsOutdated == false)
                {
                    Logging.WriteEntry(Types.LogType.Error, "Netlenium.Driver.Chrome", "The Chrome Driver cannot be updated because it's already up to date");
                    throw new DriverAlreadyUpToDate();
                }
            }

            string ChromeDriverDirectory = $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome";
            string ChromeDriverCurrentVersionFile = $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome{Path.DirectorySeparatorChar}current_version";
            string ChromeDriverExecutable = $"{Configuration.DriverDirectory}{Path.DirectorySeparatorChar}chrome{Path.DirectorySeparatorChar}chromedriver.exe";

            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver.Chrome", "Uninstalling ChromeDriver");

            if (File.Exists(ChromeDriverCurrentVersionFile) == true)
            {
                File.Delete(ChromeDriverCurrentVersionFile);
            }

            if(File.Exists(ChromeDriverExecutable) == true)
            {
                File.Delete(ChromeDriverExecutable);
            }

            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver.Chrome", "Installing latest Chrome Driver version");
            Install();
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver.Chrome", "Chrome Driver has been updated successfully");
        }
        
    }
}