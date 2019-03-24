namespace NetleniumPackageTool
{
    partial class FileEditor
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
            this.MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.FileMenuItem = new System.Windows.Forms.MenuItem();
            this.LoadFileContentsMenuItem = new System.Windows.Forms.MenuItem();
            this.SaveFileMenuItem = new System.Windows.Forms.MenuItem();
            this.ReloadFileMenuItem = new System.Windows.Forms.MenuItem();
            this.MenuItemSeperator2 = new System.Windows.Forms.MenuItem();
            this.ExitMenuItem = new System.Windows.Forms.MenuItem();
            this.EditMenuItem = new System.Windows.Forms.MenuItem();
            this.UndoMenuItem = new System.Windows.Forms.MenuItem();
            this.RedoMenuItem = new System.Windows.Forms.MenuItem();
            this.MenuItemSeperator1 = new System.Windows.Forms.MenuItem();
            this.TabIndentingMenuItem = new System.Windows.Forms.MenuItem();
            this.MenuItemSeperator3 = new System.Windows.Forms.MenuItem();
            this.SyntaxHighlightingMenuItem = new System.Windows.Forms.MenuItem();
            this.PlainTextSyntaxMenuItem = new System.Windows.Forms.MenuItem();
            this.PythonSyntaxMenuItem = new System.Windows.Forms.MenuItem();
            this.JSONSyntaxMenuItem = new System.Windows.Forms.MenuItem();
            this.ViewMenuItem = new System.Windows.Forms.MenuItem();
            this.ZoomInMenuItem = new System.Windows.Forms.MenuItem();
            this.ZoomOutMenuItem = new System.Windows.Forms.MenuItem();
            this.DefaultZoomMenuItem = new System.Windows.Forms.MenuItem();
            this.HelpMenuItem = new System.Windows.Forms.MenuItem();
            this.AboutNetleniumMenuItem = new System.Windows.Forms.MenuItem();
            this.AboutPackageToolMenuItem = new System.Windows.Forms.MenuItem();
            this.TextArea = new ScintillaNET.Scintilla();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.FileMenuItem,
            this.EditMenuItem,
            this.ViewMenuItem,
            this.HelpMenuItem});
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.Index = 0;
            this.FileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.LoadFileContentsMenuItem,
            this.SaveFileMenuItem,
            this.ReloadFileMenuItem,
            this.MenuItemSeperator2,
            this.ExitMenuItem});
            this.FileMenuItem.Text = "&File";
            // 
            // LoadFileContentsMenuItem
            // 
            this.LoadFileContentsMenuItem.Index = 0;
            this.LoadFileContentsMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.LoadFileContentsMenuItem.Text = "&Load File Contents";
            // 
            // SaveFileMenuItem
            // 
            this.SaveFileMenuItem.Index = 1;
            this.SaveFileMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.SaveFileMenuItem.Text = "&Save File";
            this.SaveFileMenuItem.Click += new System.EventHandler(this.SaveFileMenuItem_Click);
            // 
            // ReloadFileMenuItem
            // 
            this.ReloadFileMenuItem.Index = 2;
            this.ReloadFileMenuItem.Text = "Reload File";
            this.ReloadFileMenuItem.Click += new System.EventHandler(this.ReloadFileMenuItem_Click);
            // 
            // MenuItemSeperator2
            // 
            this.MenuItemSeperator2.Index = 3;
            this.MenuItemSeperator2.Text = "-";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Index = 4;
            this.ExitMenuItem.Shortcut = System.Windows.Forms.Shortcut.AltF4;
            this.ExitMenuItem.Text = "&Exit";
            // 
            // EditMenuItem
            // 
            this.EditMenuItem.Index = 1;
            this.EditMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.UndoMenuItem,
            this.RedoMenuItem,
            this.MenuItemSeperator1,
            this.TabIndentingMenuItem,
            this.MenuItemSeperator3,
            this.SyntaxHighlightingMenuItem});
            this.EditMenuItem.Text = "&Edit";
            // 
            // UndoMenuItem
            // 
            this.UndoMenuItem.Index = 0;
            this.UndoMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.UndoMenuItem.Text = "&Undo";
            this.UndoMenuItem.Click += new System.EventHandler(this.UndoMenuItem_Click);
            // 
            // RedoMenuItem
            // 
            this.RedoMenuItem.Index = 1;
            this.RedoMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlY;
            this.RedoMenuItem.Text = "&Redo";
            this.RedoMenuItem.Click += new System.EventHandler(this.RedoMenuItem_Click);
            // 
            // MenuItemSeperator1
            // 
            this.MenuItemSeperator1.Index = 2;
            this.MenuItemSeperator1.Text = "-";
            // 
            // TabIndentingMenuItem
            // 
            this.TabIndentingMenuItem.Index = 3;
            this.TabIndentingMenuItem.Text = "Tab Indenting";
            this.TabIndentingMenuItem.Click += new System.EventHandler(this.TabIndentingMenuItem_Click);
            // 
            // MenuItemSeperator3
            // 
            this.MenuItemSeperator3.Index = 4;
            this.MenuItemSeperator3.Text = "-";
            // 
            // SyntaxHighlightingMenuItem
            // 
            this.SyntaxHighlightingMenuItem.Index = 5;
            this.SyntaxHighlightingMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.PlainTextSyntaxMenuItem,
            this.PythonSyntaxMenuItem,
            this.JSONSyntaxMenuItem});
            this.SyntaxHighlightingMenuItem.Text = "Syntax Highlighting";
            // 
            // PlainTextSyntaxMenuItem
            // 
            this.PlainTextSyntaxMenuItem.Index = 0;
            this.PlainTextSyntaxMenuItem.Text = "Plain Text";
            this.PlainTextSyntaxMenuItem.Click += new System.EventHandler(this.PlainTextSyntaxMenuItem_Click);
            // 
            // PythonSyntaxMenuItem
            // 
            this.PythonSyntaxMenuItem.Index = 1;
            this.PythonSyntaxMenuItem.Text = "Python";
            this.PythonSyntaxMenuItem.Click += new System.EventHandler(this.PythonSyntaxMenuItem_Click);
            // 
            // JSONSyntaxMenuItem
            // 
            this.JSONSyntaxMenuItem.Index = 2;
            this.JSONSyntaxMenuItem.Text = "JSON";
            this.JSONSyntaxMenuItem.Click += new System.EventHandler(this.JSONSyntaxMenuItem_Click);
            // 
            // ViewMenuItem
            // 
            this.ViewMenuItem.Index = 2;
            this.ViewMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ZoomInMenuItem,
            this.ZoomOutMenuItem,
            this.DefaultZoomMenuItem});
            this.ViewMenuItem.Text = "&View";
            // 
            // ZoomInMenuItem
            // 
            this.ZoomInMenuItem.Index = 0;
            this.ZoomInMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.ZoomInMenuItem.Text = "Zoom In";
            this.ZoomInMenuItem.Click += new System.EventHandler(this.ZoomInMenuItem_Click);
            // 
            // ZoomOutMenuItem
            // 
            this.ZoomOutMenuItem.Index = 1;
            this.ZoomOutMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.ZoomOutMenuItem.Text = "Zoom Out";
            this.ZoomOutMenuItem.Click += new System.EventHandler(this.ZoomOutMenuItem_Click);
            // 
            // DefaultZoomMenuItem
            // 
            this.DefaultZoomMenuItem.Index = 2;
            this.DefaultZoomMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlD;
            this.DefaultZoomMenuItem.Text = "Default Zoom";
            this.DefaultZoomMenuItem.Click += new System.EventHandler(this.DefaultZoomMenuItem_Click);
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.Index = 3;
            this.HelpMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.AboutNetleniumMenuItem,
            this.AboutPackageToolMenuItem});
            this.HelpMenuItem.Text = "&Help";
            // 
            // AboutNetleniumMenuItem
            // 
            this.AboutNetleniumMenuItem.Index = 0;
            this.AboutNetleniumMenuItem.Text = "&About Netlenium";
            // 
            // AboutPackageToolMenuItem
            // 
            this.AboutPackageToolMenuItem.Index = 1;
            this.AboutPackageToolMenuItem.Text = "&About Package Tool";
            // 
            // TextArea
            // 
            this.TextArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextArea.Location = new System.Drawing.Point(0, 0);
            this.TextArea.Name = "TextArea";
            this.TextArea.Size = new System.Drawing.Size(664, 420);
            this.TextArea.TabIndex = 1;
            this.TextArea.TextChanged += new System.EventHandler(this.TextArea_TextChanged);
            // 
            // FileEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 420);
            this.Controls.Add(this.TextArea);
            this.Icon = global::NetleniumPackageTool.Properties.Resources.editor;
            this.Menu = this.MainMenu;
            this.Name = "FileEditor";
            this.Text = "%FILENAME%";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileEditor_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu MainMenu;
        private System.Windows.Forms.MenuItem FileMenuItem;
        private System.Windows.Forms.MenuItem EditMenuItem;
        private System.Windows.Forms.MenuItem ViewMenuItem;
        private System.Windows.Forms.MenuItem ZoomInMenuItem;
        private System.Windows.Forms.MenuItem ZoomOutMenuItem;
        private ScintillaNET.Scintilla TextArea;
        private System.Windows.Forms.MenuItem DefaultZoomMenuItem;
        private System.Windows.Forms.MenuItem LoadFileContentsMenuItem;
        private System.Windows.Forms.MenuItem SaveFileMenuItem;
        private System.Windows.Forms.MenuItem MenuItemSeperator2;
        private System.Windows.Forms.MenuItem ExitMenuItem;
        private System.Windows.Forms.MenuItem UndoMenuItem;
        private System.Windows.Forms.MenuItem RedoMenuItem;
        private System.Windows.Forms.MenuItem HelpMenuItem;
        private System.Windows.Forms.MenuItem AboutNetleniumMenuItem;
        private System.Windows.Forms.MenuItem MenuItemSeperator1;
        private System.Windows.Forms.MenuItem SyntaxHighlightingMenuItem;
        private System.Windows.Forms.MenuItem PlainTextSyntaxMenuItem;
        private System.Windows.Forms.MenuItem PythonSyntaxMenuItem;
        private System.Windows.Forms.MenuItem JSONSyntaxMenuItem;
        private System.Windows.Forms.MenuItem AboutPackageToolMenuItem;
        private System.Windows.Forms.MenuItem TabIndentingMenuItem;
        private System.Windows.Forms.MenuItem MenuItemSeperator3;
        private System.Windows.Forms.MenuItem ReloadFileMenuItem;
    }
}