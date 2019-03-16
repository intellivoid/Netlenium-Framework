namespace NetleniumBuild
{
    /// <summary>
    /// Indicates the Message Type that will be formatted to be displayed in the CLI
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Generic Out Message
        /// </summary>
        Out = 0,
        
        /// <summary>
        /// Information Type Message
        /// </summary>
        Information = 1,
        
        /// <summary>
        /// Fatal Error Type Message
        /// </summary>
        Error = 2,
        
        /// <summary>
        /// Warning Message
        /// </summary>
        Warning = 3,
        
        /// <summary>
        /// Success Message
        /// </summary>
        Success = 4
    }
}
