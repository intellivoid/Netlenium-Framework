﻿using Ionic.Zip;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Console = Colorful.Console;

namespace NetleniumBuild
{
    /// <summary>
    /// CLI Options for Netlenium package Builder
    /// </summary>
    public class CLIOptions
    {
        [Option('s', "source", Required = true, HelpText = "The directory which contains the source code")]
        public string Source { get; set; }

    }

    class Program
    {
        /// <summary>
        /// Passes on the command line arguments to the main executing process
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var Opotions = Parser.Default.ParseArguments<CLIOptions>(args).WithParsed(
                options => {
                    MainCLI(options);
                });
        }

        /// <summary>
        /// Fetches the Assembly's executing directory
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
        /// Main Program for Building the Package
        /// </summary>
        /// <param name="Options"></param>
        static void MainCLI(CLIOptions Options)
        {
            Console.WriteLine($"Netlenium package Builder v{FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion}");
            Console.WriteLine("Written by Zi Xing Narrakas");
            Console.WriteLine();

            CheckResources(Options.Source);
            
            try
            {
                ReadMetaInformation(Options.Source);
            }
            catch(Exception exception)
            {
                Print(MessageType.Information, $"Cannot read package.json: {exception.Message}");
                Environment.Exit(1);
            }

            BuildPackage(Options.Source);

            Print(MessageType.Information, "Package built successfully");
            Environment.Exit(0);

        }

        /// <summary>
        /// Prints to the console
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Output"></param>
        private static void Print(MessageType Type, string Output)
        {
            switch(Type)
            {
                case MessageType.Out:
                    Console.Write(" > ", Color.White);
                    Console.WriteLine(Output);
                    break;

                case MessageType.Information:
                    Console.Write(" [INFO] ", Color.Cyan);
                    Console.WriteLine(Output);
                    break;

                case MessageType.Warning:
                    Console.Write(" [WARNING] ", Color.Yellow);
                    Console.WriteLine(Output);
                    break;

                case MessageType.Error:
                    Console.Write(" [ERROR] ", Color.Red);
                    Console.WriteLine(Output);
                    break;
            }
        }

        /// <summary>
        /// Builds the pacakge assuming all previous checks has passed
        /// </summary>
        /// <param name="Source"></param>
        private static void BuildPackage(string Source)
        {
            if (File.Exists($"{Source}.np") == true)
            {
                Print(MessageType.Out, $"Deleting old {Source}.np file");
                try
                {
                    File.Delete($"{Source}.np");
                }
                catch (Exception exception)
                {
                    Print(MessageType.Error, $"Cannot delete the old Netlenium package: {exception.Message}");
                    Environment.Exit(1);
                }
            }

            try
            {
                Print(MessageType.Out, $"Building package {Source}.np");
                using (ZipFile Zip = new ZipFile())
                {
                    Print(MessageType.Out, "Adding Source Directory");
                    Zip.AddDirectory($"{Source}", "source");
                    Zip.AddEntry("c_netlenium.xml", ConstructDependency("Netlenium"));
                    Zip.AddEntry("c_netlenium.driver.xml", ConstructDependency("Netlenium.Driver"));
                    Zip.AddEntry("c_netlenium.driver.chrome.xml", ConstructDependency("Netlenium.Driver.Chrome"));
                    Zip.AddEntry("c_netlenium.driver.geckofxlib.xml", ConstructDependency("Netlenium.Driver.GeckoFXLib"));
                    Print(MessageType.Out, "Writing package to disk");
                    Zip.Save($"{Source}.np");
                }
            }
            catch (Exception exception)
            {
                Print(MessageType.Error, $"There was an error while trying to build the pacakge: {exception.Message}");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Reads the packages meta information
        /// </summary>
        /// <param name="Source"></param>
        private static void ReadMetaInformation(string Source)
        {
            Print(MessageType.Out, "Reading package.json");
            JObject JsonPackageMetaInformation = JObject.Parse(File.ReadAllText($"{Source}{Path.DirectorySeparatorChar}package.json"));

            if ((string)JsonPackageMetaInformation["name"] == null)
            {
                Print(MessageType.Error, $"package.json is missing \"name\"");
                Environment.Exit(1);
            }

            if ((string)JsonPackageMetaInformation["version"] == null)
            {
                Print(MessageType.Error, $"package.json is missing \"version\"");
                Environment.Exit(1);
            }

            Print(MessageType.Information, $"Name: {(string)JsonPackageMetaInformation["name"]}");
            Print(MessageType.Information, $"Version: {(string)JsonPackageMetaInformation["version"]}");

            if ((string)JsonPackageMetaInformation["author"] != null)
            {
                Print(MessageType.Information, $"Author: {(string)JsonPackageMetaInformation["author"]}");
            }
            else
            {
                Print(MessageType.Information, $"Author: None");
            }

            if ((string)JsonPackageMetaInformation["company"] != null)
            {
                Print(MessageType.Information, $"Company: {(string)JsonPackageMetaInformation["company"]}");
            }
            else
            {
                Print(MessageType.Information, $"Company: None");
            }
        }

        /// <summary>
        /// Constructs the dependency information (XML Format)
        /// </summary>
        /// <param name="Dependency"></param>
        /// <returns></returns>
        private static string ConstructDependency(string Dependency)
        {
            string DependencyFile = $"{AssemblyDirectory}{Path.DirectorySeparatorChar}{Dependency}.dll";

            if(File.Exists(DependencyFile) == false)
            {
                Print(MessageType.Error, $"The required dependency for the framework cannot be found \"{DependencyFile}\"");
            }

            Print(MessageType.Out, $"Constructing Dependency Information for \"{Dependency}\"");

            NameValueCollection Data = new NameValueCollection();

            FileVersionInfo VersionInformation = FileVersionInfo.GetVersionInfo($"{AssemblyDirectory}{Path.DirectorySeparatorChar}{Dependency}.dll");

            Data.Add("dependency", Dependency);
            Data.Add("major", Convert.ToString(VersionInformation.FileMajorPart));
            Data.Add("minor", Convert.ToString(VersionInformation.FileMinorPart));
            Data.Add("build", Convert.ToString(VersionInformation.FileBuildPart));
            Data.Add("revision", Convert.ToString(VersionInformation.FilePrivatePart));
            Data.Add("file_name", Convert.ToString(VersionInformation.OriginalFilename));
            Data.Add("internal", Convert.ToString(VersionInformation.InternalName));
            Data.Add("publisher", Convert.ToString(VersionInformation.CompanyName));

            var Results = new XElement("Netlenium_Framework", Data.AllKeys.Select(o => new XElement(o, Data[o])));
            return Results.ToString();

        }

        /// <summary>
        /// Checks the package resources and determines if anything is missing
        /// </summary>
        /// <param name="Source"></param>
        private static void CheckResources(string Source)
        {
            if (Directory.Exists(Source) == false)
            {
                Console.Write("Missing Resource Error: ", Color.Red);
                Console.Write("The directory");
                Console.Write($" \"{Source}\" ", Color.Gray);
                Console.Write("was not found");
                Console.WriteLine();
                Environment.Exit(1);
            }

            if (File.Exists($"{Source}{Path.DirectorySeparatorChar}package.json") == false)
            {
                Console.Write("Missing Resource Error: ", Color.Red);
                Console.Write("The directory");
                Console.Write($" \"{Source}{Path.DirectorySeparatorChar}package.json\" ", Color.Gray);
                Console.Write("was not found");
                Console.WriteLine();
                Environment.Exit(1);
            }

            if (File.Exists($"{Source}{Path.DirectorySeparatorChar}main.py") == false)
            {
                Console.Write("Missing Resource Error: ", Color.Red);
                Console.Write("The directory");
                Console.Write($" \"{Source}{Path.DirectorySeparatorChar}main.py\" ", Color.Gray);
                Console.Write("was not found");
                Console.WriteLine();
                Environment.Exit(1);
            }
        }
    }
}
