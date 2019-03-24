using System;
using System.Windows.Forms;

namespace NetleniumPackageTool
{
    /// <summary>
    /// Prompts the user for a FileName input
    /// </summary>
    public partial class NewFileDialog : Form
    {
        /// <summary>
        /// File Name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Public Constructor
        /// </summary>
        public NewFileDialog()
        {
            InitializeComponent();
            FileNameTextBox.Focus();
        }

        /// <summary>
        /// Raises when the text is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            FileName = FileNameTextBox.Text;

            if (FileName.Length > 0)
            {
                CreateButton.Enabled = true;
            }
            else
            {
                CreateButton.Enabled = false;
            }
        }

        /// <summary>
        /// Submits on enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
