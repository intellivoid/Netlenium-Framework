using System;

namespace WebAPI_Test
{
    /// <summary>
    /// Program Class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main Program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Fetching content");
            foreach(Netlenium.WebAPI.Google.Content content in Netlenium.WebAPI.Google.Storage.FetchContents())
            {
                Console.WriteLine(content.Name);
                Console.WriteLine($"Size: {content.Size} bytes");
                Console.WriteLine($"Meta Generation: {content.MetaGeneration}");
                Console.WriteLine($"Last Modified: {content.LastModified}");
                Console.WriteLine($"Generation: {content.Generation}");
                Console.WriteLine($"ETag: {content.ETag}");
                Console.WriteLine($"Access Location: {content.AccessLocation} bytes");
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine("Done, press return to exit");
            Console.ReadLine();
        }
    }
}
