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
        /// Main Program Pointer
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine(APIServer.Start());

            Console.ReadLine();
        }
    }
}
