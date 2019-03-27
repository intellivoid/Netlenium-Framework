using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netlenium_Server.ResponseObjects
{
    public class Generic
    {
        public bool Status { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }

        public object Payload { get; set; }
    }
}
