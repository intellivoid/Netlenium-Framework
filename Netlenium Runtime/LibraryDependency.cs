namespace NetleniumRuntime
{
    /// <summary>
    /// Library Dependency Class
    /// </summary>
    public class LibraryDependency
    {
        /// <summary>
        /// The dependency name
        /// </summary>
        public string Dependency
        {
            get; set;
        }

        /// <summary>
        /// The version of the dependency
        /// </summary>
        public System.Version Version
        {
            get; set;
        }

        /// <summary>
        /// The File Name of the dependency
        /// </summary>
        public string FileName
        {
            get; set;
        }

        /// <summary>
        /// Internal File Name of the dependency
        /// </summary>
        public string Internal
        {
            get; set;
        }

        /// <summary>
        /// The publisher of the dependency
        /// </summary>
        public string Publisher
        {
            get; set;
        }
    }
}
