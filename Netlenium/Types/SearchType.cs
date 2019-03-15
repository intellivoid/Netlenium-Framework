namespace Netlenium.Types
{
    /// <summary>
    /// Search Type for Getting Elements from the active DOM
    /// </summary>
    public enum SearchType
    {
        /// <summary>
        /// Gets Elements by Id
        /// </summary>
        Id = 0,
        
        /// <summary>
        /// Gets Elements by ClassName
        /// </summary>
        ClassName = 1,
        
        /// <summary>
        /// Gets Elements using CssSelector (Selenium Only)
        /// </summary>
        CssSelector = 2,
        
        /// <summary>
        /// Gets Elements using the TagName
        /// </summary>
        TagName = 3,
        
        /// <summary>
        /// Gets Elements by Name
        /// </summary>
        Name = 4
    }
}
