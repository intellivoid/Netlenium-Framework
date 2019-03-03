using Ionic.Zip;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetleniumRuntime
{
    class Program
    {
        private static string RuntimeID;

        static void ProcessExitHandler(object sender, EventArgs e)
        {
            ClearRuntime();
            Environment.Exit(0);
        }
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

        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;
            string PackageFile = Convert.ToString(args[0]);

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
        /// Creates a runtime environment
        /// </summary>
        /// <returns></returns>
        private static string CreateEnvironment()
        {
            RuntimeID = RandomRuntimeID(12);

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

       

        private static void ClearRuntime()
        {
            if (Directory.Exists($"{Netlenium.Configuration.RuntimeDirectory}{Path.DirectorySeparatorChar}{RuntimeID}"))
            {
                try
                {
                    Directory.Delete($"{Netlenium.Configuration.RuntimeDirectory}{Path.DirectorySeparatorChar}{RuntimeID}", true);
                }
                catch (Exception)
                {
                    Console.WriteLine("Warning: The runtime cannot be closed properly");
                }
            }
        }

        private static Random RandomObject = new Random();

        public static string RandomRuntimeID(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[RandomObject.Next(s.Length)]).ToArray());
        }
    }
}
