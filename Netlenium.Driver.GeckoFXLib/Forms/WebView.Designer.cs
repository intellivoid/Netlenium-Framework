namespace Netlenium.Driver.GeckoFXLib.Forms
{
    partial class WebView
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
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.FileMenuItem = new System.Windows.Forms.MenuItem();
            this.ExitMenuItem = new System.Windows.Forms.MenuItem();
            this.AboutMenuItem = new System.Windows.Forms.MenuItem();
            this.AboutNetleniumMenuItem = new System.Windows.Forms.MenuItem();
            this.GeckoWebBrowser = new Gecko.GeckoWebBrowser();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabel,
            this.ToolStripProgressBar});
            this.StatusStrip.Location = new System.Drawing.Point(0, 293);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(559, 22);
            this.StatusStrip.TabIndex = 0;
            // 
            // ToolStripStatusLabel
            // 
            this.ToolStripStatusLabel.Name = "ToolStripStatusLabel";
            this.ToolStripStatusLabel.Size = new System.Drawing.Size(42, 17);
            this.ToolStripStatusLabel.Text = "Ready!";
            // 
            // ToolStripProgressBar
            // 
            this.ToolStripProgressBar.Name = "ToolStripProgressBar";
            this.ToolStripProgressBar.Size = new System.Drawing.Size(164, 16);
            this.ToolStripProgressBar.Visible = false;
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.FileMenuItem,
            this.AboutMenuItem});
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.Index = 0;
            this.FileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ExitMenuItem});
            this.FileMenuItem.Text = "&File";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Index = 0;
            this.ExitMenuItem.Text = "&Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Index = 1;
            this.AboutMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.AboutNetleniumMenuItem});
            this.AboutMenuItem.Text = "&About";
            // 
            // AboutNetleniumMenuItem
            // 
            this.AboutNetleniumMenuItem.Index = 0;
            this.AboutNetleniumMenuItem.Text = "&About Netlenium";
            this.AboutNetleniumMenuItem.Click += new System.EventHandler(this.AboutNetleniumMenuItem_Click);
            // 
            // GeckoWebBrowser
            // 
            this.GeckoWebBrowser.ConsoleMessageEventReceivesConsoleLogCalls = true;
            this.GeckoWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GeckoWebBrowser.FrameEventsPropagateToMainWindow = false;
            this.GeckoWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.GeckoWebBrowser.Name = "GeckoWebBrowser";
            this.GeckoWebBrowser.Size = new System.Drawing.Size(559, 293);
            this.GeckoWebBrowser.TabIndex = 1;
            this.GeckoWebBrowser.UseHttpActivityObserver = false;
            this.GeckoWebBrowser.DocumentCompleted += new System.EventHandler<Gecko.Events.GeckoDocumentCompletedEventArgs>(this.GeckoWebBrowser_DocumentCompleted);
            this.GeckoWebBrowser.ProgressChanged += new System.EventHandler<Gecko.GeckoProgressEventArgs>(this.GeckoWebBrowser_ProgressChanged);
            this.GeckoWebBrowser.StatusTextChanged += new System.EventHandler(this.GeckoWebBrowser_StatusTextChanged);
            // 
            // WebView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 315);
            this.Controls.Add(this.GeckoWebBrowser);
            this.Controls.Add(this.StatusStrip);
            this.Icon = global::Netlenium.Driver.GeckoFXLib.Properties.Resources.logo;
            this.Menu = this.MainMenu;
            this.MinimumSize = new System.Drawing.Size(220, 160);
            this.Name = "WebView";
            this.Text = "Netlenium";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WebView_FormClosed);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.MainMenu MainMenu;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar ToolStripProgressBar;
        private System.Windows.Forms.MenuItem FileMenuItem;
        private System.Windows.Forms.MenuItem ExitMenuItem;
        private System.Windows.Forms.MenuItem AboutMenuItem;
        private System.Windows.Forms.MenuItem AboutNetleniumMenuItem;
        public Gecko.GeckoWebBrowser GeckoWebBrowser;
    }
}