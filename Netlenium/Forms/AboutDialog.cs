using System.Diagnostics;
using System.Reflection;
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
            VersionLabel.Text = $"Version: {FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion}";
            AboutTextBox.Text = Properties.Resources.AboutDialogText;
        }
    }
}
