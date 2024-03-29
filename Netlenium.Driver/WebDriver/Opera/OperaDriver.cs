using System;
using Netlenium.Driver.WebDriver.Remote;

namespace Netlenium.Driver.WebDriver.Opera
{
    /// <summary>
    /// Provides a mechanism to write tests against Opera
    /// </summary>
    /// <example>
    /// <code>
    /// [TestFixture]
    /// public class Testing
    /// {
    ///     private IWebDriver driver;
    ///     <para></para>
    ///     [SetUp]
    ///     public void SetUp()
    ///     {
    ///         driver = new OperaDriver();
    ///     }
    ///     <para></para>
    ///     [Test]
    ///     public void TestGoogle()
    ///     {
    ///         driver.Navigate().GoToUrl("http://www.google.co.uk");
    ///         /*
    ///         *   Rest of the test
    ///         */
    ///     }
    ///     <para></para>
    ///     [TearDown]
    ///     public void TearDown()
    ///     {
    ///         driver.Quit();
    ///     }
    /// }
    /// </code>
    /// </example>
    public class OperaDriver : RemoteWebDriver
    {
        /// <summary>
        /// Accept untrusted SSL Certificates
        /// </summary>
        public static readonly bool AcceptUntrustedCertificates = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperaDriver"/> class.
        /// </summary>
        public OperaDriver()
            : this(new OperaOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperaDriver"/> class using the specified options.
        /// </summary>
        /// <param name="options">The <see cref="OperaOptions"/> to be used with the Opera driver.</param>
        public OperaDriver(OperaOptions options)
            : this(OperaDriverService.CreateDefaultService(), options, DefaultCommandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperaDriver"/> class using the specified path
        /// to the directory containing OperaDriver.exe.
        /// </summary>
        /// <param name="operaDriverDirectory">The full path to the directory containing OperaDriver.exe.</param>
        public OperaDriver(string operaDriverDirectory)
            : this(operaDriverDirectory, new OperaOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperaDriver"/> class using the specified path
        /// to the directory containing OperaDriver.exe and options.
        /// </summary>
        /// <param name="operaDriverDirectory">The full path to the directory containing OperaDriver.exe.</param>
        /// <param name="options">The <see cref="OperaOptions"/> to be used with the Opera driver.</param>
        public OperaDriver(string operaDriverDirectory, OperaOptions options)
            : this(operaDriverDirectory, options, DefaultCommandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperaDriver"/> class using the specified path
        /// to the directory containing OperaDriver.exe, options, and command timeout.
        /// </summary>
        /// <param name="operaDriverDirectory">The full path to the directory containing OperaDriver.exe.</param>
        /// <param name="options">The <see cref="OperaOptions"/> to be used with the Opera driver.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public OperaDriver(string operaDriverDirectory, OperaOptions options, TimeSpan commandTimeout)
            : this(OperaDriverService.CreateDefaultService(operaDriverDirectory), options, commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperaDriver"/> class using the specified
        /// <see cref="OperaDriverService"/> and options.
        /// </summary>
        /// <param name="service">The <see cref="OperaDriverService"/> to use.</param>
        /// <param name="options">The <see cref="OperaOptions"/> used to initialize the driver.</param>
        public OperaDriver(OperaDriverService service, OperaOptions options)
            : this(service, options, DefaultCommandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperaDriver"/> class using the specified <see cref="OperaDriverService"/>.
        /// </summary>
        /// <param name="service">The <see cref="OperaDriverService"/> to use.</param>
        /// <param name="options">The <see cref="OperaOptions"/> to be used with the Opera driver.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public OperaDriver(OperaDriverService service, OperaOptions options, TimeSpan commandTimeout)
            : base(new DriverServiceCommandExecutor(service, commandTimeout), ConvertOptionsToCapabilities(options))
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="IFileDetector"/> responsible for detecting
        /// sequences of keystrokes representing file paths and names.
        /// </summary>
        /// <remarks>The Opera driver does not allow a file detector to be set,
        /// as the server component of the Opera driver (OperaDriver.exe) only
        /// allows uploads from the local computer environment. Attempting to set
        /// this property has no effect, but does not throw an exception. If you
        /// are attempting to run the Opera driver remotely, use <see cref="RemoteWebDriver"/>
        /// in conjunction with a standalone WebDriver server.</remarks>
        public override IFileDetector FileDetector
        {
            get { return base.FileDetector; }
            set { }
        }

        private static ICapabilities ConvertOptionsToCapabilities(OperaOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options", "options must not be null");
            }

            return options.ToCapabilities();
        }
    }
}
