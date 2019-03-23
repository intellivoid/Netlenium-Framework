using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetleniumPackageTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads directory contents
        /// </summary>
        /// <param name="Dir"></param>
        public void RefreshTree()
        {
            ProjectDirectoryTreeview.Nodes.Clear();
            DirectoryInfo di = new DirectoryInfo(ProjectDirectoryTextbox.Text);
            //Setting ProgressBar Maximum Value  
            TreeNode tds = ProjectDirectoryTreeview.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.ImageIndex = 0;
            tds.SelectedImageIndex = tds.ImageIndex;
            LoadFiles(ProjectDirectoryTextbox.Text, tds);
            LoadSubDirectories(ProjectDirectoryTextbox.Text, tds);
        }

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
                tds.ImageIndex = 1;
                tds.SelectedImageIndex = tds.ImageIndex;
                LoadFiles(subdirectory, tds);
                LoadSubDirectories(subdirectory, tds);

            }
        }

        private void LoadFiles(string dir, TreeNode td)
        {
            string[] Files = Directory.GetFiles(dir, "*.*");

            // Loop through them to see files  
            foreach (string file in Files)
            {
                FileInfo fi = new FileInfo(file);
                TreeNode tds = td.Nodes.Add(fi.Name);
                tds.Tag = fi.FullName;
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
    }
}
