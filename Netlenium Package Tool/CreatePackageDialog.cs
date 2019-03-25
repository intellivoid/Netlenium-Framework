using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace NetleniumPackageTool
{

    /// <summary>
    /// Create Package Dialog
    /// </summary>
    public partial class CreatePackageDialog : Form
    {
        /// <summary>
        /// The output directory of the created package
        /// </summary>
        public string OutputDirectory { get; set; }

        /// <summary>
        /// Public Constructor
        /// </summary>
        public CreatePackageDialog()
        {
            InitializeComponent();
            PackageNameTextbox.Text = "Netlenium Package";
            PackageVersionTextbox.Text = "1.0.0.0";
            PackageAuthorTextbox.Text = "Unknown";
            PackageCompanyTextbox.Text = "None";
            LocationTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ProjectDirectoryNameTextBox.Text = "netlenium_package";
        }

        /// <summary>
        /// Package details
        /// </summary>
        public class PackageDetails
        {
            /// <summary>
            /// The name of the package
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// The version of the package
            /// </summary>
            public string Version { get; set; }

            /// <summary>
            /// The author of the package
            /// </summary>
            public string Author { get; set; }

            /// <summary>
            /// The company that distributes this package
            /// </summary>
            public string Company { get; set; }
        }

        /// <summary>
        /// Creates the package
        /// </summary>
        private void CreatePackage()
        {
            var TargetDirectory = $"{LocationTextBox.Text}{Path.DirectorySeparatorChar}{ProjectDirectoryNameTextBox.Text}";

            if (Directory.Exists(TargetDirectory) == true)
            {

                MessageBox.Show(
                    $"The package directory cannot be created because {TargetDirectory} already exists",
                    "Directory Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Error
                );

                return;
            }

            try
            {
                Directory.CreateDirectory(TargetDirectory);
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                   $"The package directory cannot be created due to an error: {exception.Message}",
                   "Directory Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Error
               );

                return;
            }

            var PackageDetailsObj = new PackageDetails()
            {
                Name = PackageNameTextbox.Text,
                Version = PackageVersionTextbox.Text,
                Author = PackageAuthorTextbox.Text,
                Company = PackageCompanyTextbox.Text
            };
            
            File.WriteAllText($"{TargetDirectory}{Path.DirectorySeparatorChar}package.json", JsonConvert.SerializeObject(PackageDetailsObj, Formatting.Indented));
            File.Create($"{TargetDirectory}{Path.DirectorySeparatorChar}main.py");

            OutputDirectory = TargetDirectory;
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// When the Create Package button gets clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatePackageButton_Click(object sender, EventArgs e)
        {
            if(PackageNameTextbox.Text.Length == 0)
            {
                MessageBox.Show("The package name cannot be empty", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(PackageVersionTextbox.Text.Length == 0)
            {
                MessageBox.Show("The package version cannot be empty", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(PackageAuthorTextbox.Text.Length == 0)
            {
                MessageBox.Show("The package author cannot be empty!", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(PackageCompanyTextbox.Text.Length == 0)
            {
                MessageBox.Show("The package company cannot be empty!", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(Directory.Exists(LocationTextBox.Text) == false)
            {
                MessageBox.Show("The output location does not exist", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CreatePackage();
        }

        /// <summary>
        /// Changes the selected location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            var Prompt = new FolderBrowserDialog();
            if(Prompt.ShowDialog() == DialogResult.OK)
            {
                LocationTextBox.Text = Prompt.SelectedPath;
            }
        }
    }
}
