using System;
using System.Globalization;
using System.Text;
using Netlenium.Driver.WebDriver.Internal;

namespace Netlenium.Driver.WebDriver.Edge
{
    /// <summary>
    /// Exposes the service provided by the native MicrosoftWebDriver executable.
    /// </summary>
    public sealed class EdgeDriverService : DriverService
    {
        private const string MicrosoftWebDriverServiceFileName = "MicrosoftWebDriver.exe";
        private static readonly Uri MicrosoftWebDriverDownloadUrl = new Uri("http://go.microsoft.com/fwlink/?LinkId=619687");
        private string host;
        private string package;
        private bool useVerboseLogging;
        private bool? useSpecCompliantProtocol;

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeDriverService"/> class.
        /// </summary>
        /// <param name="executablePath">The full path to the EdgeDriver executable.</param>
        /// <param name="executableFileName">The file name of the EdgeDriver executable.</param>
        /// <param name="port">The port on which the EdgeDriver executable should listen.</param>
        private EdgeDriverService(string executablePath, string executableFileName, int port)
            : base(executablePath, port, executableFileName, MicrosoftWebDriverDownloadUrl)
        {
        }

        /// <summary>
        /// Gets or sets the value of the host adapter on which the Edge driver service should listen for connections.
        /// </summary>
        public string Host
        {
            get { return host; }
            set { host = value; }
        }

        /// <summary>
        /// Gets or sets the value of the package the Edge driver service will launch and automate.
        /// </summary>
        public string Package
        {
            get { return package; }
            set { package = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the service should use verbose logging.
        /// </summary>
        public bool UseVerboseLogging
        {
            get { return useVerboseLogging; }
            set { useVerboseLogging = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="EdgeDriverService"/> instance
        /// should use the a protocol dialect compliant with the W3C WebDriver Specification.
        /// </summary>
        /// <remarks>
        /// Setting this property to a non-<see langword="null"/> value for driver
        /// executables matched to versions of Windows before the 2018 Fall Creators
        /// Update will result in a the driver executable shutting down without
        /// execution, and all commands will fail. Do not set this property unless
        /// you are certain your version of the MicrosoftWebDriver.exe supports the
        /// --w3c and --jwp command-line arguments.
        /// </remarks>
        public bool? UseSpecCompliantProtocol
        {
            get { return useSpecCompliantProtocol; }
            set { useSpecCompliantProtocol = value; }
        }

        /// <summary>
        /// Gets a value indicating whether the service has a shutdown API that can be called to terminate
        /// it gracefully before forcing a termination.
        /// </summary>
        protected override bool HasShutdown
        {
            get
            {
                if (useSpecCompliantProtocol.HasValue && !useSpecCompliantProtocol.Value)
                {
                    return base.HasShutdown;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating the time to wait for the service to terminate before forcing it to terminate.
        /// </summary>
        protected override TimeSpan TerminationTimeout
        {
            // Use a very small timeout for terminating the Edge driver,
            // because the executable does not have a clean shutdown command,
            // which means we have to kill the process. Using a short timeout
            // gets us to the termination point much faster.
            get
            {
                if (useSpecCompliantProtocol.HasValue && !useSpecCompliantProtocol.Value)
                {
                    return base.TerminationTimeout;
                }

                return TimeSpan.FromMilliseconds(100);
            }
        }

        /// <summary>
        /// Gets the command-line arguments for the driver service.
        /// </summary>
        protected override string CommandLineArguments
        {
            get
            {
                var argsBuilder = new StringBuilder(base.CommandLineArguments);
                if (!string.IsNullOrEmpty(host))
                {
                    argsBuilder.Append(string.Format(CultureInfo.InvariantCulture, " --host={0}", host));
                }

                if (!string.IsNullOrEmpty(package))
                {
                    argsBuilder.Append(string.Format(CultureInfo.InvariantCulture, " --package={0}", package));
                }

                if (useVerboseLogging)
                {
                    argsBuilder.Append(" --verbose");
                }

                if (SuppressInitialDiagnosticInformation)
                {
                    argsBuilder.Append(" --silent");
                }

                if (useSpecCompliantProtocol.HasValue)
                {
                    if (useSpecCompliantProtocol.Value)
                    {
                        argsBuilder.Append(" --w3c");
                    }
                    else
                    {
                        argsBuilder.Append(" --jwp");
                    }
                }

                return argsBuilder.ToString();
            }
        }

        /// <summary>
        /// Creates a default instance of the EdgeDriverService.
        /// </summary>
        /// <returns>A EdgeDriverService that implements default settings.</returns>
        public static EdgeDriverService CreateDefaultService()
        {
            var serviceDirectory = FindDriverServiceExecutable(MicrosoftWebDriverServiceFileName, MicrosoftWebDriverDownloadUrl);
            var service = CreateDefaultService(serviceDirectory);
            return service;
        }

        /// <summary>
        /// Creates a default instance of the EdgeDriverService using a specified path to the EdgeDriver executable.
        /// </summary>
        /// <param name="driverPath">The directory containing the EdgeDriver executable.</param>
        /// <returns>A EdgeDriverService using a random port.</returns>
        public static EdgeDriverService CreateDefaultService(string driverPath)
        {
            return CreateDefaultService(driverPath, MicrosoftWebDriverServiceFileName);
        }

        /// <summary>
        /// Creates a default instance of the EdgeDriverService using a specified path to the EdgeDriver executable with the given name.
        /// </summary>
        /// <param name="driverPath">The directory containing the EdgeDriver executable.</param>
        /// <param name="driverExecutableFileName">The name of the EdgeDriver executable file.</param>
        /// <returns>A EdgeDriverService using a random port.</returns>
        public static EdgeDriverService CreateDefaultService(string driverPath, string driverExecutableFileName)
        {
            return CreateDefaultService(driverPath, driverExecutableFileName, PortUtilities.FindFreePort());
        }

        /// <summary>
        /// Creates a default instance of the EdgeDriverService using a specified path to the EdgeDriver executable with the given name and listening port.
        /// </summary>
        /// <param name="driverPath">The directory containing the EdgeDriver executable.</param>
        /// <param name="driverExecutableFileName">The name of the EdgeDriver executable file</param>
        /// <param name="port">The port number on which the driver will listen</param>
        /// <returns>A EdgeDriverService using the specified port.</returns>
        public static EdgeDriverService CreateDefaultService(string driverPath, string driverExecutableFileName, int port)
        {
            return new EdgeDriverService(driverPath, driverExecutableFileName, port);
        }
    }
}
