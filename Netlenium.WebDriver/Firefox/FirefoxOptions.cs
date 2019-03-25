// <copyright file="FirefoxOptions.cs" company="WebDriver Committers">
// Licensed to the Software Freedom Conservancy (SFC) under one
// or more contributor license agreements. See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership. The SFC licenses this file
// to you under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;
using System.Collections.Generic;
using System.Globalization;
using Netlenium.WebDriver.Remote;

namespace Netlenium.WebDriver.Firefox
{
    /// <summary>
    /// Class to manage options specific to <see cref="FirefoxDriver"/>
    /// </summary>
    /// <remarks>
    /// Used with the marionette executable wires.exe.
    /// </remarks>
    /// <example>
    /// <code>
    /// FirefoxOptions options = new FirefoxOptions();
    /// </code>
    /// <para></para>
    /// <para>For use with FirefoxDriver:</para>
    /// <para></para>
    /// <code>
    /// FirefoxDriver driver = new FirefoxDriver(options);
    /// </code>
    /// <para></para>
    /// <para>For use with RemoteWebDriver:</para>
    /// <para></para>
    /// <code>
    /// RemoteWebDriver driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options.ToCapabilities());
    /// </code>
    /// </example>
    public class FirefoxOptions : DriverOptions
    {
        private const string BrowserNameValue = "firefox";

        private const string IsMarionetteCapability = "marionette";
        private const string FirefoxLegacyProfileCapability = "firefox_profile";
        private const string FirefoxLegacyBinaryCapability = "firefox_binary";
        private const string FirefoxProfileCapability = "profile";
        private const string FirefoxBinaryCapability = "binary";
        private const string FirefoxArgumentsCapability = "args";
        private const string FirefoxLogCapability = "log";
        private const string FirefoxPrefsCapability = "prefs";
        private const string FirefoxOptionsCapability = "moz:firefoxOptions";

        private bool isMarionette = true;
        private string browserBinaryLocation;
        private FirefoxDriverLogLevel logLevel = FirefoxDriverLogLevel.Default;
        private FirefoxProfile profile;
        private List<string> firefoxArguments = new List<string>();
        private Dictionary<string, object> profilePreferences = new Dictionary<string, object>();
        private Dictionary<string, object> additionalCapabilities = new Dictionary<string, object>();
        private Dictionary<string, object> additionalFirefoxOptions = new Dictionary<string, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FirefoxOptions"/> class.
        /// </summary>
        public FirefoxOptions()
            : base()
        {
            this.BrowserName = BrowserNameValue;
            this.AddKnownCapabilityName(FirefoxOptions.FirefoxOptionsCapability, "current FirefoxOptions class instance");
            this.AddKnownCapabilityName(FirefoxOptions.IsMarionetteCapability, "UseLegacyImplementation property");
            this.AddKnownCapabilityName(FirefoxOptions.FirefoxProfileCapability, "Profile property");
            this.AddKnownCapabilityName(FirefoxOptions.FirefoxBinaryCapability, "BrowserExecutableLocation property");
            this.AddKnownCapabilityName(FirefoxOptions.FirefoxArgumentsCapability, "AddArguments method");
            this.AddKnownCapabilityName(FirefoxOptions.FirefoxPrefsCapability, "SetPreference method");
            this.AddKnownCapabilityName(FirefoxOptions.FirefoxLogCapability, "LogLevel property");
            this.AddKnownCapabilityName(FirefoxOptions.FirefoxLegacyProfileCapability, "Profile property");
            this.AddKnownCapabilityName(FirefoxOptions.FirefoxLegacyBinaryCapability, "BrowserExecutableLocation property");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FirefoxOptions"/> class for the given profile and binary.
        /// </summary>
        /// <param name="profile">The <see cref="FirefoxProfile"/> to use in the options.</param>
        /// <param name="binary">The <see cref="FirefoxBinary"/> to use in the options.</param>
        /// <param name="capabilities">The <see cref="DesiredCapabilities"/> to copy into the options.</param>
        internal FirefoxOptions(FirefoxProfile profile, FirefoxBinary binary, DesiredCapabilities capabilities)
        {
            this.BrowserName = BrowserNameValue;
            if (profile != null)
            {
                this.profile = profile;
            }

            if (binary != null)
            {
                this.browserBinaryLocation = binary.BinaryExecutable.ExecutablePath;
            }

            if (capabilities != null)
            {
                this.ImportCapabilities(capabilities);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use the legacy driver implementation.
        /// </summary>
        public bool UseLegacyImplementation
        {
            get { return !this.isMarionette; }
            set { this.isMarionette = !value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="FirefoxProfile"/> object to be used with this instance.
        /// </summary>
        public FirefoxProfile Profile
        {
            get { return this.profile; }
            set { this.profile = value; }
        }

        /// <summary>
        /// Gets or sets the path and file name of the Firefox browser executable.
        /// </summary>
        public string BrowserExecutableLocation
        {
            get { return this.browserBinaryLocation; }
            set { this.browserBinaryLocation = value; }
        }

        /// <summary>
        /// Gets or sets the logging level of the Firefox driver.
        /// </summary>
        public FirefoxDriverLogLevel LogLevel
        {
            get { return this.logLevel; }
            set { this.logLevel = value; }
        }

        /// <summary>
        /// Adds an argument to be used in launching the Firefox browser.
        /// </summary>
        /// <param name="argumentName">The argument to add.</param>
        /// <remarks>Arguments must be preceeded by two dashes ("--").</remarks>
        public void AddArgument(string argumentName)
        {
            if (string.IsNullOrEmpty(argumentName))
            {
                throw new ArgumentException("argumentName must not be null or empty", "argumentName");
            }

            this.AddArguments(argumentName);
        }

        /// <summary>
        /// Adds a list arguments to be used in launching the Firefox browser.
        /// </summary>
        /// <param name="argumentsToAdd">An array of arguments to add.</param>
        /// <remarks>Each argument must be preceeded by two dashes ("--").</remarks>
        public void AddArguments(params string[] argumentsToAdd)
        {
            this.AddArguments(new List<string>(argumentsToAdd));
        }

        /// <summary>
        /// Adds a list arguments to be used in launching the Firefox browser.
        /// </summary>
        /// <param name="argumentsToAdd">An array of arguments to add.</param>
        /// <remarks>Each argument must be preceeded by two dashes ("--").</remarks>
        public void AddArguments(IEnumerable<string> argumentsToAdd)
        {
            if (argumentsToAdd == null)
            {
                throw new ArgumentNullException("argumentsToAdd", "argumentsToAdd must not be null");
            }

            this.firefoxArguments.AddRange(argumentsToAdd);
        }

        /// <summary>
        /// Sets a preference in the profile used by Firefox.
        /// </summary>
        /// <param name="preferenceName">Name of the preference to set.</param>
        /// <param name="preferenceValue">Value of the preference to set.</param>
        public void SetPreference(string preferenceName, bool preferenceValue)
        {
            this.SetPreferenceValue(preferenceName, preferenceValue);
        }

        /// <summary>
        /// Sets a preference in the profile used by Firefox.
        /// </summary>
        /// <param name="preferenceName">Name of the preference to set.</param>
        /// <param name="preferenceValue">Value of the preference to set.</param>
        public void SetPreference(string preferenceName, int preferenceValue)
        {
            this.SetPreferenceValue(preferenceName, preferenceValue);
        }

        /// <summary>
        /// Sets a preference in the profile used by Firefox.
        /// </summary>
        /// <param name="preferenceName">Name of the preference to set.</param>
        /// <param name="preferenceValue">Value of the preference to set.</param>
        public void SetPreference(string preferenceName, long preferenceValue)
        {
            this.SetPreferenceValue(preferenceName, preferenceValue);
        }

        /// <summary>
        /// Sets a preference in the profile used by Firefox.
        /// </summary>
        /// <param name="preferenceName">Name of the preference to set.</param>
        /// <param name="preferenceValue">Value of the preference to set.</param>
        public void SetPreference(string preferenceName, double preferenceValue)
        {
            this.SetPreferenceValue(preferenceName, preferenceValue);
        }

        /// <summary>
        /// Sets a preference in the profile used by Firefox.
        /// </summary>
        /// <param name="preferenceName">Name of the preference to set.</param>
        /// <param name="preferenceValue">Value of the preference to set.</param>
        public void SetPreference(string preferenceName, string preferenceValue)
        {
            this.SetPreferenceValue(preferenceName, preferenceValue);
        }

        /// <summary>
        /// Provides a means to add additional capabilities not yet added as type safe options
        /// for the Firefox driver.
        /// </summary>
        /// <param name="capabilityName">The name of the capability to add.</param>
        /// <param name="capabilityValue">The value of the capability to add.</param>
        /// <exception cref="ArgumentException">
        /// thrown when attempting to add a capability for which there is already a type safe option, or
        /// when <paramref name="capabilityName"/> is <see langword="null"/> or the empty string.
        /// </exception>
        /// <remarks>Calling <see cref="AddAdditionalCapability(string, object)"/>
        /// where <paramref name="capabilityName"/> has already been added will overwrite the
        /// existing value with the new value in <paramref name="capabilityValue"/>.
        /// Also, by default, calling this method adds capabilities to the options object passed to
        /// geckodriver.exe.</remarks>
        public override void AddAdditionalCapability(string capabilityName, object capabilityValue)
        {
            // Add the capability to the FirefoxOptions object by default. This is to handle
            // the 80% case where the geckodriver team adds a new option in geckodriver.exe
            // and the bindings have not yet had a type safe option added.
            this.AddAdditionalCapability(capabilityName, capabilityValue, false);
        }

        /// <summary>
        /// Provides a means to add additional capabilities not yet added as type safe options
        /// for the Firefox driver.
        /// </summary>
        /// <param name="capabilityName">The name of the capability to add.</param>
        /// <param name="capabilityValue">The value of the capability to add.</param>
        /// <param name="isGlobalCapability">Indicates whether the capability is to be set as a global
        /// capability for the driver instead of a Firefox-specific option.</param>
        /// <exception cref="ArgumentException">
        /// thrown when attempting to add a capability for which there is already a type safe option, or
        /// when <paramref name="capabilityName"/> is <see langword="null"/> or the empty string.
        /// </exception>
        /// <remarks>Calling <see cref="AddAdditionalCapability(string, object, bool)"/>
        /// where <paramref name="capabilityName"/> has already been added will overwrite the
        /// existing value with the new value in <paramref name="capabilityValue"/></remarks>
        public void AddAdditionalCapability(string capabilityName, object capabilityValue, bool isGlobalCapability)
        {
            if (this.IsKnownCapabilityName(capabilityName))
            {
                string typeSafeOptionName = this.GetTypeSafeOptionName(capabilityName);
                string message = string.Format(CultureInfo.InvariantCulture, "There is already an option for the {0} capability. Please use the {1} instead.", capabilityName, typeSafeOptionName);
                throw new ArgumentException(message, "capabilityName");
            }

            if (string.IsNullOrEmpty(capabilityName))
            {
                throw new ArgumentException("Capability name may not be null an empty string.", "capabilityName");
            }

            if (isGlobalCapability)
            {
                this.additionalCapabilities[capabilityName] = capabilityValue;
            }
            else
            {
                this.additionalFirefoxOptions[capabilityName] = capabilityValue;
            }
        }

        /// <summary>
        /// Returns DesiredCapabilities for Firefox with these options included as
        /// capabilities. This does not copy the options. Further changes will be
        /// reflected in the returned capabilities.
        /// </summary>
        /// <returns>The DesiredCapabilities for Firefox with these options.</returns>
        public override ICapabilities ToCapabilities()
        {
            DesiredCapabilities capabilities = GenerateDesiredCapabilities(this.isMarionette);
            if (this.isMarionette)
            {
                Dictionary<string, object> firefoxOptions = this.GenerateFirefoxOptionsDictionary();
                capabilities.SetCapability(FirefoxOptionsCapability, firefoxOptions);
            }
            else
            {
                if (this.profile != null)
                {
                    if (this.Proxy != null)
                    {
                        this.profile.InternalSetProxyPreferences(this.Proxy);
                    }

                    capabilities.SetCapability(FirefoxProfileCapability, this.profile.ToBase64String());
                }

                if (!string.IsNullOrEmpty(this.browserBinaryLocation))
                {
                    capabilities.SetCapability(FirefoxBinaryCapability, this.browserBinaryLocation);
                }
                else
                {
                    using (FirefoxBinary executablePathBinary = new FirefoxBinary())
                    {
                        string executablePath = executablePathBinary.BinaryExecutable.ExecutablePath;
                        capabilities.SetCapability(FirefoxBinaryCapability, executablePath);
                    }
                }
            }

            foreach (KeyValuePair<string, object> pair in this.additionalCapabilities)
            {
                capabilities.SetCapability(pair.Key, pair.Value);
            }

            // Should return capabilities.AsReadOnly(), and will in a future release.
            return capabilities;
        }

        private Dictionary<string, object> GenerateFirefoxOptionsDictionary()
        {
            Dictionary<string, object> firefoxOptions = new Dictionary<string, object>();

            if (this.profile != null)
            {
                // Using Marionette/Geckodriver, so the legacy WebDriver extension
                // is not required.
                this.profile.RemoveWebDriverExtension();
                firefoxOptions[FirefoxProfileCapability] = this.profile.ToBase64String();
            }

            if (!string.IsNullOrEmpty(this.browserBinaryLocation))
            {
                firefoxOptions[FirefoxBinaryCapability] = this.browserBinaryLocation;
            }
            else
            {
                if (!this.isMarionette)
                {
                    using (FirefoxBinary executablePathBinary = new FirefoxBinary())
                    {
                        string executablePath = executablePathBinary.BinaryExecutable.ExecutablePath;
                        if (!string.IsNullOrEmpty(executablePath))
                        {
                            firefoxOptions[FirefoxBinaryCapability] = executablePath;
                        }
                    }
                }
            }

            if (this.logLevel != FirefoxDriverLogLevel.Default)
            {
                Dictionary<string, object> logObject = new Dictionary<string, object>();
                logObject["level"] = this.logLevel.ToString().ToLowerInvariant();
                firefoxOptions[FirefoxLogCapability] = logObject;
            }

            if (this.firefoxArguments.Count > 0)
            {
                List<object> args = new List<object>();
                foreach (string argument in this.firefoxArguments)
                {
                    args.Add(argument);
                }

                firefoxOptions[FirefoxArgumentsCapability] = args;
            }

            if (this.profilePreferences.Count > 0)
            {
                firefoxOptions[FirefoxPrefsCapability] = this.profilePreferences;
            }

            foreach (KeyValuePair<string, object> pair in this.additionalFirefoxOptions)
            {
                firefoxOptions.Add(pair.Key, pair.Value);
            }

            return firefoxOptions;
        }

        private void SetPreferenceValue(string preferenceName, object preferenceValue)
        {
            if (string.IsNullOrEmpty(preferenceName))
            {
                throw new ArgumentException("Preference name may not be null an empty string.", "preferenceName");
            }

            if (!this.isMarionette)
            {
                throw new ArgumentException("Preferences cannot be set directly when using the legacy FirefoxDriver implementation. Set them in the profile.");
            }

            this.profilePreferences[preferenceName] = preferenceValue;
        }

        private void ImportCapabilities(DesiredCapabilities capabilities)
        {
            foreach (KeyValuePair<string, object> pair in capabilities.CapabilitiesDictionary)
            {
                if (pair.Key == CapabilityType.BrowserName)
                {
                }
                else if (pair.Key == CapabilityType.BrowserVersion)
                {
                    this.BrowserVersion = pair.Value.ToString();
                }
                else if (pair.Key == CapabilityType.PlatformName)
                {
                    this.PlatformName = pair.Value.ToString();
                }
                else if (pair.Key == CapabilityType.Proxy)
                {
                    this.Proxy = new Proxy(pair.Value as Dictionary<string, object>);
                }
                else if (pair.Key == CapabilityType.UnhandledPromptBehavior)
                {
                    this.UnhandledPromptBehavior = (UnhandledPromptBehavior)Enum.Parse(typeof(UnhandledPromptBehavior), pair.Value.ToString(), true);
                }
                else if (pair.Key == CapabilityType.PageLoadStrategy)
                {
                    this.PageLoadStrategy = (PageLoadStrategy)Enum.Parse(typeof(PageLoadStrategy), pair.Value.ToString(), true);
                }
                else if (pair.Key == FirefoxOptionsCapability)
                {
                    Dictionary<string, object> mozFirefoxOptions = pair.Value as Dictionary<string, object>;
                    foreach (KeyValuePair<string, object> option in mozFirefoxOptions)
                    {
                        if (option.Key == FirefoxArgumentsCapability)
                        {
                            object[] args = option.Value as object[];
                            for (int i = 0; i < args.Length; i++)
                            {
                                this.firefoxArguments.Add(args[i].ToString());
                            }
                        }
                        else if (option.Key == FirefoxPrefsCapability)
                        {
                            this.profilePreferences = option.Value as Dictionary<string, object>;
                        }
                        else if (option.Key == FirefoxLogCapability)
                        {
                            Dictionary<string, object> logDictionary = option.Value as Dictionary<string, object>;
                            if (logDictionary.ContainsKey("level"))
                            {
                                this.logLevel = (FirefoxDriverLogLevel)Enum.Parse(typeof(FirefoxDriverLogLevel), logDictionary["level"].ToString(), true);
                            }
                        }
                        else if (option.Key == FirefoxBinaryCapability)
                        {
                            this.browserBinaryLocation = option.Value.ToString();
                        }
                        else if (option.Key == FirefoxProfileCapability)
                        {
                            this.profile = FirefoxProfile.FromBase64String(option.Value.ToString());
                        }
                        else
                        {
                            this.AddAdditionalCapability(option.Key, option.Value);
                        }
                    }
                }
                else
                {
                    this.AddAdditionalCapability(pair.Key, pair.Value, true);
                }
            }
        }
    }
}
