using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NetleniumPackageTool
{
    /// <summary>
    /// New Directory dialog which asks for user input
    /// </summary>
    public partial class NewDirectoryDialog : Form
    {
        /// <summary>
        /// Directory Name
        /// </summary>
        public string DirectoryName { get; set; }

        /// <summary>
        /// Public Constructor
        /// </summary>
        public NewDirectoryDialog()
        {
            InitializeComponent();
            DirectoryNameTextBox.Focus();
        }

        /// <summary>
        /// When the Directory Name text changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DirectoryNameTextBox_TextChanged(object sender, EventArgs e)
        {
            DirectoryName = DirectoryNameTextBox.Text;

            if(DirectoryName.Length > 0)
            {
                CreateButton.Enabled = true;
            }
            else
            {
                CreateButton.Enabled = false;
            }
        }

        /// <summary>
        /// Enter key shortcut for submitting changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DirectoryNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
