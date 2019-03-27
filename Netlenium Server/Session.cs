namespace Netlenium_Server
{
    public class Session
    {
        /// <summary>
        /// The current session ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The Object Controller that was initialized
        /// </summary>
        public Netlenium.Driver.Controller ObjectController;

    }
}
