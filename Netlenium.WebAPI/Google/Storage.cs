using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Xml;

namespace Netlenium.WebAPI.Google
{
    /// <summary>
    /// Google Storage API
    /// </summary>
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
            var xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(httpClient.DownloadString(endPoint));
            var contentResults = new List<Content>();

            foreach (XmlNode contentNode in xmlDocument.GetElementsByTagName("Contents"))
            {
                contentResults.Add(new Content()
                {
                    Name = contentNode["Key"].InnerText,
                    Generation = contentNode["Generation"].InnerText,
                    MetaGeneration = contentNode["MetaGeneration"].InnerText,
                    LastModified = contentNode["LastModified"].InnerText,
                    ETag = contentNode["ETag"].InnerText,
                    Size = contentNode["Size"].InnerText,
                    AccessLocation = new Uri($"{endPoint}{contentNode["Key"].InnerText}")
                });
            }

            return contentResults;
        }

        /// <summary>
        /// Fetches a specific resource
        /// </summary>
        /// <param name="endPoint"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static Content FetchResource(string name, string endPoint = "https://chromedriver.storage.googleapis.com/")
        {
            foreach(Content content in FetchContents(endPoint))
            {
                if(content.Name.ToLower() == name.ToLower())
                {
                    return content;
                }
            }

            throw new ResourceNotFoundException();
        }
    }
}
