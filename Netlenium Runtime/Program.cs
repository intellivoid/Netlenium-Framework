using CommandLine;
using Ionic.Zip;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NetleniumRuntime
{
    /// <summary>
    /// The command line arguments
    /// </summary>
    public class CLIOptions
    {
        [Option('f', "file", Required = true, HelpText = "Netlenium Package File Location (.np File)")]
        public string File { get; set; }

        [Option("skip-dependency-check", Required = false, Default = false, HelpText = "Skips the dependency check (No warnings/errors will be thrown for version mismatches)")]
        public bool SkipDependencyCheck { get; set; }

    }

    class Program
    {
        /// <summary>
        /// The Runtime ID that's currently set
        /// </summary>
        private static string RuntimeID;

        /// <summary>
        /// Handler for when the process is about to be terminated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void ProcessExitHandler(object sender, EventArgs e)
        {
            ClearRuntime();
            Environment.Exit(0);
        }

        /// <summary>
        /// The directory of the executing 
        /// </summary>
        public static string AssemblyDirectory
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
        /// Main Program which parses the arguements
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(string[] args)
        {
            var Opotions = Parser.Default.ParseArguments<CLIOptions>(args).WithParsed(
                options => {
                    Main(options);
                });
        }

        /// <summary>
        /// Main Method of Execution for Netlenium Runtime
        /// </summary>
        /// <param name="Options"></param>
        static void Main(CLIOptions Options)
        {
            AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;
            string PackageFile = Convert.ToString(Options.File);

            if (File.Exists(PackageFile) == false)
            {
                Console.WriteLine(PackageFile);
                Console.WriteLine($"Error: The file \"{PackageFile}\" does not exist!", System.Drawing.Color.Red);
                Environment.Exit(1);
            }

            string RuntimeEnvironment = CreateEnvironment();

            // Extract the package contents
            try
            {
                ZipFile zip = ZipFile.Read(PackageFile);
                Directory.CreateDirectory(RuntimeEnvironment);
                zip.ExtractAll(RuntimeEnvironment, ExtractExistingFileAction.OverwriteSilently);
            }
            catch (Exception)
            {
                Console.WriteLine("Error: There was an issue while trying to read the Netlenium Package", System.Drawing.Color.Red);
                Environment.Exit(1);
            }

            // Check required dependencies
            if(Options.SkipDependencyCheck == false)
            {
                CheckDependencies(RuntimeEnvironment);
            }

            // Build from source
            string MainScript = $"{RuntimeEnvironment}{Path.DirectorySeparatorChar}source{Path.DirectorySeparatorChar}main.py";
            string ImportedScript = $"{RuntimeEnvironment}{Path.DirectorySeparatorChar}source{Path.DirectorySeparatorChar}c_main.py";
            string CompiledScript = $"{Properties.Resources.ImportModules}{Environment.NewLine}{File.ReadAllText(MainScript)}";
            File.WriteAllText(ImportedScript, CompiledScript);
            
            ScriptEngine pythonEngine = IronPython.Hosting.Python.CreateEngine();
            ScriptScope scope = pythonEngine.CreateScope();
            scope.SetVariable("NetleniumRuntime", AssemblyDirectory);
            scope.SetVariable("LIB_Netlenium", "Netlenium.dll");
            scope.SetVariable("LIB_NetleniumDriver", "Netlenium.Driver.dll");
            ScriptSource pythonScript = pythonEngine.CreateScriptSourceFromFile(ImportedScript);
            pythonScript.Execute(scope);

            Environment.Exit(0);
            
        }

        /// <summary>
        /// Checks the dependencies if they match the requirements for the package
        /// </summary>
        /// <param name="RuntimeEnvironment"></param>
        private static void CheckDependencies(string RuntimeEnvironment)
        {
            try
            {
                LibraryDependency DependencyNetlenium = ParseDependency($"{RuntimeEnvironment}{Path.DirectorySeparatorChar}c_netlenium.xml");
                LibraryDependency DependencyNetleniumDriver = ParseDependency($"{RuntimeEnvironment}{Path.DirectorySeparatorChar}c_netlenium.driver.xml");
                LibraryDependency DependencyChromeDriver = ParseDependency($"{RuntimeEnvironment}{Path.DirectorySeparatorChar}c_netlenium.driver.chrome.xml");
                LibraryDependency DependencyGeckoFXLib = ParseDependency($"{RuntimeEnvironment}{Path.DirectorySeparatorChar}c_netlenium.driver.geckofxlib.xml");

                Version InstalledNetlenium = CheckDependency("Netlenium");
                Version InstalledNetleniumDriver = CheckDependency("Netlenium.Driver");
                Version InstalledChromeDriver = CheckDependency("Netlenium.Driver.Chrome");
                Version InstalledGeckoFXLibDriver = CheckDependency("Netlenium.Driver.GeckoFXLib");

                CompareVersion(DependencyNetlenium.Version, InstalledNetlenium, "Netlenium");
                CompareVersion(DependencyNetleniumDriver.Version, InstalledNetleniumDriver, "Netlenium.Driver");
                CompareVersion(DependencyChromeDriver.Version, InstalledChromeDriver, "Netlenium.Driver.Chrome");
                CompareVersion(DependencyGeckoFXLib.Version, InstalledGeckoFXLibDriver, "Netlenium.Driver.GeckoFXLib");
            }
            catch(Exception)
            {
                Console.WriteLine("There was an error while trying to check the installed & required dependencies");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Parses and returns the dependency information given by the package
        /// </summary>
        /// <param name="ConfigurationFile"></param>
        /// <returns></returns>
        private static LibraryDependency ParseDependency(string ConfigurationFile)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object
                xmlDoc.Load(ConfigurationFile); // Load the XML document from the specified file

                // Get elements
                LibraryDependency Dependency = new LibraryDependency();
                
                string Major = xmlDoc.GetElementsByTagName("major")[0].InnerText;
                string Minor = xmlDoc.GetElementsByTagName("minor")[0].InnerText;
                string Build = xmlDoc.GetElementsByTagName("build")[0].InnerText;
                string Revision = xmlDoc.GetElementsByTagName("revision")[0].InnerText;

                Dependency.Dependency = xmlDoc.GetElementsByTagName("dependency")[0].InnerText;
                Dependency.Version = new Version($"{Major}.{Minor}.{Build}.{Revision}");
                Dependency.FileName = xmlDoc.GetElementsByTagName("file_name")[0].InnerText;
                Dependency.Internal = xmlDoc.GetElementsByTagName("internal")[0].InnerText;
                Dependency.Publisher = xmlDoc.GetElementsByTagName("publisher")[0].InnerText;

                return Dependency;
            }
            catch(Exception)
            {
                Console.WriteLine("The Netlenium Package does not contain a valid dependency file");
                Environment.Exit(1);
                return null;
            }
        }

        /// <summary>
        /// Checks and returns the version of the installed dependency
        /// </summary>
        /// <param name="Dependency"></param>
        /// <returns></returns>
        private static Version CheckDependency(string Dependency)
        {
            string DependencyFile = $"{AssemblyDirectory}{Path.DirectorySeparatorChar}{Dependency}.dll";

            if (File.Exists(DependencyFile) == false)
            {
                Console.WriteLine($"The required dependency for the framework cannot be found \"{DependencyFile}\"");
                Environment.Exit(1);
                return null;
            }

            FileVersionInfo VersionInformation = FileVersionInfo.GetVersionInfo($"{AssemblyDirectory}{Path.DirectorySeparatorChar}{Dependency}.dll");
            return new Version($"{VersionInformation.FileMajorPart}.{VersionInformation.FileMinorPart}.{VersionInformation.FileBuildPart}.{VersionInformation.FilePrivatePart}");
        }

        /// <summary>
        /// Compares the version of two dependencies and throws a warning if they differ
        /// </summary>
        /// <param name="RequiredVersion"></param>
        /// <param name="InstalledVersion"></param>
        /// <param name="DependencyName"></param>
        private static void CompareVersion(Version RequiredVersion, Version InstalledVersion, string DependencyName)
        {
            var Results = RequiredVersion.CompareTo(InstalledVersion);
            if(Results > 0)
            {
                Console.WriteLine($"Warning: The installed version of \"{DependencyName}\" is newer than the required version for this package.");
            }
            else if(Results < 0)
            {
                Console.WriteLine($"Warning: The installed version of \"{DependencyName}\" is older than the required version for this package.");
            }
        }

        /// <summary>
        /// Creates a runtime environment
        /// </summary>
        /// <returns></returns>
        private static string CreateEnvironment()
        {
            Random RandomObject = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            RuntimeID = new string(Enumerable.Repeat(chars, 12).Select(s => s[RandomObject.Next(s.Length)]).ToArray());

            if (Directory.Exists($"{Netlenium.Configuration.RuntimeDirectory}{Path.DirectorySeparatorChar}{RuntimeID}"))
            {
                try
                {
                    Directory.Delete($"{Netlenium.Configuration.RuntimeDirectory}{Path.DirectorySeparatorChar}{RuntimeID}");
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: Duplicate Runtime", System.Drawing.Color.Red);
                }
            }

            Directory.CreateDirectory($"{Netlenium.Configuration.RuntimeDirectory}{Path.DirectorySeparatorChar}{RuntimeID}");

            return $"{Netlenium.Configuration.RuntimeDirectory}{Path.DirectorySeparatorChar}{RuntimeID}";
        }

       
        /// <summary>
        /// Clears the Runtime environment
        /// </summary>
        private static void ClearRuntime()
        {
            if (Directory.Exists($"{Netlenium.Configuration.RuntimeDirectory}{Path.DirectorySeparatorChar}{RuntimeID}"))
            {
                try
                {
                    //Directory.Delete($"{Netlenium.Configuration.RuntimeDirectory}{Path.DirectorySeparatorChar}{RuntimeID}", true);
                }
                catch (Exception)
                {
                    Console.WriteLine("Warning: The runtime cannot be closed properly");
                }
            }
        }
        
    }
}
