using Netlenium.Driver;
using System;
using System.IO;
using System.Windows.Forms;

namespace Test
{
    class Program
    {
        

        [STAThread]
        static void Main(string[] args)
        {
            Netlenium.Logging.AllowLogging = true;
            Netlenium.Logging.OutputFile = "netlenium.log";

            Controller Browser = new Controller(Netlenium.Types.Driver.GeckoLib);
            Browser.Initialize();

            Browser.Navigate("https://nlp.stanford.edu/software/");
            Browser.GetElements(Netlenium.Types.SearchType.ClassName, "question");

            Console.WriteLine("Done");

            Application.Run();
        }
    }
}
