using System;

namespace Netlenium.WebAPI.Google.ChromeDriver
{
    public class Content
    {
        public string Name { get; set; }
        
        public int Generation { get; set; }
        
        public int MetaGeneration { get; set; }
        
        public string LastModified { get; set; }
        
        public string ETag { get; set; }
        
        public int Size { get; set; }
        
        public Uri AccessLocation { get; set; }
    }
}