using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Netlenium.WebDriver.Html5;

namespace Netlenium.WebDriver.Remote
{
    /// <summary>
    /// Defines the interface through which the user can manipulate local storage.
    /// </summary>
    public class RemoteLocalStorage : ILocalStorage
    {
        private RemoteWebDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteLocalStorage"/> class.
        /// </summary>
        /// <param name="driver">The <see cref="RemoteWebDriver"/> for which the application cache will be managed.</param>
        public RemoteLocalStorage(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// Gets the number of items in local storage.
        /// </summary>
        public int Count
        {
            get
            {
                Response commandResponse = this.driver.InternalExecute(DriverCommand.GetLocalStorageSize, null);
                return Convert.ToInt32(commandResponse.Value, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Returns local storage value given a key.
        /// </summary>
        /// <param name="key">The key value for the item in storage.</param>
        /// <returns>A local storage <see cref="string"/> value given a key, if present, otherwise returns null.</returns>
        public string GetItem(string key)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("key", key);
            Response commandResponse = this.driver.InternalExecute(DriverCommand.GetLocalStorageItem, parameters);
            if (commandResponse.Value == null)
            {
                return null;
            }

            return commandResponse.Value.ToString();
        }

        /// <summary>
        /// Returns a read-only list of local storage keys.
        /// </summary>
        /// <returns>A <see cref="ReadOnlyCollection{T}">read-only list</see> of local storage keys.</returns>
        public ReadOnlyCollection<string> KeySet()
        {
            List<string> result = new List<string>();
            Response commandResponse = this.driver.InternalExecute(DriverCommand.GetLocalStorageKeys, null);
            object[] keys = commandResponse.Value as object[];
            foreach (string key in keys)
            {
                result.Add(key);
            }

            return result.AsReadOnly();
        }

        /// <summary>
        /// Sets local storage entry using given key/value pair.
        /// </summary>
        /// <param name="key">local storage key</param>
        /// <param name="value">local storage value</param>
        public void SetItem(string key, string value)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("key", key);
            parameters.Add("value", value);
            this.driver.InternalExecute(DriverCommand.SetLocalStorageItem, parameters);
        }

        /// <summary>
        /// Removes local storage entry for the given key.
        /// </summary>
        /// <param name="key">key to be removed from the list</param>
        /// <returns>Response value <see cref="string"/>for the given key.</returns>
        public string RemoveItem(string key)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("key", key);
            Response commandResponse = this.driver.InternalExecute(DriverCommand.RemoveLocalStorageItem, parameters);
            if (commandResponse.Value == null)
            {
                return null;
            }

            return commandResponse.Value.ToString();
        }

        /// <summary>
        /// Removes all entries from the local storage.
        /// </summary>
        public void Clear()
        {
            this.driver.InternalExecute(DriverCommand.ClearLocalStorage, null);
        }
    }
}
