using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netlenium.WebServer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Netlenium_Server
{
    public class APIHandler
    {
        public static void CreateSession(HttpRequestEventArgs httpRequest)
        {
            if(httpRequest.Request.QueryString.Get("target_driver") == null)
            {
                var Response = new ResponseObjects.Generic()
                {
                    Code = 401,
                    Status = false,
                    Message = "Missing paramerter 'target_driver'",
                    Payload = null
                };

                httpRequest.Response.StatusCode = 401;
                httpRequest.Response.Headers.Add("content-Type", "application/json");

                APIServer.SendResponse(httpRequest.Response, JsonConvert.SerializeObject(Response));
            }
        }
    }
}
