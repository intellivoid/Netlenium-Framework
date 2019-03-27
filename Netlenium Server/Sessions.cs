using Netlenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Netlenium_Server
{
    
    public class Sessions
    {
        /// <summary>
        /// The current list of active sessions
        /// </summary>
        private static Dictionary<string, Session> activeSessions;

        /// <summary>
        /// Generates a new Session ID
        /// </summary>
        /// <returns></returns>
        private static string GeneratedSessionId()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[32];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }

        /// <summary>
        /// Creates a new session and starts the WebDriver
        /// </summary>
        /// <param name="targetDriver"></param>
        /// <returns></returns>
        public static Session CreateSession(string targetDriver)
        {
            var SessionObject = new Session();

            var DriverConfiguration = new Netlenium.DriverConfiguration()
            {
                Headless = true,
                DriverLogging = false,
                DriverVerboseLogging = false,
                FrameworkLogging = true,
                FrameworkVerboseLogging = true, 
                TargetPlatform = Netlenium.Types.Platform.AutoDetect
            };

            switch(targetDriver.ToLower())
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
            SessionObject.Id = GeneratedSessionId();

            SessionObject.ObjectController.Initialize();

            if(activeSessions == null)
            {
                activeSessions = new Dictionary<string, Session>();
            }

            activeSessions.Add(SessionObject.Id, SessionObject);

            return SessionObject;
        }

        public static void CloseAllSessions()
        {
            if(activeSessions != null)
            {
                foreach(var Session in activeSessions.Values.ToList())
                {
                    try
                    {
                        Logging.WriteEntry(Netlenium.Types.LogType.Information, "Netlenium Server", $"Killing session {Session.Id}");
                        Session.ObjectController.Quit();
                    }
                    catch(Exception exception)
                    {
                        Logging.WriteEntry(Netlenium.Types.LogType.Warning, "Netlenium Server",  $"Failed to close session {Session.Id}, {exception.Message}");
                    }

                    activeSessions.Remove(Session.Id);
                }
            }
        }

    }
}
