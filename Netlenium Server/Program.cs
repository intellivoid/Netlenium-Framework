using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// Main Program Pointer
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Title = "Netlenium Server";
            Console.WriteLine($"Netlenium Server v{version}");
            Console.WriteLine("Written by Zi Xing Narrakas");
            Console.WriteLine(Environment.NewLine);

            Netlenium.Logging.Enabled = true;
            Netlenium.Logging.VerboseLogging = true;

            Console.WriteLine(APIServer.Start());

            Console.ReadLine();
        }
    }
}
