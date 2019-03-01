using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace Netlenium.Forms
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
            this.VersionLabel.Text = $"Version: {FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion}";
            this.AboutTextBox.Text = Properties.Resources.AboutDialogText;
        }
    }
}
