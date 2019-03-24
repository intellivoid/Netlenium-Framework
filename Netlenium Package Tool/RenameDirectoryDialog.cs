using System;
using System.Windows.Forms;

namespace NetleniumPackageTool
{
    /// <summary>
    /// Rename Directory Dialog
    /// </summary>
    public partial class RenameDirectoryDialog : Form
    {
        /// <summary>
        /// Directory Name
        /// </summary>
        public string DirectoryName { get; set; }

        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="directoryName"></param>
        public RenameDirectoryDialog(string directoryName)
        {
            InitializeComponent();
            DirectoryName = directoryName;
            DirectoryNameTextBox.Text = DirectoryName;
        }

        /// <summary>
        /// When the text changes Enable/Disable the submit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DirectoryNameTextBox_TextChanged(object sender, EventArgs e)
        {
            DirectoryName = DirectoryNameTextBox.Text;

            if (DirectoryName.Length > 0)
            {
                RenameButton.Enabled = true;
            }
            else
            {
                RenameButton.Enabled = false;
            }
        }

        /// <summary>
        /// Submit changes via Enter Key
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

        /// <summary>
        /// When the dialog is shown, select the TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenameDirectoryDialog_Shown(object sender, EventArgs e)
        {
            DirectoryNameTextBox.Focus();
            DirectoryNameTextBox.SelectAll();
        }
    }
}
