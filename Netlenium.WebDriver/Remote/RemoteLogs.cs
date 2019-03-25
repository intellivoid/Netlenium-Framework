﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Netlenium.WebDriver.Remote
{
    /// <summary>
    /// Provides a mechanism for examining logs for the driver during the test.
    /// </summary>
    public class RemoteLogs : ILogs
    {
        private RemoteWebDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteLogs"/> class.
        /// </summary>
        /// <param name="driver">Instance of the driver currently in use</param>
        public RemoteLogs(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// Gets the list of available log types for this driver.
        /// </summary>
        public ReadOnlyCollection<string> AvailableLogTypes
        {
            get
            {
                List<string> availableLogTypes = new List<string>();
                Response commandResponse = this.driver.InternalExecute(DriverCommand.GetAvailableLogTypes, null);
                object[] responseValue = commandResponse.Value as object[];
                if (responseValue != null)
                {
                    foreach (object logKind in responseValue)
                    {
                        availableLogTypes.Add(logKind.ToString());
                    }
                }

                return availableLogTypes.AsReadOnly();
            }
        }

        /// <summary>
        /// Gets the set of <see cref="LogEntry"/> objects for a specified log.
        /// </summary>
        /// <param name="logKind">The log for which to retrieve the log entries.
        /// Log types can be found in the <see cref="LogType"/> class.</param>
        /// <returns>The list of <see cref="LogEntry"/> objects for the specified log.</returns>
        public ReadOnlyCollection<LogEntry> GetLog(string logKind)
        {
            List<LogEntry> entries = new List<LogEntry>();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("type", logKind);
            Response commandResponse = this.driver.InternalExecute(DriverCommand.GetLog, parameters);

            object[] responseValue = commandResponse.Value as object[];
            if (responseValue != null)
            {
                foreach (object rawEntry in responseValue)
                {
                    Dictionary<string, object> entryDictionary = rawEntry as Dictionary<string, object>;
                    if (entryDictionary != null)
                    {
                        entries.Add(LogEntry.FromDictionary(entryDictionary));
                    }
                }
            }

            return entries.AsReadOnly();
        }
    }
}
