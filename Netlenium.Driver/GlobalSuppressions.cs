[assembly: System.CLSCompliant(true)]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Netlenium.Driver.WebDriver.By.#FindElementsMethod", Justification = "Type is properly specified. It should be a Func<T, TResult> that returns a ReadOnlyCollection<IWebElement>")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Netlenium.Driver.WebDriver.By.#.ctor(System.Func`2<Netlenium.Driver.WebDriver.ISearchContext,Netlenium.Driver.WebDriver.IWebElement>,System.Func`2<Netlenium.Driver.WebDriver.ISearchContext,System.Collections.ObjectModel.ReadOnlyCollection`1<Netlenium.Driver.WebDriver.IWebElement>>)", Justification = "Type is properly specified. It should be a Func<T, TResult> that returns a ReadOnlyCollection<IWebElement>")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Netlenium.Driver.WebDriver.PhantomJS", Justification = "Namespaces are properly scoped.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Netlenium.Driver.WebDriver.Interactions", Justification = "Namespaces are properly scoped.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Netlenium.Driver.WebDriver.Chrome", Justification = "Namespaces are properly scoped.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Netlenium.Driver.WebDriver.Opera", Justification = "Namespaces are properly scoped.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Netlenium.Driver.WebDriver.Safari", Justification = "Namespaces are properly scoped.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Netlenium.Driver.WebDriver.ITakesScreenshot.#GetScreenshot()", Justification = "API specification demands method.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags", Scope = "type", Target = "Netlenium.Driver.WebDriver.ProxyKind", Justification = "The ProxyKind enum is not a set of flags, but has values determined by an external API.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags", Scope = "type", Target = "Netlenium.Driver.WebDriver.WebDriverResult", Justification = "The WebDriverResult enum is not a set of flags, but has values determined by an external API.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "Netlenium.Driver.WebDriver.IWebDriver.#Url", Justification = "Specification demands string value for property.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "Netlenium.Driver.WebDriver.Proxy.#ProxyAutoConfigUrl", Justification = "Proxy configuration can be string instead of Uri.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "Netlenium.Driver.WebDriver.IE.InternetExplorerOptions.#InitialBrowserUrl", Justification = "InitialBrowserUrl should be string instead of Uri.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "Netlenium.Driver.WebDriver.Chrome.ChromeDriverService.#UrlPathPrefix", Justification = "UrlPathPrefix is a prefix for use with ChromeDriver, and should be a string.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "Netlenium.Driver.WebDriver.Opera.OperaDriverService.#UrlPathPrefix", Justification = "UrlPathPrefix is a prefix for use with OperaDriver, and should be a string.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "Netlenium.Driver.WebDriver.PhantomJS.PhantomJSDriverService.#GridHubUrl", Justification = "GridHubUrl is a command line for use with PhantomJS, and should properly be a string.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String,System.Object)", Scope = "member", Target = "Netlenium.Driver.WebDriver.Internal.FileUtilities.#DeleteDirectory(System.String)", Justification = "Informational message only.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Netlenium.Driver.WebDriver.Firefox.Preferences.#SetPreferenceValue(System.String,System.Object)", Justification = "Strings are normalized to lower case by JSON wire protocol.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Netlenium.Driver.WebDriver.Remote.RemoteWebElement.#GetAttribute(System.String)", Justification = "Strings are normalized to lower case by JSON wire protocol.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Netlenium.Driver.WebDriver.Firefox.FirefoxDriverService.#CommandLineArguments", Justification = "Strings are normalized to lower case by JSON wire protocol.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Netlenium.Driver.WebDriver.Interactions.PointerInputDevice+PointerMoveInteraction.#Serialize()", Justification = "Strings are normalized to lower case by JSON wire protocol.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Netlenium.Driver.WebDriver.Interactions.PointerInputDevice.#Serialize()", Justification = "Strings are normalized to lower case by JSON wire protocol.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Netlenium.Driver.WebDriver.Firefox.FirefoxOptions.#GenerateFirefoxOptionsDictionary()", Justification = "Strings are normalized to lower case by JSON wire protocol.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Netlenium.Driver.WebDriver.Interactions.PointerInputDevice+PointerMoveInteraction.#ToDictionary()", Justification = "Strings are normalized to lower case by JSON wire protocol.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member", Target = "Netlenium.Driver.WebDriver.Interactions.PointerInputDevice.#ToDictionary()", Justification = "Strings are normalized to lower case by JSON wire protocol.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "type", Target = "Netlenium.Driver.WebDriver.Remote.RemoteWebDriver", Justification = "RemoteWebDriver is a large class, and will have tight couplings with a lot of classes.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "OnScreen", Scope = "member", Target = "Netlenium.Driver.WebDriver.Interactions.Internal.ICoordinates.#LocationOnScreen", Justification = "On Screen is properly used as two-word discrete term.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "OnScreen", Scope = "member", Target = "Netlenium.Driver.WebDriver.ILocatable.#LocationOnScreenOnceScrolledIntoView", Justification = "On Screen is properly used as two-word discrete term.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BridgePort", Scope = "member", Target = "Netlenium.Driver.WebDriver.Chrome.ChromeDriverService.#AndroidDebugBridgePort", Justification = "Bridge Port is properly used as two-word discrete term.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BridgePort", Scope = "member", Target = "Netlenium.Driver.WebDriver.Opera.OperaDriverService.#AndroidDebugBridgePort", Justification = "Bridge Port is properly used as two-word discrete term.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "TouchScreen", Scope = "type", Target = "Netlenium.Driver.WebDriver.IHasTouchScreen", Justification = "Touch Screen is properly used as two-word discrete term.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "TouchScreen", Scope = "member", Target = "Netlenium.Driver.WebDriver.IHasTouchScreen.#TouchScreen", Justification = "Touch Screen is properly used as two-word discrete term.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "TouchScreen", Scope = "type", Target = "Netlenium.Driver.WebDriver.ITouchScreen", Justification = "Touch Screen is properly used as two-word discrete term.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "TouchScreen", Scope = "type", Target = "Netlenium.Driver.WebDriver.Remote.RemoteTouchScreen", Justification = "Touch Screen is properly used as two-word discrete term.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "xpath", Scope = "member", Target = "Netlenium.Driver.WebDriver.By.#XPath(System.String)", Justification = "XPath is spelled correctly.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "xpath", Scope = "member", Target = "Netlenium.Driver.WebDriver.Internal.IFindsByXPath.#FindElementByXPath(System.String)", Justification = "XPath is spelled correctly.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "xpath", Scope = "member", Target = "Netlenium.Driver.WebDriver.Internal.IFindsByXPath.#FindElementsByXPath(System.String)", Justification = "XPath is spelled correctly.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Minidump", Scope = "member", Target = "Netlenium.Driver.WebDriver.Chrome.ChromeOptions.#MinidumpPath", Justification = "Minidump is spelled correctly.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Minidump", Scope = "member", Target = "Netlenium.Driver.WebDriver.Opera.OperaOptions.#MinidumpPath", Justification = "Minidump is spelled correctly.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Api", Scope = "member", Target = "Netlenium.Driver.WebDriver.IE.InternetExplorerOptions.#ForceCreateProcessApi", Justification = "API is spelled and cased correctly for use in method names.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Api", Scope = "member", Target = "Netlenium.Driver.WebDriver.IE.InternetExplorerOptions.#ForceShellWindowsApi", Justification = "API is spelled and cased correctly for use in method names.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Uncached", Scope = "member", Target = "Netlenium.Driver.WebDriver.Html5.AppCacheStatus.#Uncached", Justification = "Uncached property is correctly spelled.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multiprocess", Scope = "member", Target = "Netlenium.Driver.WebDriver.Firefox.FirefoxDriverService.#DisableBrowserMultiprocessSupport", Justification = "Multiprocess is correctly spelled.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Scope = "member", Target = "Netlenium.Driver.WebDriver.Interactions.PointerInputDevice.#CreatePointerMove(Netlenium.Driver.WebDriver.IWebElement,Netlenium.Driver.WebDriver.Interactions.CoordinateOrigin,System.Int32,System.Int32,System.TimeSpan)", Justification = "An x-offset is an appropriate name for the parameter.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Scope = "member", Target = "Netlenium.Driver.WebDriver.Interactions.PointerInputDevice.#CreatePointerMove(Netlenium.Driver.WebDriver.IWebElement,Netlenium.Driver.WebDriver.Interactions.CoordinateOrigin,System.Int32,System.Int32,System.TimeSpan)", Justification = "A y-offset is an appropriate name for the parameter.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Png", Scope = "member", Target = "Netlenium.Driver.WebDriver.ScreenshotImageFormat.#Png", Justification = "PNG is the propert term for Portable Network Graphics.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "Netlenium.Driver.WebDriver.Cookie.#ExpirySeconds", Justification = "This property only exists so that the JSON serializer can serialize a cookie without resorting to a custom converter.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "Netlenium.Driver.WebDriver.Remote.ErrorResponse.#StackTrace", Justification = "Specification compliance demands use of an array.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "Netlenium.Driver.WebDriver.Screenshot.#AsByteArray", Justification = "Specification compliance demands use of an array.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Netlenium.Driver.WebDriver.Chrome.ChromeDriver.#.ctor(Netlenium.Driver.WebDriver.Chrome.ChromeOptions)", Justification = "Driver ensures that all dependent service objects are properly disposed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Netlenium.Driver.WebDriver.Chrome.ChromeDriver.#.ctor(System.String,Netlenium.Driver.WebDriver.Chrome.ChromeOptions,System.TimeSpan)", Justification = "Driver ensures that all dependent service objects are properly disposed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Netlenium.Driver.WebDriver.Edge.EdgeDriver.#.ctor(Netlenium.Driver.WebDriver.Edge.EdgeOptions)", Justification = "Driver ensures that all dependent service objects are properly disposed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Netlenium.Driver.WebDriver.Edge.EdgeDriver.#.ctor(System.String,Netlenium.Driver.WebDriver.Edge.EdgeOptions,System.TimeSpan)", Justification = "Driver ensures that all dependent service objects are properly disposed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Netlenium.Driver.WebDriver.IE.InternetExplorerDriver.#.ctor(Netlenium.Driver.WebDriver.IE.InternetExplorerOptions)", Justification = "Driver ensures that all dependent service objects are properly disposed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Netlenium.Driver.WebDriver.IE.InternetExplorerDriver.#.ctor(System.String,Netlenium.Driver.WebDriver.IE.InternetExplorerOptions,System.TimeSpan)", Justification = "Driver ensures that all dependent service objects are properly disposed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Netlenium.Driver.WebDriver.Opera.OperaDriver.#.ctor(Netlenium.Driver.WebDriver.Opera.OperaOptions)", Justification = "Driver ensures that all dependent service objects are properly disposed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Netlenium.Driver.WebDriver.Opera.OperaDriver.#.ctor(System.String,Netlenium.Driver.WebDriver.Opera.OperaOptions,System.TimeSpan)", Justification = "Driver ensures that all dependent service objects are properly disposed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Netlenium.Driver.WebDriver.PhantomJS.PhantomJSDriver.#.ctor(Netlenium.Driver.WebDriver.PhantomJS.PhantomJSOptions)", Justification = "Driver ensures that all dependent service objects are properly disposed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Netlenium.Driver.WebDriver.PhantomJS.PhantomJSDriver.#.ctor(System.String,Netlenium.Driver.WebDriver.PhantomJS.PhantomJSOptions,System.TimeSpan)", Justification = "Driver ensures that all dependent service objects are properly disposed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Netlenium.Driver.WebDriver.Safari.SafariDriver.#.ctor(Netlenium.Driver.WebDriver.Safari.SafariOptions)", Justification = "Driver ensures that all dependent service objects are properly disposed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Netlenium.Driver.WebDriver.Safari.SafariDriver.#.ctor(System.String,Netlenium.Driver.WebDriver.Safari.SafariOptions,System.TimeSpan)", Justification = "Driver ensures that all dependent service objects are properly disposed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Netlenium.Driver.WebDriver.Firefox.FirefoxDriver.#.ctor(Netlenium.Driver.WebDriver.Firefox.FirefoxOptions)", Justification = "Driver ensures that all dependent service objects are properly disposed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Netlenium.Driver.WebDriver.Firefox.FirefoxDriver.#.ctor()", Justification = "Driver ensures that all dependent service objects are properly disposed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Netlenium.Driver.WebDriver.Firefox.FirefoxDriver.#.ctor(Netlenium.Driver.WebDriver.Firefox.FirefoxProfile)", Justification = "Driver ensures that all dependent service objects are properly disposed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Netlenium.Driver.WebDriver.Firefox.FirefoxDriver.#CreateExtensionConnection(Netlenium.Driver.WebDriver.Firefox.FirefoxBinary,Netlenium.Driver.WebDriver.Firefox.FirefoxProfile,System.TimeSpan)", Justification = "Driver ensures that all dependent service objects are properly disposed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Scope = "member", Target = "Netlenium.Driver.WebDriver.Firefox.FirefoxProfile.#FromBase64String(System.String)", Justification = "Separate disposal of the stream object is approved, and ensures disposal if there are exceptions in nested object constructor or method.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Scope = "member", Target = "Netlenium.Driver.WebDriver.Firefox.FirefoxProfile.#ReadDefaultPreferences()", Justification = "Separate disposal of the stream object is approved, and ensures disposal if there are exceptions in nested object constructor or method.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Scope = "member", Target = "Netlenium.Driver.WebDriver.Firefox.FirefoxProfile.#ToBase64String()", Justification = "Separate disposal of the stream object is approved, and ensures disposal if there are exceptions in nested object constructor or method.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Scope = "member", Target = "Netlenium.Driver.WebDriver.Remote.RemoteWebElement.#UploadFile(System.String)", Justification = "Separate disposal of the stream object is approved, and ensures disposal if there are exceptions in nested object constructor or method.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Scope = "member", Target = "Netlenium.Driver.WebDriver.Remote.RemoteWebElement.#GetAtom(System.String)", Justification = "Separate disposal of the stream object is approved, and ensures disposal if there are exceptions in nested object constructor or method.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "WebDriver", Scope = "member", Target = "Netlenium.Driver.WebDriver.Remote.HttpCommandExecutor.#CreateResponse(System.Net.WebRequest)", Justification = "WebDriver is correctly used as a single word.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "HasApplicationCache", Scope = "member", Target = "Netlenium.Driver.WebDriver.Remote.RemoteWebDriver.#ApplicationCache", Justification = "HasApplicationCache property name is properly spelled")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "HasLocationContext", Scope = "member", Target = "Netlenium.Driver.WebDriver.Remote.RemoteWebDriver.#LocationContext", Justification = "HasLocationContext property name is properly spelled")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "HasWebStorage", Scope = "member", Target = "Netlenium.Driver.WebDriver.Remote.RemoteWebDriver.#WebStorage", Justification = "HasWebStorage property name is properly spelled")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "geolocation", Scope = "member", Target = "Netlenium.Driver.WebDriver.Remote.RemoteWebDriver.#LocationContext", Justification = "Geolocation is properly spelled")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "Netlenium.Driver.WebDriver.Remote.RemoteWebDriver.#.ctor(Netlenium.Driver.WebDriver.Remote.ICommandExecutor,Netlenium.Driver.WebDriver.ICapabilities)", Justification = "Class provides a hook for subclasses to modify functionality, so virtual method call in constructor is appropriate.")]

// Temporary suppressions until Interactions API is fully updated.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "Netlenium.Driver.WebDriver.Interactions.PauseInteraction.#.ctor(Netlenium.Driver.WebDriver.Interactions.InputDevice)", Justification = "Temporary suppressions until Interactions API is made public.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "Netlenium.Driver.WebDriver.Interactions.PointerInputDevice.#.ctor(Netlenium.Driver.WebDriver.Interactions.PointerKind)", Justification = "Temporary suppressions until Interactions API is made public.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "Netlenium.Driver.WebDriver.Interactions.PointerInputDevice.#CreatePointerCancel()", Justification = "Temporary suppressions until Interactions API is made public.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "Netlenium.Driver.WebDriver.Interactions.PointerInputDevice+PointerCancelInteraction.#.ctor(Netlenium.Driver.WebDriver.Interactions.InputDevice)", Justification = "Temporary suppressions until Interactions API is made public.")]
