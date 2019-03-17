using System.Windows.Forms;

namespace Netlenium.Forms
{
    /// <summary>
    /// About Dialog for Netlenium Framework
    /// </summary>
    public partial class AboutDialog : Form
    {
        /// <summary>
        /// Public Constructor for Form
        /// </summary>
        public AboutDialog()
        {
            InitializeComponent();
            VersionLabel.Text = @"Version: 1.0.0.1";
            AboutTextBox.Text = Properties.Resources.AboutDialogText;
        }
    }
}
