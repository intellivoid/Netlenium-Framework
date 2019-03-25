using Ionic.Zip;
using Mono.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace NetleniumBuild
{
    /// <summary>
    /// The paramerters used for this Command Line Application
    /// </summary>
    internal class Paramerters
    {
        /// <summary>
        /// The Source Code Directory Location
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Prompts the user before exiting the process
        /// </summary>
        public bool Prompt { get; set; }

        /// <summary>
        /// Indicates if the help paramerter was used
        /// </summary>
        public bool Help { get; set; }
    }

    /// <summary>
    /// Main Program Class
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The used paramerters for this command line interface
        /// </summary>
        private static Paramerters _usedParameters;

        /// <summary>
        /// The hard-coded Application Version
        /// </summary>
        private const string ApplicationVersion = "1.0.0.2";

        /// <summary>
        /// Reads the arguments given via the command-line and parses into objective paramerters
        /// </summary>
        /// <param name="args"></param>
        private static void GetParamerters(IEnumerable<string> args)
        {
            _usedParameters = new Paramerters();

            var p = new OptionSet
            {
                {
                    "s|source=", "The source directory which contains the main python script and package metadata",
                    v => {
                        _usedParameters.Source = v;
                    }
                },
                {
                    "p|prompt=", "Prompts the user before exiting the process",
                    v => {
                         if(v == null)
                         {
                            _usedParameters.Prompt = false;
                         }
                         else
                         {
                            if((string)v.ToLower() == "true")
                            {
                                _usedParameters.Prompt = true;
                            }
                            else
                            {
                                _usedParameters.Prompt = false;
                            }
                         }
                    }
                },
                {
                    "h|help=", "Displays the help menu",
                    v => { _usedParameters.Help = v != null; }
                }
            };

            try
            {
                p.Parse(args);

                if(_usedParameters.Help)
                {
                    ShowHelp();
                    RequestExit(1);
                }

                if (_usedParameters.Source != null) return;
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($@"Error: Missing paramerter ""source""{Environment.NewLine}");
                Console.ResetColor();
                ShowHelp();
                RequestExit(2);
            }
            catch (Exception)
            {
                ShowHelp();
                RequestExit(11);
            }
        }

        /// <summary>
        /// Requests the user to exit the process
        /// </summary>
        /// <param name="ExitCode"></param>
        private static void RequestExit(int ExitCode)
        {
            if(_usedParameters.Prompt == true)
            {
                Console.WriteLine();
                Console.WriteLine($"Process completed with exit code {ExitCode}");
                Console.WriteLine("Press Return to exit ...");
                Console.ReadLine();
            }

            Environment.Exit(ExitCode);
        }

        /// <summary>
        /// Displays the help menu
        /// </summary>
        private static void ShowHelp()
        {
            Console.WriteLine(@"Netlenium Package Builder");
            Console.WriteLine($@"Version {ApplicationVersion}{Environment.NewLine}");

            Console.WriteLine(@"usage: npbuild [options]");
            Console.WriteLine(@" options:");
            Console.WriteLine(@"     -h, --help                  Displays the help menu");
            Console.WriteLine(@"     -p, --prompt  true/false    Prompts the user before exiting the process");
            Console.WriteLine(@"     -s, --source  required      The source directory which contains the main python script and package metadata");
        }

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
                return Path.GetDirectoryName(File.Exists($"{Path.GetDirectoryName(path)}{Path.DirectorySeparatorChar}Netlenium.dll") ? path : Process.GetCurrentProcess().MainModule.FileName);
            }
        }

        /// <summary>
        /// Main Program for Building the Package
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            GetParamerters(args);

            Console.WriteLine($@"Netlenium package Builder v{ApplicationVersion}");
            Console.WriteLine(@"Written by Zi Xing Narrakas");
            Console.WriteLine();

            CheckResources(_usedParameters.Source);
            
            try
            {
                ReadMetaInformation(_usedParameters.Source);
            }
            catch(Exception exception)
            {
                Print(MessageType.Information, $"Cannot read package.json: {exception.Message}");
                RequestExit(3);
            }

            BuildPackage(_usedParameters.Source);

            Print(MessageType.Success, "Package built successfully");
            RequestExit(0);

        }

        /// <summary>
        /// Prints to the console
        /// </summary>
        /// <param name="type"></param>
        /// <param name="output"></param>
        private static void Print(MessageType type, string output)
        {
            switch(type)
            {
                case MessageType.Out:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(@"[ ~~~ ]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(@": ");
                    Console.ResetColor();
                    Console.WriteLine(output);
                    break;

                case MessageType.Information:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(@"[  !  ]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(@": ");
                    Console.ResetColor();
                    Console.WriteLine(output);
                    break;

                case MessageType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(@"[  !  ]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(@": ");
                    Console.ResetColor();
                    Console.WriteLine(output);
                    break;
                    
                case MessageType.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(@"[  !  ]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(@": ");
                    Console.ResetColor();
                    Console.WriteLine(output);
                    break; 

                case MessageType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(@"[  X  ]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(@": ");
                    Console.ResetColor();
                    Console.WriteLine(output);
                    break;
                    
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        /// <summary>
        /// Builds the pacakge assuming all previous checks has passed
        /// </summary>
        /// <param name="source"></param>
        private static void BuildPackage(string source)
        {
            if (File.Exists($"{source}.np"))
            {
                Print(MessageType.Out, $"Deleting old {source}.np file");
                try
                {
                    File.Delete($"{source}.np");
                }
                catch (Exception exception)
                {
                    Print(MessageType.Error, $"Cannot delete the old Netlenium package: {exception.Message}");
                    RequestExit(5);
                }
            }

            try
            {
                Print(MessageType.Out, $"Building package {source}.np");
                using (var zip = new ZipFile())
                {
                    Print(MessageType.Out, "Adding Source Directory");
                    zip.AddDirectory($"{source}", "source");
                    zip.AddEntry("c_netlenium.xml", ConstructDependency("Netlenium"));
                    zip.AddEntry("c_netlenium.driver.xml", ConstructDependency("Netlenium.Driver"));
                    zip.AddEntry("c_netlenium.driver.chrome.xml", ConstructDependency("Netlenium.Driver.Chrome"));
                    zip.AddEntry("c_netlenium.driver.geckofxlib.xml", ConstructDependency("Netlenium.Driver.GeckoFXLib"));
                    Print(MessageType.Out, "Writing package to disk");
                    zip.Save($"{source}.np");
                }
            }
            catch (Exception exception)
            {
                Print(MessageType.Error, $"There was an error while trying to build the pacakge: {exception.Message}");
                RequestExit(4);
            }
        }

        /// <summary>
        /// Reads the packages meta information
        /// </summary>
        /// <param name="source"></param>
        private static void ReadMetaInformation(string source)
        {
            Print(MessageType.Out, "Reading package.json");
            var jsonPackageMetaInformation = JObject.Parse(File.ReadAllText($"{source}{Path.DirectorySeparatorChar}package.json"));

            if ((string)jsonPackageMetaInformation["name"] == null)
            {
                Print(MessageType.Error, "package.json is missing \"name\"");
                RequestExit(6);
            }

            if ((string)jsonPackageMetaInformation["version"] == null)
            {
                Print(MessageType.Error, "package.json is missing \"version\"");
                RequestExit(7);
            }

            Print(MessageType.Information, $"Name: {(string)jsonPackageMetaInformation["name"]}");
            Print(MessageType.Information, $"Version: {(string)jsonPackageMetaInformation["version"]}");

            Print(MessageType.Information,
                (string) jsonPackageMetaInformation["author"] != null
                    ? $"Author: {(string) jsonPackageMetaInformation["author"]}"
                    : "Author: None");

            Print(MessageType.Information,
                (string) jsonPackageMetaInformation["company"] != null
                    ? $"Company: {(string) jsonPackageMetaInformation["company"]}"
                    : "Company: None");
        }

        /// <summary>
        /// Constructs the dependency information (XML Format)
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        private static string ConstructDependency(string dependency)
        {
            var dependencyFile = $"{AssemblyDirectory}{Path.DirectorySeparatorChar}{dependency}.dll";

            if(File.Exists(dependencyFile) == false)
            {
                Print(MessageType.Error, $"The required dependency for the framework cannot be found \"{dependencyFile}\"");
            }

            Print(MessageType.Out, $"Constructing Dependency Information for \"{dependency}\"");

            var data = new NameValueCollection();

            var versionInformation = FileVersionInfo.GetVersionInfo($"{AssemblyDirectory}{Path.DirectorySeparatorChar}{dependency}.dll");

            data.Add("dependency", dependency);
            data.Add("major", Convert.ToString(versionInformation.FileMajorPart));
            data.Add("minor", Convert.ToString(versionInformation.FileMinorPart));
            data.Add("build", Convert.ToString(versionInformation.FileBuildPart));
            data.Add("revision", Convert.ToString(versionInformation.FilePrivatePart));
            data.Add("file_name", Convert.ToString(versionInformation.OriginalFilename));
            data.Add("internal", Convert.ToString(versionInformation.InternalName));
            data.Add("publisher", Convert.ToString(versionInformation.CompanyName));

            var results = new XElement("Netlenium_Framework", data.AllKeys.Select(o => new XElement(o, data[o])));
            return results.ToString();

        }

        /// <summary>
        /// Checks the package resources and determines if anything is missing
        /// </summary>
        /// <param name="source"></param>
        private static void CheckResources(string source)
        {
            if (Directory.Exists(source) == false)
            {
                Print(MessageType.Error, $"The source directory \"{source}\" does not exist");
                RequestExit(8);
            }

            if (File.Exists($"{source}{Path.DirectorySeparatorChar}package.json") == false)
            {
                Print(MessageType.Error, "The file package.json does not exist in the source directory");
                RequestExit(9);
            }

            if (File.Exists($"{source}{Path.DirectorySeparatorChar}main.py")) return;
            
            Print(MessageType.Error, "The main python source does not exist (main.py)");
            RequestExit(10);
        }
    }
}
