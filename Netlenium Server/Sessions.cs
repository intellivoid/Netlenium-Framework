using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netlenium_Server
{
    
    public class Sessions
    {
        /// <summary>
        /// The current list of active sessions
        /// </summary>
        private static Dictionary<string, Session> activeSessions;

        public static Session CreateSession(SessionConfiguration configuration)
        {
            var SessionObject = new Session();

            var DriverConfiguration = new Netlenium.DriverConfiguration()
            {
                Headless = configuration.Headless,
                DriverLogging = false,
                DriverVerboseLogging = false,
                FrameworkLogging = true,
                FrameworkVerboseLogging = true, 
                TargetPlatform = Netlenium.Types.Platform.AutoDetect
            };

            switch(configuration.TargetDriver.ToLower())
            {
                case "chrome":
                    DriverConfiguration.TargetDriver = Netlenium.Types.Driver.Chrome;
                    break;

                case "gecko_lib":
                    DriverConfiguration.TargetDriver = Netlenium.Types.Driver.GeckoLib;
                    break;

                default:
                    throw new UnsupportedDriverException();
            }

            SessionObject.ObjectController = new Netlenium.Driver.Controller(DriverConfiguration);
            SessionObject.Id = "session_id";

            activeSessions.Add(SessionObject.Id, SessionObject);

            return SessionObject;
        }

    }
}
