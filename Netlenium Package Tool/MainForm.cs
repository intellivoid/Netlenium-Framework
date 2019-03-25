using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace NetleniumPackageTool
{
    /// <summary>
    /// Main Form of the Netlenium Package Tool
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Indicates if the package is currently loaded
        /// </summary>
        private bool PackageLoaded;

        /// <summary>
        /// The location of the loaded package
        /// </summary>
        private string PackageLocation;

        /// <summary>
        /// The current selected node
        /// </summary>
        private TreeNode SelectedNode;

        /// <summary>
        /// The JSON data for the package information
        /// </summary>
        private string PackageJSON;

        /// <summary>
        /// Public Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the current Assembly Directory
        /// </summary>
        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        /// <summary>
        /// Loads an existing project into memory
        /// </summary>
        /// <param name="packageDirectoryLocation"></param>
        private void LoadPackage(string packageDirectoryLocation)
        {
            if(File.Exists($"{packageDirectoryLocation}{Path.DirectorySeparatorChar}package.json") == false)
            {
                MessageBox.Show("This package source directory is invalid because it's missing package.json", "Invalid Package Directory Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PackageLoaded = true;
            PackageLocation = packageDirectoryLocation;
            ProjectDirectoryTextbox.Text = packageDirectoryLocation;

            PackageDetailsGroupBox.Enabled = true;
            ProjectDirectoryGroupBox.Enabled = true;
            NoItemsLabel.Visible = false;
            RefreshTree();
            RefreshPackageInformation();
        }

        /// <summary>
        /// Refreshes the package information
        /// </summary>
        public void RefreshPackageInformation()
        {
            try
            {
                PackageJSON = File.ReadAllText($"{PackageLocation}{Path.DirectorySeparatorChar}package.json");
            }
            catch(Exception exception)
            {
                MessageBox.Show($"There was an error while trying to read package.json{Environment.NewLine}{Environment.NewLine}{exception.Message}", "package.json Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            JObject ParsedData = null;

            try
            {
                ParsedData = JObject.Parse(PackageJSON);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"There was an error while trying to read JSON data from package.json{Environment.NewLine}{Environment.NewLine}{exception.Message}", "JSON Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            try
            {
                PackageNameTextbox.Text = (string)ParsedData["name"];
            }
            catch (Exception exception)
            {
                MessageBox.Show($"There was an error while trying to read 'name' from package.json{Environment.NewLine}{Environment.NewLine}{exception.Message}", "JSON Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                PackageVersionTextbox.Text = (string)ParsedData["version"];
            }
            catch (Exception exception)
            {
                MessageBox.Show($"There was an error while trying to read 'version' from package.json{Environment.NewLine}{Environment.NewLine}{exception.Message}", "JSON Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                PackageAuthorTextbox.Text = (string)ParsedData["author"];
            }
            catch (Exception exception)
            {
                MessageBox.Show($"There was an error while trying to read 'author' from package.json{Environment.NewLine}{Environment.NewLine}{exception.Message}", "JSON Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                PackageCompanyTextbox.Text = (string)ParsedData["company"];
            }
            catch (Exception exception)
            {
                MessageBox.Show($"There was an error while trying to read 'company' from package.json{Environment.NewLine}{Environment.NewLine}{exception.Message}", "JSON Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// Loads directory contents
        /// </summary>
        /// <param name="Dir"></param>
        public void RefreshTree()
        {
            ProjectDirectoryTreeview.Nodes.Clear();
            DirectoryInfo di = new DirectoryInfo(PackageLocation);
            //Setting ProgressBar Maximum Value  
            TreeNode tds = ProjectDirectoryTreeview.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.ContextMenuStrip = ProjectDirectoryContextMenuStrip;
            tds.ImageIndex = 0;
            tds.SelectedImageIndex = tds.ImageIndex;
            LoadFiles(ProjectDirectoryTextbox.Text, tds);
            LoadSubDirectories(ProjectDirectoryTextbox.Text, tds);
            ProjectDirectoryTreeview.ExpandAll();
        }

        /// <summary>
        /// Load all sub directories to the tree view
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="td"></param>
        private void LoadSubDirectories(string dir, TreeNode td)
        {
            // Get all subdirectories  
            string[] subdirectoryEntries = Directory.GetDirectories(dir);
            // Loop through them to see if they have any other subdirectories  
            foreach (string subdirectory in subdirectoryEntries)
            {
                DirectoryInfo di = new DirectoryInfo(subdirectory);
                TreeNode tds = td.Nodes.Add(di.Name);
                tds.Tag = di.FullName;
                tds.ContextMenuStrip = DirectoryContextMenuStrip;
                tds.ImageIndex = 1;
                tds.SelectedImageIndex = tds.ImageIndex;
                LoadFiles(subdirectory, tds);
                LoadSubDirectories(subdirectory, tds);
            }
        }

        /// <summary>
        /// Load all files in the directory to the treeview
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="td"></param>
        private void LoadFiles(string dir, TreeNode td)
        {
            string[] Files = Directory.GetFiles(dir, "*.*");

            // Loop through them to see files  
            foreach (string file in Files)
            {
                FileInfo fi = new FileInfo(file);
                TreeNode tds = td.Nodes.Add(fi.Name);
                tds.Tag = fi.FullName;
                tds.ContextMenuStrip = FileContextMenuStrip;
                switch(Path.GetExtension(fi.FullName))
                {
                    case ".py":
                        tds.ImageIndex = 3;
                        tds.SelectedImageIndex = tds.ImageIndex;
                        break;

                    case ".json":
                        tds.ImageIndex = 4;
                        tds.SelectedImageIndex = tds.ImageIndex;
                        break;

                    default:
                        tds.ImageIndex = 2;
                        tds.SelectedImageIndex = tds.ImageIndex;
                        break;
                }

                if(fi.Name.ToLower() == "package.json")
                {
                    tds.ImageIndex = 5;
                    tds.SelectedImageIndex = tds.ImageIndex;
                }
                

            }
        }
       
        /// <summary>
        /// WHen double clicking on a file, open the editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectDirectoryTreeview_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            FileAttributes attr = File.GetAttributes(SelectedNode.Tag.ToString());
            if((attr & FileAttributes.Directory) != FileAttributes.Directory)
            {
                var Editor = new FileEditor(SelectedNode.Tag.ToString());
            }
        }

        /// <summary>
        /// Prompts the user to create a directory
        /// </summary>
        /// <param name="targetDirectory"></param>
        private void PromptCreateDirectory(string targetDirectory)
        {
            var Prompt = new NewDirectoryDialog();
            if (Prompt.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Directory.CreateDirectory($"{targetDirectory}{Path.DirectorySeparatorChar}{Prompt.DirectoryName}");
                    RefreshTree();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Directory Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Prompts the user to rename the directory
        /// </summary>
        /// <param name="targetDirectory"></param>
        private void PromptRenameDriectory(string targetDirectory)
        {
            var Prompt = new RenameDirectoryDialog(Path.GetFileName(targetDirectory));
            if (Prompt.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(targetDirectory);
                    Directory.Move(targetDirectory, $"{di.Parent.FullName}{Path.DirectorySeparatorChar}{Prompt.DirectoryName}");
                    RefreshTree();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Directory Rename Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Prompts the user to rename the file
        /// </summary>
        /// <param name="targetFile"></param>
        private void PromptRenameFile(string targetFile)
        {
            var Prompt = new RenameFileDialog(Path.GetFileName(targetFile));
            if (Prompt.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileInfo fi = new FileInfo(targetFile);
                    File.Move(targetFile, $"{fi.Directory.FullName}{Path.DirectorySeparatorChar}{Prompt.FileName}");
                    RefreshTree();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Directory Rename Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Prompts the user to create a new file
        /// </summary>
        /// <param name="targetDirectory"></param>
        private void PromptCreateFile(string targetDirectory)
        {
            var Prompt = new NewFileDialog();
            if (Prompt.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists($"{targetDirectory}{Path.DirectorySeparatorChar}{Prompt.FileName}") == true)
                {
                    MessageBox.Show("The file already exists", "Cannot Create File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    try
                    {
                        File.Create($"{targetDirectory}{Path.DirectorySeparatorChar}{Prompt.FileName}");
                        RefreshTree();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, "Directory Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Prompts the user to confirm the action about deleting a Directory
        /// </summary>
        /// <param name="targetDirectory"></param>
        private void PromptDeleteDirectory(string targetDirectory)
        {
            var Prompt = MessageBox.Show($"Are you sure you want to delete the directory {Path.GetFileName(targetDirectory)}?", "Delete Directory", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(Prompt == DialogResult.Yes)
            {
                try
                {
                    Directory.Delete(targetDirectory, true);
                    RefreshTree();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Directory Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Prompts the user to confrim the action about deleting a File
        /// </summary>
        /// <param name="targetFile"></param>
        private void PromptDeleteFile(string targetFile)
        {
            var Prompt = MessageBox.Show($"Are you sure you want to delete the file {Path.GetFileName(targetFile)}?", "Delete File", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Prompt == DialogResult.Yes)
            {
                try
                {
                    File.Delete(targetFile);
                    RefreshTree();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Directory Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// When the user creates a directory on Project Root directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PromptCreateDirectory(SelectedNode.Tag.ToString());
        }

        /// <summary>
        /// When the user creates a file on the Project Root directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PromptCreateFile(SelectedNode.Tag.ToString());
        }

        /// <summary>
        /// When the user creates a directory in a directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createDirectoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PromptCreateDirectory(SelectedNode.Tag.ToString());
        }

        /// <summary>
        /// When the user creates a file in a directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PromptCreateFile(SelectedNode.Tag.ToString());
        }

        /// <summary>
        /// Rename the selected directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PromptRenameDriectory(SelectedNode.Tag.ToString());
        }

        /// <summary>
        /// Sets the selected node after it has been selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectDirectoryTreeview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedNode = ProjectDirectoryTreeview.SelectedNode;
        }

        /// <summary>
        /// When right clicking directly on the node, make sure it's selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectDirectoryTreeview_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right) ProjectDirectoryTreeview.SelectedNode = e.Node;
        }

        /// <summary>
        /// Delete the selected directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PromptDeleteDirectory(SelectedNode.Tag.ToString());
        }

        /// <summary>
        /// Deletes the selected file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PromptDeleteFile(SelectedNode.Tag.ToString());
        }

        /// <summary>
        /// Renames the selected file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PromptRenameFile(SelectedNode.Tag.ToString());
        }

        /// <summary>
        /// Edits the selected file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileAttributes attr = File.GetAttributes(SelectedNode.Tag.ToString());
            if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
            {
                var Editor = new FileEditor(SelectedNode.Tag.ToString());
            }
        }

        /// <summary>
        /// Shows the Create New Package dialog and loads the created package afer it has been created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewPackageMenuItem_Click(object sender, EventArgs e)
        {
            var Dialog = new CreatePackageDialog();
            if(Dialog.ShowDialog() == DialogResult.OK)
            {
                LoadPackage(Dialog.OutputDirectory);
            }
        }

        /// <summary>
        /// Brings up the Folder Browser dialog and loads the source directory for a Netlenium Package
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadSourceDirectoryMenuItem_Click(object sender, EventArgs e)
        {
            var Prompt = new FolderBrowserDialog()
            {
                Description = "Select a source directory for a Netlenium Package"
            };

            if(Prompt.ShowDialog() == DialogResult.OK)
            {
                LoadPackage(Prompt.SelectedPath);
            }
        }

        /// <summary>
        /// Exits the software
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Builds the package
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuildPackageMenuItem_Click(object sender, EventArgs e)
        {
            if(File.Exists($"{AssemblyDirectory}{Path.DirectorySeparatorChar}npbuild.exe") == false)
            {
                MessageBox.Show($"The package cannot be built because {AssemblyDirectory}{Path.DirectorySeparatorChar}npbuild.exe is missing", "Missing Netlenium Package Builder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var PackageBuildProcess = new Process();
            PackageBuildProcess.StartInfo.FileName = $"{AssemblyDirectory}{Path.DirectorySeparatorChar}npbuild.exe";
            PackageBuildProcess.StartInfo.Arguments = $@"--source ""{PackageLocation}"" --prompt false";
            PackageBuildProcess.StartInfo.CreateNoWindow = false;
            PackageBuildProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;

            try
            {
                PackageBuildProcess.Start();

                while(PackageBuildProcess.HasExited == false)
                {
                    Application.DoEvents();
                }

                switch (PackageBuildProcess.ExitCode)
                {
                    // TODO: Move these strings as resources
                    case 0:
                        MessageBox.Show("The package was built successfully without any errors", "Package Builder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case 1:
                        MessageBox.Show("The help menu was shown instead of the package being built, please make sure the Package Tool is up to date", "Package Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;

                    case 2:
                        MessageBox.Show("Missing paramerter \"source\", please make sure the Package Tool is up to date", "Package Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;

                    case 3:
                        MessageBox.Show("Cannot read package.json, verify that the file exists and that there is no syntax errors", "Package Builder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4:
                        MessageBox.Show("There was an unknown error while trying to build the .np file", "Package Builder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 5:
                        MessageBox.Show("There was an error while trying to delete the old .np file, make sure it isn't being used by another process", "Package Builder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 6:
                        MessageBox.Show("The file package.json is missing \"name\"", "Package Builder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 7:
                        MessageBox.Show("The file package.json is missing \"version\"", "Package Builder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 8:
                        MessageBox.Show("The source directory does not exist, try reloading the Package Tool", "Package Builder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 9:
                        MessageBox.Show("The file package.json was not found", "Package Builder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 10:
                        MessageBox.Show("The file main.py was not found", "Package Builder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 11:
                        MessageBox.Show("There was an error while trying to parse the command-line arguments, please make sure the Package Tool is up to date", "Package Builder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("The package builder returned an unknown error, please make sure the Package Tool is up to date", "Package Builder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message, "Package Build Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
