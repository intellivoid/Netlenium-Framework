using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Xml;

namespace Netlenium.WebAPI.Google.ChromeDriver
{
    public class Storage
    {
        /// <summary>
        /// Fetches the content from the Storage API
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public static List<Content> FetchContents(string endPoint = "https://chromedriver.storage.googleapis.com/")
        {
            var httpClient = new WebClient();
            var response = httpClient.DownloadString(endPoint);
            
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(response);

            return(from XmlNode contentNode in xmlDocument.SelectNodes("ListBucketResult/Contents")
                select new Content
                {
                    Name = contentNode.SelectSingleNode("//Key").Value,
                    Generation = Convert.ToInt32(contentNode.SelectSingleNode("//Generation").Value),
                    MetaGeneration = Convert.ToInt32(contentNode.SelectSingleNode("//MetaGeneration").Value),
                    LastModified = contentNode.SelectSingleNode("//LastModified").Value,
                    ETag = contentNode.SelectSingleNode("//ETag").Value,
                    Size = Convert.ToInt32(contentNode.SelectSingleNode("//Size").Value),
                    AccessLocation = new Uri($"{endPoint}{contentNode.SelectSingleNode("//Key").Value}");
                }).ToList();
        }
    }
}