﻿using System;

namespace Netlenium.Driver.WebDriver
{
    /// <summary>
    /// Represents the known and supported Platforms that WebDriver runs on.
    /// </summary>
    /// <remarks>The <see cref="Platform"/> class maps closely to the Operating System,
    /// but differs slightly, because this class is used to extract information such as
    /// program locations and line endings. </remarks>
    public enum PlatformType
    {
        /// <summary>
        /// Any platform. This value is never returned by a driver, but can be used to find
        /// drivers with certain capabilities.
        /// </summary>
        Any,

        /// <summary>
        /// Any version of Microsoft Windows. This value is never returned by a driver,
        /// but can be used to find drivers with certain capabilities.
        /// </summary>
        Windows,

        /// <summary>
        /// Any Windows NT-based version of Microsoft Windows. This value is never returned
        /// by a driver, but can be used to find drivers with certain capabilities. This value
        /// is equivalent to PlatformType.Windows.
        /// </summary>
        WinNT = Windows,

        /// <summary>
        /// Versions of Microsoft Windows that are compatible with Windows XP.
        /// </summary>
        XP,

        /// <summary>
        /// Versions of Microsoft Windows that are compatible with Windows Vista.
        /// </summary>
        Vista,

        /// <summary>
        /// Any version of the Macintosh OS
        /// </summary>
        Mac,

        /// <summary>
        /// Any version of the Unix operating system.
        /// </summary>
        Unix,

        /// <summary>
        /// Any version of the Linux operating system.
        /// </summary>
        Linux,

        /// <summary>
        /// A version of the Android mobile operating system.
        /// </summary>
        Android
    }

    /// <summary>
    /// Represents the platform on which tests are to be run.
    /// </summary>
    public class Platform
    {
        private static Platform current;
        private PlatformType platformTypeValue;
        private int major;
        private int minor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Platform"/> class for a specific platform type.
        /// </summary>
        /// <param name="typeValue">The platform type.</param>
        public Platform(PlatformType typeValue)
        {
            platformTypeValue = typeValue;
        }

        private Platform()
        {
            major = Environment.OSVersion.Version.Major;
            minor = Environment.OSVersion.Version.Minor;

            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    if (major == 5)
                    {
                        platformTypeValue = PlatformType.XP;
                    }
                    else if (major == 6)
                    {
                        platformTypeValue = PlatformType.Vista;
                    }
                    else
                    {
                        platformTypeValue = PlatformType.Windows;
                    }

                    break;

                // Thanks to a bug in Mono Mac and Linux will be treated the same  https://bugzilla.novell.com/show_bug.cgi?id=515570 but adding this in case
                case PlatformID.MacOSX:
                    platformTypeValue = PlatformType.Mac;
                    break;

                case PlatformID.Unix:
                    platformTypeValue = PlatformType.Unix;
                    break;
            }
        }

        /// <summary>
        /// Gets the current platform.
        /// </summary>
        public static Platform CurrentPlatform
        {
            get
            {
                if (current == null)
                {
                    current = new Platform();
                }

                return current;
            }
        }

        /// <summary>
        /// Gets the major version of the platform operating system.
        /// </summary>
        public int MajorVersion
        {
            get { return major; }
        }

        /// <summary>
        /// Gets the major version of the platform operating system.
        /// </summary>
        public int MinorVersion
        {
            get { return minor; }
        }

        /// <summary>
        /// Gets the type of the platform.
        /// </summary>
        public PlatformType PlatformType
        {
            get { return platformTypeValue; }
        }

        /// <summary>
        /// Gets the value of the platform type for transmission using the JSON Wire Protocol.
        /// </summary>
        public string ProtocolPlatformType
        {
            get { return platformTypeValue.ToString("G").ToUpperInvariant(); }
        }

        /// <summary>
        /// Compares the platform to the specified type.
        /// </summary>
        /// <param name="compareTo">A <see cref="PlatformType"/> value to compare to.</param>
        /// <returns><see langword="true"/> if the platforms match; otherwise <see langword="false"/>.</returns>
        public bool IsPlatformType(PlatformType compareTo)
        {
            var platformIsType = false;
            switch (compareTo)
            {
                case PlatformType.Any:
                    platformIsType = true;
                    break;

                case PlatformType.Windows:
                    platformIsType = platformTypeValue == PlatformType.Windows || platformTypeValue == PlatformType.XP || platformTypeValue == PlatformType.Vista;
                    break;

                case PlatformType.Vista:
                    platformIsType = platformTypeValue == PlatformType.Windows || platformTypeValue == PlatformType.Vista;
                    break;

                case PlatformType.XP:
                    platformIsType = platformTypeValue == PlatformType.Windows || platformTypeValue == PlatformType.XP;
                    break;

                case PlatformType.Linux:
                    platformIsType = platformTypeValue == PlatformType.Linux || platformTypeValue == PlatformType.Unix;
                    break;

                default:
                    platformIsType = platformTypeValue == compareTo;
                    break;
            }

            return platformIsType;
        }

        /// <summary>
        /// Returns the string value for this platform type.
        /// </summary>
        /// <returns>The string value for this platform type.</returns>
        public override string ToString()
        {
            return platformTypeValue.ToString();
        }

        /// <summary>
        /// Creates a <see cref="Platform"/> object from a string name of the platform.
        /// </summary>
        /// <param name="platformName">The name of the platform to create.</param>
        /// <returns>The Platform object represented by the string name.</returns>
        internal static Platform FromString(string platformName)
        {
            var platformTypeFromString = PlatformType.Any;
            try
            {
                platformTypeFromString = (PlatformType)Enum.Parse(typeof(PlatformType), platformName, true);
            }
            catch (ArgumentException)
            {
                // If the requested platform string is not a valid platform type,
                // ignore it and use PlatformType.Any.
            }

            return new Platform(platformTypeFromString);
        }
    }
}
