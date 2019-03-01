using System;
using System.Windows.Forms;

namespace Netlenium.Driver.GeckoFXLib.Forms
{
    public partial class WebView : Form
    {

        public bool DocumentReady;

        /// <summary>
        /// Public Constructor
        /// </summary>
        public WebView()
        {

            InitializeComponent();
        }

        /// <summary>
        /// Raises when the Status Text has changed for the Gecko Web Browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeckoWebBrowser_StatusTextChanged(object sender, EventArgs e)
        {
            ToolStripStatusLabel.Text = GeckoWebBrowser.StatusText;
            if(GeckoWebBrowser.StatusText.Length > 0)
            {
                Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver.GeckoFXLib", GeckoWebBrowser.StatusText);
            }
        }

        /// <summary>
        /// Handles the rendering for the progress bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeckoWebBrowser_ProgressChanged(object sender, Gecko.GeckoProgressEventArgs e)
        {
            ToolStripProgressBar.Visible = true;
            ToolStripProgressBar.Maximum = Convert.ToInt32(e.MaximumProgress);
            ToolStripProgressBar.Value = Convert.ToInt32(e.MaximumProgress);

            if(ToolStripProgressBar.Value == ToolStripProgressBar.Maximum)
            {
                ToolStripProgressBar.Visible = false;
            }
        }

        /// <summary>
        /// Sets the document ready variable to true
        /// </summary>
        ///// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeckoWebBrowser_DocumentCompleted(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
        {
            this.DocumentReady = true;
            Logging.WriteEntry(Types.LogType.Information, "Netlenium.Driver.GeckoFXLib", "Document Loaded");
        }

        /// <summary>
        /// Terminates the process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Displays the About Dialog for Netlenium
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutNetleniumMenuItem_Click(object sender, EventArgs e)
        {
            Netlenium.Forms.AboutDialog AboutDialog = new Netlenium.Forms.AboutDialog();
            AboutDialog.ShowDialog();
        }

        /// <summary>
        /// Terminates the process when the Web view is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
