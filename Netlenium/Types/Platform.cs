namespace Netlenium.Types
{
    /// <summary>
    /// Platform Indication
    /// </summary>
    public enum Platform
    {
        /// <summary>
        /// Auto-Detects the Platform that the system is running on
        /// </summary>
        AutoDetect = 0,
        
        /// <summary>
        /// Windows 32bit Platform
        /// </summary>
        Win32 = 1,
        
        /// <summary>
        /// Linux/Unix 32bit Platform
        /// </summary>
        Linux32 = 2,
        
        /// <summary>
        /// Linux/Unix 64bit Platform
        /// </summary>
        Linux64 = 3
    }
}
