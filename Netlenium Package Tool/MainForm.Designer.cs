namespace NetleniumPackageTool
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.PackageDetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.EditPackageDetailsButton = new System.Windows.Forms.Button();
            this.PackageCompanyPanel = new System.Windows.Forms.Panel();
            this.PackageCompanyTextbox = new System.Windows.Forms.TextBox();
            this.PackageCompanyLabel = new System.Windows.Forms.Label();
            this.PanelSeperator3 = new System.Windows.Forms.Panel();
            this.PackageAuthorPanel = new System.Windows.Forms.Panel();
            this.PackageAuthorTextbox = new System.Windows.Forms.TextBox();
            this.PackageAuthorLabel = new System.Windows.Forms.Label();
            this.PanelSeperator2 = new System.Windows.Forms.Panel();
            this.PackageVersionPanel = new System.Windows.Forms.Panel();
            this.PackageVersionTextbox = new System.Windows.Forms.TextBox();
            this.PackageVersionLabel = new System.Windows.Forms.Label();
            this.PanelSeperator1 = new System.Windows.Forms.Panel();
            this.PackageNamePanel = new System.Windows.Forms.Panel();
            this.PackageNameTextbox = new System.Windows.Forms.TextBox();
            this.PackageNameLabel = new System.Windows.Forms.Label();
            this.ProjectDirectoryLabel = new System.Windows.Forms.Label();
            this.ProjectDirectoryPanel = new System.Windows.Forms.Panel();
            this.ProjectDirectoryTextbox = new System.Windows.Forms.TextBox();
            this.ProjectFilesImageList = new System.Windows.Forms.ImageList(this.components);
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.ProjectDirectoryTreeview = new System.Windows.Forms.TreeView();
            this.ProjectDirectoryGroupBox = new System.Windows.Forms.GroupBox();
            this.ProjectDirectoryContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DirectoryContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createDirectoryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.FileMenuItem = new System.Windows.Forms.MenuItem();
            this.CreateNewPackageMenuItem = new System.Windows.Forms.MenuItem();
            this.LoadSourceDirectoryMenuItem = new System.Windows.Forms.MenuItem();
            this.MenuItemSeperator1 = new System.Windows.Forms.MenuItem();
            this.ExitMenuItem = new System.Windows.Forms.MenuItem();
            this.EditMenuItem = new System.Windows.Forms.MenuItem();
            this.EditPackageDetailsMenuItem = new System.Windows.Forms.MenuItem();
            this.ViewMenuItem = new System.Windows.Forms.MenuItem();
            this.BuildMenuItem = new System.Windows.Forms.MenuItem();
            this.BuildPackageMenuItem = new System.Windows.Forms.MenuItem();
            this.BuildAndRunPackageMenuItem = new System.Windows.Forms.MenuItem();
            this.ToolsMenuItem = new System.Windows.Forms.MenuItem();
            this.RunPackageMenuItem = new System.Windows.Forms.MenuItem();
            this.HelpMenuItem = new System.Windows.Forms.MenuItem();
            this.AboutNetleniumFrameworkMenuItem = new System.Windows.Forms.MenuItem();
            this.AboutPackageToolMenuItem = new System.Windows.Forms.MenuItem();
            this.PackageDetailsGroupBox.SuspendLayout();
            this.PackageCompanyPanel.SuspendLayout();
            this.PackageAuthorPanel.SuspendLayout();
            this.PackageVersionPanel.SuspendLayout();
            this.PackageNamePanel.SuspendLayout();
            this.ProjectDirectoryPanel.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.ProjectDirectoryGroupBox.SuspendLayout();
            this.ProjectDirectoryContextMenuStrip.SuspendLayout();
            this.DirectoryContextMenuStrip.SuspendLayout();
            this.FileContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // PackageDetailsGroupBox
            // 
            this.PackageDetailsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PackageDetailsGroupBox.Controls.Add(this.EditPackageDetailsButton);
            this.PackageDetailsGroupBox.Controls.Add(this.PackageCompanyPanel);
            this.PackageDetailsGroupBox.Controls.Add(this.PanelSeperator3);
            this.PackageDetailsGroupBox.Controls.Add(this.PackageAuthorPanel);
            this.PackageDetailsGroupBox.Controls.Add(this.PanelSeperator2);
            this.PackageDetailsGroupBox.Controls.Add(this.PackageVersionPanel);
            this.PackageDetailsGroupBox.Controls.Add(this.PanelSeperator1);
            this.PackageDetailsGroupBox.Controls.Add(this.PackageNamePanel);
            this.PackageDetailsGroupBox.Location = new System.Drawing.Point(12, 40);
            this.PackageDetailsGroupBox.Name = "PackageDetailsGroupBox";
            this.PackageDetailsGroupBox.Size = new System.Drawing.Size(254, 165);
            this.PackageDetailsGroupBox.TabIndex = 0;
            this.PackageDetailsGroupBox.TabStop = false;
            this.PackageDetailsGroupBox.Text = "Package Details";
            // 
            // EditPackageDetailsButton
            // 
            this.EditPackageDetailsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EditPackageDetailsButton.Location = new System.Drawing.Point(173, 134);
            this.EditPackageDetailsButton.Name = "EditPackageDetailsButton";
            this.EditPackageDetailsButton.Size = new System.Drawing.Size(75, 23);
            this.EditPackageDetailsButton.TabIndex = 10;
            this.EditPackageDetailsButton.Text = "Edit";
            this.EditPackageDetailsButton.UseVisualStyleBackColor = true;
            // 
            // PackageCompanyPanel
            // 
            this.PackageCompanyPanel.Controls.Add(this.PackageCompanyTextbox);
            this.PackageCompanyPanel.Controls.Add(this.PackageCompanyLabel);
            this.PackageCompanyPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PackageCompanyPanel.Location = new System.Drawing.Point(3, 99);
            this.PackageCompanyPanel.Name = "PackageCompanyPanel";
            this.PackageCompanyPanel.Size = new System.Drawing.Size(248, 22);
            this.PackageCompanyPanel.TabIndex = 9;
            // 
            // PackageCompanyTextbox
            // 
            this.PackageCompanyTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PackageCompanyTextbox.Location = new System.Drawing.Point(69, 0);
            this.PackageCompanyTextbox.Name = "PackageCompanyTextbox";
            this.PackageCompanyTextbox.ReadOnly = true;
            this.PackageCompanyTextbox.Size = new System.Drawing.Size(179, 22);
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
            // PanelSeperator3
            // 
            this.PanelSeperator3.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSeperator3.Location = new System.Drawing.Point(3, 94);
            this.PanelSeperator3.Name = "PanelSeperator3";
            this.PanelSeperator3.Size = new System.Drawing.Size(248, 5);
            this.PanelSeperator3.TabIndex = 8;
            // 
            // PackageAuthorPanel
            // 
            this.PackageAuthorPanel.Controls.Add(this.PackageAuthorTextbox);
            this.PackageAuthorPanel.Controls.Add(this.PackageAuthorLabel);
            this.PackageAuthorPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PackageAuthorPanel.Location = new System.Drawing.Point(3, 72);
            this.PackageAuthorPanel.Name = "PackageAuthorPanel";
            this.PackageAuthorPanel.Size = new System.Drawing.Size(248, 22);
            this.PackageAuthorPanel.TabIndex = 7;
            // 
            // PackageAuthorTextbox
            // 
            this.PackageAuthorTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PackageAuthorTextbox.Location = new System.Drawing.Point(69, 0);
            this.PackageAuthorTextbox.Name = "PackageAuthorTextbox";
            this.PackageAuthorTextbox.ReadOnly = true;
            this.PackageAuthorTextbox.Size = new System.Drawing.Size(179, 22);
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
            // PanelSeperator2
            // 
            this.PanelSeperator2.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSeperator2.Location = new System.Drawing.Point(3, 67);
            this.PanelSeperator2.Name = "PanelSeperator2";
            this.PanelSeperator2.Size = new System.Drawing.Size(248, 5);
            this.PanelSeperator2.TabIndex = 6;
            // 
            // PackageVersionPanel
            // 
            this.PackageVersionPanel.Controls.Add(this.PackageVersionTextbox);
            this.PackageVersionPanel.Controls.Add(this.PackageVersionLabel);
            this.PackageVersionPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PackageVersionPanel.Location = new System.Drawing.Point(3, 45);
            this.PackageVersionPanel.Name = "PackageVersionPanel";
            this.PackageVersionPanel.Size = new System.Drawing.Size(248, 22);
            this.PackageVersionPanel.TabIndex = 5;
            // 
            // PackageVersionTextbox
            // 
            this.PackageVersionTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PackageVersionTextbox.Location = new System.Drawing.Point(69, 0);
            this.PackageVersionTextbox.Name = "PackageVersionTextbox";
            this.PackageVersionTextbox.ReadOnly = true;
            this.PackageVersionTextbox.Size = new System.Drawing.Size(179, 22);
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
            // PanelSeperator1
            // 
            this.PanelSeperator1.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSeperator1.Location = new System.Drawing.Point(3, 40);
            this.PanelSeperator1.Name = "PanelSeperator1";
            this.PanelSeperator1.Size = new System.Drawing.Size(248, 5);
            this.PanelSeperator1.TabIndex = 4;
            // 
            // PackageNamePanel
            // 
            this.PackageNamePanel.Controls.Add(this.PackageNameTextbox);
            this.PackageNamePanel.Controls.Add(this.PackageNameLabel);
            this.PackageNamePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PackageNamePanel.Location = new System.Drawing.Point(3, 18);
            this.PackageNamePanel.Name = "PackageNamePanel";
            this.PackageNamePanel.Size = new System.Drawing.Size(248, 22);
            this.PackageNamePanel.TabIndex = 1;
            // 
            // PackageNameTextbox
            // 
            this.PackageNameTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PackageNameTextbox.Location = new System.Drawing.Point(69, 0);
            this.PackageNameTextbox.Name = "PackageNameTextbox";
            this.PackageNameTextbox.ReadOnly = true;
            this.PackageNameTextbox.Size = new System.Drawing.Size(179, 22);
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
            // ProjectDirectoryLabel
            // 
            this.ProjectDirectoryLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ProjectDirectoryLabel.Location = new System.Drawing.Point(0, 0);
            this.ProjectDirectoryLabel.Name = "ProjectDirectoryLabel";
            this.ProjectDirectoryLabel.Size = new System.Drawing.Size(95, 22);
            this.ProjectDirectoryLabel.TabIndex = 1;
            this.ProjectDirectoryLabel.Text = "Project Directory:";
            this.ProjectDirectoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ProjectDirectoryLabel.UseCompatibleTextRendering = true;
            // 
            // ProjectDirectoryPanel
            // 
            this.ProjectDirectoryPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProjectDirectoryPanel.Controls.Add(this.ProjectDirectoryTextbox);
            this.ProjectDirectoryPanel.Controls.Add(this.ProjectDirectoryLabel);
            this.ProjectDirectoryPanel.Location = new System.Drawing.Point(12, 12);
            this.ProjectDirectoryPanel.Name = "ProjectDirectoryPanel";
            this.ProjectDirectoryPanel.Size = new System.Drawing.Size(560, 22);
            this.ProjectDirectoryPanel.TabIndex = 3;
            // 
            // ProjectDirectoryTextbox
            // 
            this.ProjectDirectoryTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectDirectoryTextbox.Location = new System.Drawing.Point(95, 0);
            this.ProjectDirectoryTextbox.Name = "ProjectDirectoryTextbox";
            this.ProjectDirectoryTextbox.ReadOnly = true;
            this.ProjectDirectoryTextbox.Size = new System.Drawing.Size(465, 22);
            this.ProjectDirectoryTextbox.TabIndex = 3;
            // 
            // ProjectFilesImageList
            // 
            this.ProjectFilesImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ProjectFilesImageList.ImageStream")));
            this.ProjectFilesImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ProjectFilesImageList.Images.SetKeyName(0, "folder_brick.png");
            this.ProjectFilesImageList.Images.SetKeyName(1, "folder.png");
            this.ProjectFilesImageList.Images.SetKeyName(2, "page_white.png");
            this.ProjectFilesImageList.Images.SetKeyName(3, "page_white_code.png");
            this.ProjectFilesImageList.Images.SetKeyName(4, "page_white_code_red.png");
            this.ProjectFilesImageList.Images.SetKeyName(5, "page_white_gear.png");
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabel,
            this.ProgressBar});
            this.StatusStrip.Location = new System.Drawing.Point(0, 218);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(584, 22);
            this.StatusStrip.TabIndex = 5;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // ToolStripStatusLabel
            // 
            this.ToolStripStatusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.ToolStripStatusLabel.Name = "ToolStripStatusLabel";
            this.ToolStripStatusLabel.Size = new System.Drawing.Size(42, 17);
            this.ToolStripStatusLabel.Text = "Ready!";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(100, 16);
            this.ProgressBar.Visible = false;
            // 
            // ProjectDirectoryTreeview
            // 
            this.ProjectDirectoryTreeview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProjectDirectoryTreeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectDirectoryTreeview.ImageIndex = 0;
            this.ProjectDirectoryTreeview.ImageList = this.ProjectFilesImageList;
            this.ProjectDirectoryTreeview.Location = new System.Drawing.Point(3, 18);
            this.ProjectDirectoryTreeview.Name = "ProjectDirectoryTreeview";
            this.ProjectDirectoryTreeview.SelectedImageIndex = 0;
            this.ProjectDirectoryTreeview.Size = new System.Drawing.Size(294, 144);
            this.ProjectDirectoryTreeview.TabIndex = 7;
            this.ProjectDirectoryTreeview.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ProjectDirectoryTreeview_AfterSelect);
            this.ProjectDirectoryTreeview.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ProjectDirectoryTreeview_NodeMouseClick);
            this.ProjectDirectoryTreeview.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ProjectDirectoryTreeview_NodeMouseDoubleClick);
            // 
            // ProjectDirectoryGroupBox
            // 
            this.ProjectDirectoryGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProjectDirectoryGroupBox.Controls.Add(this.ProjectDirectoryTreeview);
            this.ProjectDirectoryGroupBox.Location = new System.Drawing.Point(272, 40);
            this.ProjectDirectoryGroupBox.Name = "ProjectDirectoryGroupBox";
            this.ProjectDirectoryGroupBox.Size = new System.Drawing.Size(300, 165);
            this.ProjectDirectoryGroupBox.TabIndex = 11;
            this.ProjectDirectoryGroupBox.TabStop = false;
            this.ProjectDirectoryGroupBox.Text = "Project Files";
            // 
            // ProjectDirectoryContextMenuStrip
            // 
            this.ProjectDirectoryContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createDirectoryToolStripMenuItem,
            this.newFileToolStripMenuItem});
            this.ProjectDirectoryContextMenuStrip.Name = "ProjectDirectoryContextMenuStrip";
            this.ProjectDirectoryContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ProjectDirectoryContextMenuStrip.Size = new System.Drawing.Size(160, 48);
            // 
            // createDirectoryToolStripMenuItem
            // 
            this.createDirectoryToolStripMenuItem.Name = "createDirectoryToolStripMenuItem";
            this.createDirectoryToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.createDirectoryToolStripMenuItem.Text = "Create Directory";
            this.createDirectoryToolStripMenuItem.Click += new System.EventHandler(this.createDirectoryToolStripMenuItem_Click);
            // 
            // newFileToolStripMenuItem
            // 
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.newFileToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.newFileToolStripMenuItem.Text = "New File";
            this.newFileToolStripMenuItem.Click += new System.EventHandler(this.newFileToolStripMenuItem_Click);
            // 
            // DirectoryContextMenuStrip
            // 
            this.DirectoryContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createDirectoryToolStripMenuItem1,
            this.newFileToolStripMenuItem1,
            this.renameToolStripMenuItem1,
            this.deleteToolStripMenuItem});
            this.DirectoryContextMenuStrip.Name = "DirectoryContextMenuStrip";
            this.DirectoryContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.DirectoryContextMenuStrip.Size = new System.Drawing.Size(160, 92);
            // 
            // createDirectoryToolStripMenuItem1
            // 
            this.createDirectoryToolStripMenuItem1.Name = "createDirectoryToolStripMenuItem1";
            this.createDirectoryToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.createDirectoryToolStripMenuItem1.Text = "Create Directory";
            this.createDirectoryToolStripMenuItem1.Click += new System.EventHandler(this.createDirectoryToolStripMenuItem1_Click);
            // 
            // newFileToolStripMenuItem1
            // 
            this.newFileToolStripMenuItem1.Name = "newFileToolStripMenuItem1";
            this.newFileToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.newFileToolStripMenuItem1.Text = "New File";
            this.newFileToolStripMenuItem1.Click += new System.EventHandler(this.newFileToolStripMenuItem1_Click);
            // 
            // renameToolStripMenuItem1
            // 
            this.renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
            this.renameToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.renameToolStripMenuItem1.Text = "Rename";
            this.renameToolStripMenuItem1.Click += new System.EventHandler(this.renameToolStripMenuItem1_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // FileContextMenuStrip
            // 
            this.FileContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editFileToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem1});
            this.FileContextMenuStrip.Name = "FileContextMenuStrip";
            this.FileContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.FileContextMenuStrip.Size = new System.Drawing.Size(118, 70);
            // 
            // editFileToolStripMenuItem
            // 
            this.editFileToolStripMenuItem.Name = "editFileToolStripMenuItem";
            this.editFileToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.editFileToolStripMenuItem.Text = "Edit File";
            this.editFileToolStripMenuItem.Click += new System.EventHandler(this.editFileToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.FileMenuItem,
            this.EditMenuItem,
            this.ViewMenuItem,
            this.BuildMenuItem,
            this.ToolsMenuItem,
            this.HelpMenuItem});
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.Index = 0;
            this.FileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.CreateNewPackageMenuItem,
            this.LoadSourceDirectoryMenuItem,
            this.MenuItemSeperator1,
            this.ExitMenuItem});
            this.FileMenuItem.Text = "&File";
            // 
            // CreateNewPackageMenuItem
            // 
            this.CreateNewPackageMenuItem.Index = 0;
            this.CreateNewPackageMenuItem.Text = "&Create New Package";
            // 
            // LoadSourceDirectoryMenuItem
            // 
            this.LoadSourceDirectoryMenuItem.Index = 1;
            this.LoadSourceDirectoryMenuItem.Text = "&Load Source Directory";
            // 
            // MenuItemSeperator1
            // 
            this.MenuItemSeperator1.Index = 2;
            this.MenuItemSeperator1.Text = "-";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Index = 3;
            this.ExitMenuItem.Text = "&Exit";
            // 
            // EditMenuItem
            // 
            this.EditMenuItem.Index = 1;
            this.EditMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.EditPackageDetailsMenuItem});
            this.EditMenuItem.Text = "&Edit";
            // 
            // EditPackageDetailsMenuItem
            // 
            this.EditPackageDetailsMenuItem.Index = 0;
            this.EditPackageDetailsMenuItem.Text = "&Edit Package Details";
            // 
            // ViewMenuItem
            // 
            this.ViewMenuItem.Index = 2;
            this.ViewMenuItem.Text = "&View";
            // 
            // BuildMenuItem
            // 
            this.BuildMenuItem.Index = 3;
            this.BuildMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.BuildPackageMenuItem,
            this.BuildAndRunPackageMenuItem});
            this.BuildMenuItem.Text = "&Build";
            // 
            // BuildPackageMenuItem
            // 
            this.BuildPackageMenuItem.Index = 0;
            this.BuildPackageMenuItem.Text = "&Build Package";
            // 
            // BuildAndRunPackageMenuItem
            // 
            this.BuildAndRunPackageMenuItem.Index = 1;
            this.BuildAndRunPackageMenuItem.Text = "&Build and Run Package";
            // 
            // ToolsMenuItem
            // 
            this.ToolsMenuItem.Index = 4;
            this.ToolsMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.RunPackageMenuItem});
            this.ToolsMenuItem.Text = "&Tools";
            // 
            // RunPackageMenuItem
            // 
            this.RunPackageMenuItem.Index = 0;
            this.RunPackageMenuItem.Text = "&Run Package";
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.Index = 5;
            this.HelpMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.AboutNetleniumFrameworkMenuItem,
            this.AboutPackageToolMenuItem});
            this.HelpMenuItem.Text = "&Help";
            // 
            // AboutNetleniumFrameworkMenuItem
            // 
            this.AboutNetleniumFrameworkMenuItem.Index = 0;
            this.AboutNetleniumFrameworkMenuItem.Text = "&About Netlenium Framework";
            // 
            // AboutPackageToolMenuItem
            // 
            this.AboutPackageToolMenuItem.Index = 1;
            this.AboutPackageToolMenuItem.Text = "&About Package Tool";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(584, 240);
            this.Controls.Add(this.ProjectDirectoryGroupBox);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.ProjectDirectoryPanel);
            this.Controls.Add(this.PackageDetailsGroupBox);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::NetleniumPackageTool.Properties.Resources.netlenium_wizard;
            this.MaximizeBox = false;
            this.Menu = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "Netlenium Package Tool";
            this.PackageDetailsGroupBox.ResumeLayout(false);
            this.PackageCompanyPanel.ResumeLayout(false);
            this.PackageCompanyPanel.PerformLayout();
            this.PackageAuthorPanel.ResumeLayout(false);
            this.PackageAuthorPanel.PerformLayout();
            this.PackageVersionPanel.ResumeLayout(false);
            this.PackageVersionPanel.PerformLayout();
            this.PackageNamePanel.ResumeLayout(false);
            this.PackageNamePanel.PerformLayout();
            this.ProjectDirectoryPanel.ResumeLayout(false);
            this.ProjectDirectoryPanel.PerformLayout();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ProjectDirectoryGroupBox.ResumeLayout(false);
            this.ProjectDirectoryContextMenuStrip.ResumeLayout(false);
            this.DirectoryContextMenuStrip.ResumeLayout(false);
            this.FileContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox PackageDetailsGroupBox;
        private System.Windows.Forms.Button EditPackageDetailsButton;
        private System.Windows.Forms.Panel PackageCompanyPanel;
        private System.Windows.Forms.TextBox PackageCompanyTextbox;
        private System.Windows.Forms.Label PackageCompanyLabel;
        private System.Windows.Forms.Panel PanelSeperator3;
        private System.Windows.Forms.Panel PackageAuthorPanel;
        private System.Windows.Forms.TextBox PackageAuthorTextbox;
        private System.Windows.Forms.Label PackageAuthorLabel;
        private System.Windows.Forms.Panel PanelSeperator2;
        private System.Windows.Forms.Panel PackageVersionPanel;
        private System.Windows.Forms.TextBox PackageVersionTextbox;
        private System.Windows.Forms.Label PackageVersionLabel;
        private System.Windows.Forms.Panel PanelSeperator1;
        private System.Windows.Forms.Panel PackageNamePanel;
        private System.Windows.Forms.TextBox PackageNameTextbox;
        private System.Windows.Forms.Label PackageNameLabel;
        private System.Windows.Forms.Label ProjectDirectoryLabel;
        private System.Windows.Forms.Panel ProjectDirectoryPanel;
        private System.Windows.Forms.TextBox ProjectDirectoryTextbox;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel;
        private System.Windows.Forms.ImageList ProjectFilesImageList;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.TreeView ProjectDirectoryTreeview;
        private System.Windows.Forms.GroupBox ProjectDirectoryGroupBox;
        private System.Windows.Forms.ContextMenuStrip ProjectDirectoryContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem createDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip DirectoryContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem createDirectoryToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip FileContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.MainMenu MainMenu;
        private System.Windows.Forms.MenuItem FileMenuItem;
        private System.Windows.Forms.MenuItem CreateNewPackageMenuItem;
        private System.Windows.Forms.MenuItem LoadSourceDirectoryMenuItem;
        private System.Windows.Forms.MenuItem MenuItemSeperator1;
        private System.Windows.Forms.MenuItem ExitMenuItem;
        private System.Windows.Forms.MenuItem EditMenuItem;
        private System.Windows.Forms.MenuItem EditPackageDetailsMenuItem;
        private System.Windows.Forms.MenuItem ViewMenuItem;
        private System.Windows.Forms.MenuItem BuildMenuItem;
        private System.Windows.Forms.MenuItem BuildPackageMenuItem;
        private System.Windows.Forms.MenuItem BuildAndRunPackageMenuItem;
        private System.Windows.Forms.MenuItem ToolsMenuItem;
        private System.Windows.Forms.MenuItem RunPackageMenuItem;
        private System.Windows.Forms.MenuItem HelpMenuItem;
        private System.Windows.Forms.MenuItem AboutNetleniumFrameworkMenuItem;
        private System.Windows.Forms.MenuItem AboutPackageToolMenuItem;
    }
}