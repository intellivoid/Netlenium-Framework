using Ionic.Zip;
using Mono.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using Microsoft.Scripting.Hosting;

namespace NetleniumRuntime
{
    /// <summary>
    /// The Parameters used for this Application
    /// </summary>
    internal class Parameters
    {
        /// <summary>
        /// The Netlenium Package to execute (.np file)
        /// </summary>
        public string PackageFile { get; set; }

        /// <summary>
        /// Skips the dependency check of the package
        /// </summary>
        public bool SkipDependencyCheck { get; set; }

        /// <summary>
        /// Indiciates if the Help menu should only be displayed
        /// </summary>
        public bool Help { get; set; }
    }

    /// <summary>
    /// Console Color Option Interface
    /// </summary>
    internal class ConsoleColorOption
    {
        /// <summary>
        /// Pulic Constructor
        /// </summary>
        /// <param name="useColor"></param>
        /// <param name="selectedColor"></param>
        public ConsoleColorOption(bool useColor, ConsoleColor selectedColor = ConsoleColor.White)
        {
            UseColor = useColor;
            SelectedColor = selectedColor;
        }
        
        /// <summary>
        /// The selected color to be displayed
        /// </summary>
        public ConsoleColor SelectedColor { get; set; }
        
        /// <summary>
        /// Indicates if the color formatting is going to be used
        /// </summary>
        public bool UseColor { get; set; }
    }
    
    /// <summary>
    /// Main Program
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Application Version
        /// </summary>
        private const string ApplicationVersion = "1.0.0.0";
        
        /// <summary>
        /// The paramerters used for this CLI
        /// </summary>
        private static Parameters _usedParameters;

        /// <summary>
        /// The Runtime ID that's currently set
        /// </summary>
        private static string _runtimeId;

        /// <summary>
        /// Handler for when the process is about to be terminated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ProcessExitHandler(object sender, EventArgs e)
        {
            ClearRuntime();
            Environment.Exit(0);
        }

        /// <summary>
        /// The directory of the executing 
        /// </summary>
        private static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(File.Exists($"{Path.GetDirectoryName(path)}{Path.DirectorySeparatorChar}Netlenium.dll") ? path : Process.GetCurrentProcess().MainModule.FileName);
            }
        }
        
        /// <summary>
        /// Parses the arguments and returns the paramerters given
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static void GetParamerters(IEnumerable<string> args)
        {
            _usedParameters = new Parameters();

            var p = new OptionSet
            {
                {
                    "h|help=", "Displays the help menu",
                    v => { _usedParameters.Help = v != null; }
                },
                {
                    "f|file=", "The Netlenium Package to execute (.np file)",
                    v => {
                        _usedParameters.PackageFile = v;
                    }
                },
                {
                    "skip-dependency-check", "Skips the dependency check of the package",
                    v => { _usedParameters.SkipDependencyCheck = v != null; }
                }
            };

            try
            {
                p.Parse(args);

                if(_usedParameters.Help)
                {
                    ShowHelp();
                    Environment.Exit(0);
                }

                if (_usedParameters.PackageFile != null) return;
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($@"Error: Missing paramerter ""file""{Environment.NewLine}");
                Console.ResetColor();
                ShowHelp();
                Environment.Exit(1);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                ShowHelp();
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Displays the help usage for this command-line application
        /// </summary>
        private static void ShowHelp()
        {
            Console.WriteLine(@"Netlenium Runtime");
            Console.WriteLine($@"Version {ApplicationVersion}{Environment.NewLine}");

            Console.WriteLine(@"usage: netlenium_re [options]");
            Console.WriteLine(@" options:");
            Console.WriteLine(@"     -h, --help                  Displays the help menu");
            Console.WriteLine(@"     -f, --file  required        The Netlenium Package to execute (.np file)");
            Console.WriteLine(@"     --skip-dependency-check     Skips the dependency check of the package");
        }
        
        /// <summary>
        /// Prints a formatted colored output to the command-line interface
        /// </summary>
        /// <param name="value"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        private static void Print(string value, ConsoleColorOption foregroundColor, ConsoleColorOption backgroundColor)
        {
            if (foregroundColor.UseColor)
            {
                Console.ForegroundColor = foregroundColor.SelectedColor;
            }

            if (backgroundColor.UseColor)
            {
                Console.BackgroundColor = backgroundColor.SelectedColor;
            }
            
            Console.Write(value);
            Console.ResetColor();
        }

        /// <summary>
        /// Main Method of Execution for Netlenium Runtime
        /// </summary>
        /// <param name="args"></param>
        [SuppressMessage("ReSharper", "RedundantArgumentDefaultValue")]
        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;
            GetParamerters(args);

            if (File.Exists(_usedParameters.PackageFile) == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($@"Error: The file ""{_usedParameters.PackageFile}"" does not exist!");
                Console.ResetColor();
                Environment.Exit(1);
            }

            var runtimeEnvironment = CreateEnvironment();

            // Extract the package contents
            try
            {
                var zip = ZipFile.Read(_usedParameters.PackageFile);
                Directory.CreateDirectory(runtimeEnvironment);
                zip.ExtractAll(runtimeEnvironment, ExtractExistingFileAction.OverwriteSilently);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"Error: There was an issue while trying to read the Netlenium Package");
                Console.ResetColor();
                Environment.Exit(1);
            }

            //Check required dependencies
            if(_usedParameters.SkipDependencyCheck == false)
            {
                CheckDependencies(runtimeEnvironment);
            }

            // Build from source
            var mainScript = $"{runtimeEnvironment}{Path.DirectorySeparatorChar}source{Path.DirectorySeparatorChar}main.py";
            var importedScript = $"{runtimeEnvironment}{Path.DirectorySeparatorChar}source{Path.DirectorySeparatorChar}c_main.py";
            var compiledScript = $"{Properties.Resources.ImportModules}{Environment.NewLine}{File.ReadAllText(mainScript)}";
            File.WriteAllText(importedScript, compiledScript);
            
            // TODO: Define Package Variables

            // Prepare the Script Runtime Host
            var pythonEngine = IronPython.Hosting.Python.CreateEngine();
            var scope = pythonEngine.CreateScope();
            scope.SetVariable("NetleniumRuntime", AssemblyDirectory);
            scope.SetVariable("RuntimeDirectory", $"{runtimeEnvironment}{Path.DirectorySeparatorChar}source");
            scope.SetVariable("LIB_Netlenium", "Netlenium.dll");
            scope.SetVariable("LIB_NetleniumDriver", "Netlenium.Driver.dll");
            var pythonScript = pythonEngine.CreateScriptSourceFromFile(importedScript);
            
            // Execute the python code
            try
            {
                pythonScript.Execute(scope);
            }
            catch (Exception exception)
            {
                Console.WriteLine();

                string errorOperations;
                
                try
                {
                    errorOperations = pythonEngine.GetService<ExceptionOperations>().FormatException(exception);
                }
                catch (Exception)
                {
                    errorOperations = "No Data";
                }
                
                Print("There was an unexcepted error while trying to run the package in Netlenium Runtime",
                    new ConsoleColorOption(true, ConsoleColor.White), new ConsoleColorOption(true, ConsoleColor.Red)
                );
                Console.WriteLine();
                
                Print("Exception Message: ", 
                    new ConsoleColorOption(true, ConsoleColor.White), new ConsoleColorOption(false)
                );
                Print($"{exception.Message}{Environment.NewLine}", 
                    new ConsoleColorOption(true, ConsoleColor.Gray), new ConsoleColorOption(false)
                );
                Print("Exception Source: ", 
                    new ConsoleColorOption(true, ConsoleColor.White), new ConsoleColorOption(false)
                );
                Print($"{exception.Source}{Environment.NewLine}", 
                    new ConsoleColorOption(true, ConsoleColor.Gray), new ConsoleColorOption(false)
                );
                Console.WriteLine();
                
                Print($"======== FRAMEWORK STACKTRACE ========{Environment.NewLine}", 
                    new ConsoleColorOption(true, ConsoleColor.White), new ConsoleColorOption(false)
                );
                Print(exception.StackTrace, 
                    new ConsoleColorOption(true, ConsoleColor.Red), new ConsoleColorOption(false)
                );
                Console.WriteLine(Environment.NewLine);
                
                Print($"======== EXCEPTION OPERATIONS ========{Environment.NewLine}", 
                    new ConsoleColorOption(true, ConsoleColor.White), new ConsoleColorOption(false)
                );
                Print(errorOperations, 
                    new ConsoleColorOption(true, ConsoleColor.Red), new ConsoleColorOption(false)
                );
                
                Console.WriteLine(Environment.NewLine);
                Environment.Exit(1);
            }
            
            // Once done, terminate the process
            Environment.Exit(0);
            
        }

        /// <summary>
        /// Checks the dependencies if they match the requirements for the package
        /// </summary>
        /// <param name="runtimeEnvironment"></param>
        private static void CheckDependencies(string runtimeEnvironment)
        {
            try
            {
                var dependencyNetlenium = ParseDependency($"{runtimeEnvironment}{Path.DirectorySeparatorChar}c_netlenium.xml");
                var dependencyNetleniumDriver = ParseDependency($"{runtimeEnvironment}{Path.DirectorySeparatorChar}c_netlenium.driver.xml");
                var dependencyChromeDriver = ParseDependency($"{runtimeEnvironment}{Path.DirectorySeparatorChar}c_netlenium.driver.chrome.xml");
                var dependencyGeckoFxLib = ParseDependency($"{runtimeEnvironment}{Path.DirectorySeparatorChar}c_netlenium.driver.geckofxlib.xml");

                var installedNetlenium = CheckDependency("Netlenium");
                var installedNetleniumDriver = CheckDependency("Netlenium.Driver");
                var installedChromeDriver = CheckDependency("Netlenium.Driver.Chrome");
                var installedGeckoFxLibDriver = CheckDependency("Netlenium.Driver.GeckoFXLib");

                CompareVersion(dependencyNetlenium.Version, installedNetlenium, "Netlenium");
                CompareVersion(dependencyNetleniumDriver.Version, installedNetleniumDriver, "Netlenium.Driver");
                CompareVersion(dependencyChromeDriver.Version, installedChromeDriver, "Netlenium.Driver.Chrome");
                CompareVersion(dependencyGeckoFxLib.Version, installedGeckoFxLibDriver, "Netlenium.Driver.GeckoFXLib");
            }
            catch(Exception)
            {
                Console.WriteLine(@"There was an error while trying to check the installed & required dependencies");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Parses and returns the dependency information given by the package
        /// </summary>
        /// <param name="configurationFile"></param>
        /// <returns></returns>
        private static LibraryDependency ParseDependency(string configurationFile)
        {
            try
            {
                var xmlDoc = new XmlDocument(); // Create an XML document object
                xmlDoc.Load(configurationFile); // Load the XML document from the specified file

                // Get elements
                var dependency = new LibraryDependency();
                
                var major = xmlDoc.GetElementsByTagName("major")[0].InnerText;
                var minor = xmlDoc.GetElementsByTagName("minor")[0].InnerText;
                var build = xmlDoc.GetElementsByTagName("build")[0].InnerText;
                var revision = xmlDoc.GetElementsByTagName("revision")[0].InnerText;

                dependency.Dependency = xmlDoc.GetElementsByTagName("dependency")[0].InnerText;
                dependency.Version = new Version($"{major}.{minor}.{build}.{revision}");
                dependency.FileName = xmlDoc.GetElementsByTagName("file_name")[0].InnerText;
                dependency.Internal = xmlDoc.GetElementsByTagName("internal")[0].InnerText;
                dependency.Publisher = xmlDoc.GetElementsByTagName("publisher")[0].InnerText;

                return dependency;
            }
            catch(Exception)
            {
                Console.WriteLine(@"The Netlenium Package does not contain a valid dependency file");
                Environment.Exit(1);
                
                // ReSharper disable once HeuristicUnreachableCode
                return null;
            }
        }

        /// <summary>
        /// Checks and returns the version of the installed dependency
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        private static Version CheckDependency(string dependency)
        {
            var dependencyFile = $"{AssemblyDirectory}{Path.DirectorySeparatorChar}{dependency}.dll";
            
            if (File.Exists(dependencyFile) == false)
            {
                Console.WriteLine($@"The required dependency for the framework cannot be found ""{dependencyFile}""");
                Environment.Exit(1);
                
                // ReSharper disable once HeuristicUnreachableCode
                return null;
            }

            var versionInformation = FileVersionInfo.GetVersionInfo(dependencyFile);
            return new Version($"{versionInformation.FileMajorPart}.{versionInformation.FileMinorPart}.{versionInformation.FileBuildPart}.{versionInformation.FilePrivatePart}");
        }

        /// <summary>
        /// Compares the version of two dependencies and throws a warning if they differ
        /// </summary>
        /// <param name="requiredVersion"></param>
        /// <param name="installedVersion"></param>
        /// <param name="dependencyName"></param>
        private static void CompareVersion(Version requiredVersion, Version installedVersion, string dependencyName)
        {
            var results = requiredVersion.CompareTo(installedVersion);
            if(results > 0)
            {
                Console.WriteLine($@"Warning: The installed version of ""{dependencyName}"" is newer than the required version for this package.");
            }
            else if(results < 0)
            {
                Console.WriteLine($@"Warning: The installed version of ""{dependencyName}"" is older than the required version for this package.");
            }
        }

        /// <summary>Color
        /// Creates a runtime environment
        /// </summary>
        /// <returns></returns>
        private static string CreateEnvironment()
        {
            var randomObject = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            _runtimeId = new string(Enumerable.Repeat(chars, 12).Select(s => s[randomObject.Next(s.Length)]).ToArray());

            if (Directory.Exists($"{Netlenium.Configuration.RuntimeDirectory}{Path.DirectorySeparatorChar}{_runtimeId}"))
            {
                try
                {
                    Directory.Delete($"{Netlenium.Configuration.RuntimeDirectory}{Path.DirectorySeparatorChar}{_runtimeId}");
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(@"Error: Duplicate Runtime");
                    Console.ResetColor();
                }
            }

            Directory.CreateDirectory($"{Netlenium.Configuration.RuntimeDirectory}{Path.DirectorySeparatorChar}{_runtimeId}");

            return $"{Netlenium.Configuration.RuntimeDirectory}{Path.DirectorySeparatorChar}{_runtimeId}";
        }

       
        /// <summary>
        /// Clears the Runtime environment
        /// </summary>
        private static void ClearRuntime()
        {
            if (!Directory.Exists(
                $"{Netlenium.Configuration.RuntimeDirectory}{Path.DirectorySeparatorChar}{_runtimeId}")) return;
            
            try
            {
                Directory.Delete($"{Netlenium.Configuration.RuntimeDirectory}{Path.DirectorySeparatorChar}{_runtimeId}", true);
            }
            catch (Exception)
            {
                Console.WriteLine(@"Warning: The runtime cannot be closed properly");
            }
        }
        
    }
}
