namespace Netlenium.Driver
{
    /// <summary>
    /// Driver Client for controlling a Browser/WebDriver
    /// </summary>
    public class Client
    {

        /// <summary>
        /// The target driver that this client will control
        /// </summary>
        private BrowserType TargetBrowser { get; }

        /// <summary>
        /// Controls the driver interface
        /// </summary>
        public readonly IController Controller;
        
        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="targetBrowser">The browser that this Client will utilize</param>
        public Client(BrowserType targetBrowser)
        {
            TargetBrowser = targetBrowser;
            
            switch (TargetBrowser)
            {
                case BrowserType.Chrome:
                    Controller = new Chrome.Controller();
                    break;
                
                default:
                    throw new UnsupportedBrowserTypeException();
            }
            
        }
        
    }
}
