namespace NetleniumPackageTool
{
    partial class CreatePackageDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.PackageDetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.PackageCompanyPanel = new System.Windows.Forms.Panel();
            this.PackageCompanyTextbox = new System.Windows.Forms.TextBox();
            this.PackageCompanyLabel = new System.Windows.Forms.Label();
            this.PackageAuthorPanel = new System.Windows.Forms.Panel();
            this.PackageAuthorTextbox = new System.Windows.Forms.TextBox();
            this.PackageAuthorLabel = new System.Windows.Forms.Label();
            this.PackageVersionPanel = new System.Windows.Forms.Panel();
            this.PackageVersionTextbox = new System.Windows.Forms.TextBox();
            this.PackageVersionLabel = new System.Windows.Forms.Label();
            this.PackageNamePanel = new System.Windows.Forms.Panel();
            this.PackageNameTextbox = new System.Windows.Forms.TextBox();
            this.PackageNameLabel = new System.Windows.Forms.Label();
            this.LocationLabel = new System.Windows.Forms.Label();
            this.LocationTextBox = new System.Windows.Forms.TextBox();
            this.SelectFolderButton = new System.Windows.Forms.Button();
            this.ProjectDirectoryNameTextBox = new System.Windows.Forms.TextBox();
            this.ProjectDirectoryNameLabel = new System.Windows.Forms.Label();
            this.FooterPanel = new System.Windows.Forms.Panel();
            this.CancelButton = new System.Windows.Forms.Button();
            this.CreatePackageButton = new System.Windows.Forms.Button();
            this.PackageDetailsGroupBox.SuspendLayout();
            this.PackageCompanyPanel.SuspendLayout();
            this.PackageAuthorPanel.SuspendLayout();
            this.PackageVersionPanel.SuspendLayout();
            this.PackageNamePanel.SuspendLayout();
            this.FooterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HeaderLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeaderLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.HeaderLabel.Location = new System.Drawing.Point(12, 9);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.Size = new System.Drawing.Size(492, 25);
            this.HeaderLabel.TabIndex = 4;
            this.HeaderLabel.Text = "Create new Netlenium Package\r\n";
            this.HeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PackageDetailsGroupBox
            // 
            this.PackageDetailsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PackageDetailsGroupBox.Controls.Add(this.PackageCompanyPanel);
            this.PackageDetailsGroupBox.Controls.Add(this.PackageAuthorPanel);
            this.PackageDetailsGroupBox.Controls.Add(this.PackageVersionPanel);
            this.PackageDetailsGroupBox.Controls.Add(this.PackageNamePanel);
            this.PackageDetailsGroupBox.Location = new System.Drawing.Point(12, 46);
            this.PackageDetailsGroupBox.Name = "PackageDetailsGroupBox";
            this.PackageDetailsGroupBox.Size = new System.Drawing.Size(492, 151);
            this.PackageDetailsGroupBox.TabIndex = 5;
            this.PackageDetailsGroupBox.TabStop = false;
            this.PackageDetailsGroupBox.Text = "Package Details";
            // 
            // PackageCompanyPanel
            // 
            this.PackageCompanyPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PackageCompanyPanel.Controls.Add(this.PackageCompanyTextbox);
            this.PackageCompanyPanel.Controls.Add(this.PackageCompanyLabel);
            this.PackageCompanyPanel.Location = new System.Drawing.Point(18, 110);
            this.PackageCompanyPanel.Name = "PackageCompanyPanel";
            this.PackageCompanyPanel.Size = new System.Drawing.Size(451, 22);
            this.PackageCompanyPanel.TabIndex = 14;
            // 
            // PackageCompanyTextbox
            // 
            this.PackageCompanyTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PackageCompanyTextbox.Location = new System.Drawing.Point(69, 0);
            this.PackageCompanyTextbox.Name = "PackageCompanyTextbox";
            this.PackageCompanyTextbox.Size = new System.Drawing.Size(382, 22);
            this.PackageCompanyTextbox.TabIndex = 5;
            // 
            // PackageCompanyLabel
            // 
            this.PackageCompanyLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.PackageCompanyLabel.Location = new System.Drawing.Point(0, 0);
            this.PackageCompanyLabel.Name = "PackageCompanyLabel";
            this.PackageCompanyLabel.Size = new System.Drawing.Size(69, 22);
            this.PackageCompanyLabel.TabIndex = 4;
            this.PackageCompanyLabel.Text = "Company:";
            this.PackageCompanyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PackageCompanyLabel.UseCompatibleTextRendering = true;
            // 
            // PackageAuthorPanel
            // 
            this.PackageAuthorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PackageAuthorPanel.Controls.Add(this.PackageAuthorTextbox);
            this.PackageAuthorPanel.Controls.Add(this.PackageAuthorLabel);
            this.PackageAuthorPanel.Location = new System.Drawing.Point(18, 82);
            this.PackageAuthorPanel.Name = "PackageAuthorPanel";
            this.PackageAuthorPanel.Size = new System.Drawing.Size(451, 22);
            this.PackageAuthorPanel.TabIndex = 13;
            // 
            // PackageAuthorTextbox
            // 
            this.PackageAuthorTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PackageAuthorTextbox.Location = new System.Drawing.Point(69, 0);
            this.PackageAuthorTextbox.Name = "PackageAuthorTextbox";
            this.PackageAuthorTextbox.Size = new System.Drawing.Size(382, 22);
            this.PackageAuthorTextbox.TabIndex = 5;
            // 
            // PackageAuthorLabel
            // 
            this.PackageAuthorLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.PackageAuthorLabel.Location = new System.Drawing.Point(0, 0);
            this.PackageAuthorLabel.Name = "PackageAuthorLabel";
            this.PackageAuthorLabel.Size = new System.Drawing.Size(69, 22);
            this.PackageAuthorLabel.TabIndex = 4;
            this.PackageAuthorLabel.Text = "Author:";
            this.PackageAuthorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PackageAuthorLabel.UseCompatibleTextRendering = true;
            // 
            // PackageVersionPanel
            // 
            this.PackageVersionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PackageVersionPanel.Controls.Add(this.PackageVersionTextbox);
            this.PackageVersionPanel.Controls.Add(this.PackageVersionLabel);
            this.PackageVersionPanel.Location = new System.Drawing.Point(18, 54);
            this.PackageVersionPanel.Name = "PackageVersionPanel";
            this.PackageVersionPanel.Size = new System.Drawing.Size(451, 22);
            this.PackageVersionPanel.TabIndex = 12;
            // 
            // PackageVersionTextbox
            // 
            this.PackageVersionTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PackageVersionTextbox.Location = new System.Drawing.Point(69, 0);
            this.PackageVersionTextbox.Name = "PackageVersionTextbox";
            this.PackageVersionTextbox.Size = new System.Drawing.Size(382, 22);
            this.PackageVersionTextbox.TabIndex = 5;
            // 
            // PackageVersionLabel
            // 
            this.PackageVersionLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.PackageVersionLabel.Location = new System.Drawing.Point(0, 0);
            this.PackageVersionLabel.Name = "PackageVersionLabel";
            this.PackageVersionLabel.Size = new System.Drawing.Size(69, 22);
            this.PackageVersionLabel.TabIndex = 4;
            this.PackageVersionLabel.Text = "Version:";
            this.PackageVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PackageVersionLabel.UseCompatibleTextRendering = true;
            // 
            // PackageNamePanel
            // 
            this.PackageNamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PackageNamePanel.Controls.Add(this.PackageNameTextbox);
            this.PackageNamePanel.Controls.Add(this.PackageNameLabel);
            this.PackageNamePanel.Location = new System.Drawing.Point(18, 26);
            this.PackageNamePanel.Name = "PackageNamePanel";
            this.PackageNamePanel.Size = new System.Drawing.Size(451, 22);
            this.PackageNamePanel.TabIndex = 11;
            // 
            // PackageNameTextbox
            // 
            this.PackageNameTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PackageNameTextbox.Location = new System.Drawing.Point(69, 0);
            this.PackageNameTextbox.Name = "PackageNameTextbox";
            this.PackageNameTextbox.Size = new System.Drawing.Size(382, 22);
            this.PackageNameTextbox.TabIndex = 5;
            // 
            // PackageNameLabel
            // 
            this.PackageNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.PackageNameLabel.Location = new System.Drawing.Point(0, 0);
            this.PackageNameLabel.Name = "PackageNameLabel";
            this.PackageNameLabel.Size = new System.Drawing.Size(69, 22);
            this.PackageNameLabel.TabIndex = 4;
            this.PackageNameLabel.Text = "Name:";
            this.PackageNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PackageNameLabel.UseCompatibleTextRendering = true;
            // 
            // LocationLabel
            // 
            this.LocationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LocationLabel.Location = new System.Drawing.Point(21, 218);
            this.LocationLabel.Name = "LocationLabel";
            this.LocationLabel.Size = new System.Drawing.Size(69, 22);
            this.LocationLabel.TabIndex = 6;
            this.LocationLabel.Text = "Location:";
            this.LocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LocationLabel.UseCompatibleTextRendering = true;
            // 
            // LocationTextBox
            // 
            this.LocationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LocationTextBox.Location = new System.Drawing.Point(75, 218);
            this.LocationTextBox.Name = "LocationTextBox";
            this.LocationTextBox.Size = new System.Drawing.Size(318, 22);
            this.LocationTextBox.TabIndex = 7;
            // 
            // SelectFolderButton
            // 
            this.SelectFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectFolderButton.Location = new System.Drawing.Point(399, 218);
            this.SelectFolderButton.Name = "SelectFolderButton";
            this.SelectFolderButton.Size = new System.Drawing.Size(95, 23);
            this.SelectFolderButton.TabIndex = 8;
            this.SelectFolderButton.Text = "Select Folder";
            this.SelectFolderButton.UseVisualStyleBackColor = true;
            this.SelectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            // 
            // ProjectDirectoryNameTextBox
            // 
            this.ProjectDirectoryNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProjectDirectoryNameTextBox.Location = new System.Drawing.Point(149, 252);
            this.ProjectDirectoryNameTextBox.Name = "ProjectDirectoryNameTextBox";
            this.ProjectDirectoryNameTextBox.Size = new System.Drawing.Size(345, 22);
            this.ProjectDirectoryNameTextBox.TabIndex = 10;
            // 
            // ProjectDirectoryNameLabel
            // 
            this.ProjectDirectoryNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ProjectDirectoryNameLabel.Location = new System.Drawing.Point(21, 252);
            this.ProjectDirectoryNameLabel.Name = "ProjectDirectoryNameLabel";
            this.ProjectDirectoryNameLabel.Size = new System.Drawing.Size(142, 22);
            this.ProjectDirectoryNameLabel.TabIndex = 9;
            this.ProjectDirectoryNameLabel.Text = "Project Directory Name:";
            this.ProjectDirectoryNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ProjectDirectoryNameLabel.UseCompatibleTextRendering = true;
            // 
            // FooterPanel
            // 
            this.FooterPanel.BackColor = System.Drawing.SystemColors.Control;
            this.FooterPanel.Controls.Add(this.CancelButton);
            this.FooterPanel.Controls.Add(this.CreatePackageButton);
            this.FooterPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FooterPanel.Location = new System.Drawing.Point(0, 304);
            this.FooterPanel.Name = "FooterPanel";
            this.FooterPanel.Size = new System.Drawing.Size(516, 40);
            this.FooterPanel.TabIndex = 11;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(300, 7);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(84, 26);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // CreatePackageButton
            // 
            this.CreatePackageButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CreatePackageButton.Location = new System.Drawing.Point(390, 7);
            this.CreatePackageButton.Name = "CreatePackageButton";
            this.CreatePackageButton.Size = new System.Drawing.Size(119, 26);
            this.CreatePackageButton.TabIndex = 0;
            this.CreatePackageButton.Text = "Create Package";
            this.CreatePackageButton.UseVisualStyleBackColor = true;
            this.CreatePackageButton.Click += new System.EventHandler(this.CreatePackageButton_Click);
            // 
            // CreatePackageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(516, 344);
            this.Controls.Add(this.FooterPanel);
            this.Controls.Add(this.ProjectDirectoryNameTextBox);
            this.Controls.Add(this.ProjectDirectoryNameLabel);
            this.Controls.Add(this.SelectFolderButton);
            this.Controls.Add(this.LocationTextBox);
            this.Controls.Add(this.LocationLabel);
            this.Controls.Add(this.PackageDetailsGroupBox);
            this.Controls.Add(this.HeaderLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::NetleniumPackageTool.Properties.Resources.netlenium_package_cat;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreatePackageDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create new Netlenium Package";
            this.PackageDetailsGroupBox.ResumeLayout(false);
            this.PackageCompanyPanel.ResumeLayout(false);
            this.PackageCompanyPanel.PerformLayout();
            this.PackageAuthorPanel.ResumeLayout(false);
            this.PackageAuthorPanel.PerformLayout();
            this.PackageVersionPanel.ResumeLayout(false);
            this.PackageVersionPanel.PerformLayout();
            this.PackageNamePanel.ResumeLayout(false);
            this.PackageNamePanel.PerformLayout();
            this.FooterPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label HeaderLabel;
        private System.Windows.Forms.GroupBox PackageDetailsGroupBox;
        private System.Windows.Forms.Panel PackageCompanyPanel;
        private System.Windows.Forms.TextBox PackageCompanyTextbox;
        private System.Windows.Forms.Label PackageCompanyLabel;
        private System.Windows.Forms.Panel PackageAuthorPanel;
        private System.Windows.Forms.TextBox PackageAuthorTextbox;
        private System.Windows.Forms.Label PackageAuthorLabel;
        private System.Windows.Forms.Panel PackageVersionPanel;
        private System.Windows.Forms.TextBox PackageVersionTextbox;
        private System.Windows.Forms.Label PackageVersionLabel;
        private System.Windows.Forms.Panel PackageNamePanel;
        private System.Windows.Forms.TextBox PackageNameTextbox;
        private System.Windows.Forms.Label PackageNameLabel;
        private System.Windows.Forms.Label LocationLabel;
        private System.Windows.Forms.TextBox LocationTextBox;
        private System.Windows.Forms.Button SelectFolderButton;
        private System.Windows.Forms.TextBox ProjectDirectoryNameTextBox;
        private System.Windows.Forms.Label ProjectDirectoryNameLabel;
        private System.Windows.Forms.Panel FooterPanel;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button CreatePackageButton;
    }
}