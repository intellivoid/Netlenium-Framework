using CommandLine;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace NetleniumRuntime
{
    public class CLIOptions
    {
        [Option('f', "file", Required = true, HelpText = "The Netlenium Package file location (.np file)")]
        public string Source { get; set; }

    }

    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;
            var Opotions = Parser.Default.ParseArguments<CLIOptions>(args).WithParsed(
                options => {
                    Main(options);
                });
        }

        private static string RuntimeID;

        static void Main(CLIOptions Options)
        {
            // Check if the File Exists
            if (File.Exists(Options.Source) == false)
            {
                Console.WriteLine($"Error: The file \"{Options.Source}\" does not exist!", System.Drawing.Color.Red);
                Environment.Exit(1);
            }

            // Check if the Runtime Directory Exists
            string RuntimeDirectory = $"{Netlenium.Configuration.ApplicationDataDirectory}{Path.DirectorySeparatorChar}Runtime";

            if (Directory.Exists(RuntimeDirectory) == false)
            {
                try
                {
                    Directory.CreateDirectory(RuntimeDirectory);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Error: The runtime directory cannot be created, {exception.Message}", System.Drawing.Color.Red);
                    Environment.Exit(1);
                }
            }

            // Assign a Runtime ID and create the enviroment
            RuntimeID = RandomRuntimeID(12);
            string RuntimeEnvironment = $"{Netlenium.Configuration.ApplicationDataDirectory}{Path.DirectorySeparatorChar}Runtime{Path.DirectorySeparatorChar}{RuntimeID}";

            if (Directory.Exists(RuntimeEnvironment))
            {
                try
                {
                    Directory.Delete(RuntimeEnvironment);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: Duplicate Runtime", System.Drawing.Color.Red);
                    Environment.Exit(1);
                }
            }

            Directory.CreateDirectory(RuntimeEnvironment);

            // Extract the package contents
            try
            {
                ZipFile zip = ZipFile.Read(Options.Source);
                Directory.CreateDirectory(RuntimeEnvironment);
                zip.ExtractAll(RuntimeEnvironment, ExtractExistingFileAction.OverwriteSilently);
            }
            catch(Exception)
            {
                Console.WriteLine("Error: There was an issue while trying to read the Netlenium Package", System.Drawing.Color.Red);
                ClearRuntime();
                Environment.Exit(1);
            }

            // Include the CLR Modules in the Python Script
            string MainScript = $"{RuntimeEnvironment}{Path.DirectorySeparatorChar}source{Path.DirectorySeparatorChar}main.py";
            string ImportedScript = $"{RuntimeEnvironment}{Path.DirectorySeparatorChar}source{Path.DirectorySeparatorChar}c_main.py";
            string CompiledScript = $"{Properties.Resources.RuntimeImportModules}{Environment.NewLine}{File.ReadAllText(MainScript)}";

            File.WriteAllText(ImportedScript, CompiledScript);

            Process.Start("explorer.exe", RuntimeEnvironment);

            ClearRuntime();
            Environment.Exit(0);

        }

        private static Random RandomObject = new Random();

        public static string RandomRuntimeID(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[RandomObject.Next(s.Length)]).ToArray());
        }

        private static void ClearRuntime()
        {
            if (Directory.Exists($"{Netlenium.Configuration.ApplicationDataDirectory}{Path.DirectorySeparatorChar}Runtime{Path.DirectorySeparatorChar}{RuntimeID}"))
            {
                try
                {
                    Directory.Delete($"{Netlenium.Configuration.ApplicationDataDirectory}{Path.DirectorySeparatorChar}Runtime{Path.DirectorySeparatorChar}{RuntimeID}");
                }
                catch (Exception)
                {
                    Console.WriteLine("Warning: The runtime cannot be closed properly");
                }
            }
        }

        static void ProcessExitHandler(object sender, EventArgs e)
        {
            ClearRuntime();
            Environment.Exit(0);
        }
    }
}
