using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetleniumPackageTool
{
    public partial class RenameFileDialog : Form
    {
        /// <summary>
        /// File Name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="fileName"></param>
        public RenameFileDialog(string fileName)
        {
            InitializeComponent();
            FileName = fileName;
            FileNameTextBox.Text = FileName;
        }

        /// <summary>
        /// Auto-Focus on the textfield once the form is displayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenameFileDialog_Shown(object sender, EventArgs e)
        {
            FileNameTextBox.Focus();
            FileNameTextBox.SelectAll();
        }

        /// <summary>
        /// Enables/Disables the Rename Button when the text changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            FileName = FileNameTextBox.Text;

            if (FileName.Length > 0)
            {
                RenameButton.Enabled = true;
            }
            else
            {
                RenameButton.Enabled = false;
            }
        }

        /// <summary>
        /// Submits the the Enter Key button
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
