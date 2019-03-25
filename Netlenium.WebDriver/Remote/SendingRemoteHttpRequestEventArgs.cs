using System;
using System.Net;

namespace Netlenium.WebDriver.Remote
{
    public class SendingRemoteHttpRequestEventArgs : EventArgs
    {
        private HttpWebRequest request;
        private string requestBody;

        public SendingRemoteHttpRequestEventArgs(HttpWebRequest request, string requestBody)
        {
            this.request = request;
            this.requestBody = requestBody;
        }

        public HttpWebRequest Request
        {
            get { return this.request; }
        }

        public string RequestBody
        {
            get { return this.requestBody; }
        }
    }
}
