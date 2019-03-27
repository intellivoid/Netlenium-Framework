namespace Netlenium_Server
{
    public class SessionConfiguration
    {
        /// <summary>
        /// Starts the Browser as headless
        /// </summary>
        public bool Headless { get; set; }

        /// <summary>
        /// The target driver
        /// </summary>
        public string TargetDriver { get; set; }
    }
}
