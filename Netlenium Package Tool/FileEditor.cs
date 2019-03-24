using ScintillaNET;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NetleniumPackageTool
{
    /// <summary>
    /// File Editor Window
    /// </summary>
    public partial class FileEditor : Form
    {
        /// <summary>
        /// The current file that's opened
        /// </summary>
        private string OpenedFile;

        /// <summary>
        /// Indicates if the editor has changed anything about the file
        /// </summary>
        private bool Changed;

        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="fileLocation"></param>
        public FileEditor(string fileLocation)
        {
            InitializeComponent();
            this.OpenedFile = fileLocation;

            try
            {
                LoadFile();
            }
            catch (Exception)
            {
                Close();
                return;
            }

            Show();
        }

        /// <summary>
        /// Updates the title
        /// </summary>
        private void UpdateTitle()
        {
            if(Changed == true)
            {
                this.Text = $"Netlenium Package Tool - {OpenedFile}*";
            }
            else
            {
                this.Text = $"Netlenium Package Tool - {OpenedFile}";
            }
        }

        /// <summary>
        /// Sets the zoom level to default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefaultZoomMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.Zoom = 0;
        }

        /// <summary>
        /// Zoom in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomInMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.ZoomIn();
        }

        /// <summary>
        /// Zoom out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomOutMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.ZoomOut();
        }

        /// <summary>
        /// Enables/Disable tab indenting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabIndentingMenuItem_Click(object sender, EventArgs e)
        {
            if(TabIndentingMenuItem.Checked == true)
            {
                TextArea.UseTabs = false;
                TabIndentingMenuItem.Checked = false;
            }
            else
            {
                TextArea.UseTabs = true;
                TabIndentingMenuItem.Checked = true;
            }
        }

        /// <summary>
        /// Raises when the text changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextArea_TextChanged(object sender, EventArgs e)
        {
            UndoMenuItem.Enabled = TextArea.CanUndo;
            RedoMenuItem.Enabled = TextArea.CanRedo;
            Changed = true;
            UpdateTitle();
        }

        /// <summary>
        /// Undo changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UndoMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.Undo();
        }

        /// <summary>
        /// Redo changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RedoMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.Redo();
        }

        /// <summary>
        /// Loads the file from disk
        /// </summary>
        private void LoadFile()
        {
            TextArea.WrapMode = WrapMode.None;
            TextArea.IndentationGuides = IndentView.LookBoth;

            try
            {
                TextArea.Text = File.ReadAllText(OpenedFile);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Unable to open file: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            var nums = TextArea.Margins[1];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            TextArea.ScrollWidth = 1;
            TextArea.ScrollWidthTracking = true;

            TextArea.EmptyUndoBuffer();
            Changed = false;

            switch (Path.GetExtension(OpenedFile))
            {
                case ".py":
                    Syntax.Python(TextArea);
                    TextArea.UseTabs = true;
                    TabIndentingMenuItem.Checked = true;
                    break;

                case ".json":
                    Syntax.JSON(TextArea);
                    TextArea.UseTabs = true;
                    TabIndentingMenuItem.Checked = true;
                    break;

                default:
                    Syntax.Plain(TextArea);
                    TextArea.UseTabs = false;
                    TabIndentingMenuItem.Checked = false;
                    break;
            }

            UpdateTitle();
        }

       /// <summary>
       /// Reloads the file
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void ReloadFileMenuItem_Click(object sender, EventArgs e)
        {
            if (Changed == true)
            {
                if (MessageBox.Show("The file has been changed, are you sure you want to reload it? You will lose all changes.", "Reload File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    LoadFile();
                }
            }
            else
            {
                LoadFile();
            }
        }

        /// <summary>
        /// Saves the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFileMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(OpenedFile, TextArea.Text, Encoding.UTF8);
                Changed = false;
                UpdateTitle();
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Checks if the file has been changed before closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Changed == true)
            {
                e.Cancel = true;
                var Results = MessageBox.Show("The file has unsaved changes, would you like to save the changes?", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if(Results == DialogResult.Yes)
                {
                    try
                    {
                        File.WriteAllText(OpenedFile, TextArea.Text, Encoding.UTF8);
                        Changed = false;
                        UpdateTitle();
                        e.Cancel = false;
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
                else if(Results == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if(Results == DialogResult.No)
                {
                    e.Cancel = false;
                }
            }
        }

        /// <summary>
        /// Change Syntax Highlighting to Python
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PythonSyntaxMenuItem_Click(object sender, EventArgs e)
        {
            Syntax.Python(TextArea);
            TextArea.UseTabs = true;
            TabIndentingMenuItem.Checked = true;
        }

        /// <summary>
        /// Change Syntax Highlighting to JSON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JSONSyntaxMenuItem_Click(object sender, EventArgs e)
        {
            Syntax.JSON(TextArea);
            TextArea.UseTabs = true;
            TabIndentingMenuItem.Checked = true;
        }

        /// <summary>
        /// Change Syntax Highlighting to Plain Text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlainTextSyntaxMenuItem_Click(object sender, EventArgs e)
        {
            Syntax.Plain(TextArea);
            TextArea.UseTabs = false;
            TabIndentingMenuItem.Checked = false;
        }
    }
}
