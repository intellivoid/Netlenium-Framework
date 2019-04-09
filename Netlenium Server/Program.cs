using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Netlenium_Server
{
    /// <summary>
    /// Main Execution Program
    /// </summary>
    class Program
    {
        /// <summary>
        /// The version of the server
        /// </summary>
        private static string version = "1.0.0.0";

        /// <summary>
        /// Public property of the version
        /// </summary>
        public static string Version { get => version; set => version = value; }

        public static Dictionary<string, object> HttpController = new Dictionary<string, object>();
        public static Thread HTTPServer { get; set; }

        /// <summary>
        /// Starts the HttpManager by providing values
        /// </summary>
        private static void _ConstructHttpManager()
        {
            HttpController.Add("Started", false);
            HttpController.Add("ShutdownSignal", false);
            HttpController.Add("Status", false);
        }

        /// <summary>
        /// Main Program Pointer
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(string[] args)
        {
            _ConstructHttpManager();
            AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;

            Console.Title = "Netlenium Server";
            Console.WriteLine($"Netlenium Server v{Version}");
            Console.WriteLine("Written by Zi Xing Narrakas");
            Console.WriteLine();

            Netlenium.Logging.Enabled = true;
            Netlenium.Logging.VerboseLogging = false;

            HTTPServer = APIServer.StartThread("testdomain", 80);
            while ((bool)HttpController["Status"] == false)
            {
                Thread.Sleep(200);
            }

            Console.ReadLine();
            Environment.Exit(0);
            
        }

        /// <summary>
        /// Handler for when the process is about to be terminated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ProcessExitHandler(object sender, EventArgs e)
        {
            APIServer.StopThread();
        }

    }
}
