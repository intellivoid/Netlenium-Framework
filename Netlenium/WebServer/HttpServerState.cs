using System;
using System.Collections.Generic;
using System.Text;

namespace Netlenium.WebServer
{
    public enum HttpServerState
    {
        Stopped,
        Starting,
        Started,
        Stopping
    }
}
