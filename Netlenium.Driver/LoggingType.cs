namespace Netlenium.Driver
{
    internal enum LoggingType
    {
        /// <summary>
        /// [ ~~~ ]
        /// </summary>
        Information = 0,
        
        /// <summary>
        /// [  !  ]
        /// </summary>
        Warning = 1,
        
        /// <summary>
        /// [  X  ]
        /// </summary>
        Error = 2,
        
        /// <summary>
        /// [  +  ]
        /// </summary>
        Success = 3,
        
        /// <summary>
        /// [ ... ]
        /// </summary>
        InProgress = 4
    }
}