namespace NetleniumPackageTool
{
    partial class RenameDirectoryDialog
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
            this.DirectoryNameGroupBox = new System.Windows.Forms.GroupBox();
            this.DirectoryNameTextBox = new System.Windows.Forms.TextBox();
            this.FooterPanel = new System.Windows.Forms.Panel();
            this.CancelButton = new System.Windows.Forms.Button();
            this.RenameButton = new System.Windows.Forms.Button();
            this.DirectoryNameGroupBox.SuspendLayout();
            this.FooterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // DirectoryNameGroupBox
            // 
            this.DirectoryNameGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DirectoryNameGroupBox.Controls.Add(this.DirectoryNameTextBox);
            this.DirectoryNameGroupBox.Location = new System.Drawing.Point(10, 10);
            this.DirectoryNameGroupBox.Name = "DirectoryNameGroupBox";
            this.DirectoryNameGroupBox.Size = new System.Drawing.Size(344, 53);
            this.DirectoryNameGroupBox.TabIndex = 5;
            this.DirectoryNameGroupBox.TabStop = false;
            this.DirectoryNameGroupBox.Text = "Rename Directory";
            // 
            // DirectoryNameTextBox
            // 
            this.DirectoryNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DirectoryNameTextBox.Location = new System.Drawing.Point(10, 22);
            this.DirectoryNameTextBox.Name = "DirectoryNameTextBox";
            this.DirectoryNameTextBox.Size = new System.Drawing.Size(325, 22);
            this.DirectoryNameTextBox.TabIndex = 0;
            this.DirectoryNameTextBox.TextChanged += new System.EventHandler(this.DirectoryNameTextBox_TextChanged);
            this.DirectoryNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DirectoryNameTextBox_KeyDown);
            // 
            // FooterPanel
            // 
            this.FooterPanel.BackColor = System.Drawing.SystemColors.Control;
            this.FooterPanel.Controls.Add(this.CancelButton);
            this.FooterPanel.Controls.Add(this.RenameButton);
            this.FooterPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FooterPanel.Location = new System.Drawing.Point(0, 72);
            this.FooterPanel.Name = "FooterPanel";
            this.FooterPanel.Size = new System.Drawing.Size(364, 39);
            this.FooterPanel.TabIndex = 4;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(201, 7);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 25);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // RenameButton
            // 
            this.RenameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RenameButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.RenameButton.Enabled = false;
            this.RenameButton.Location = new System.Drawing.Point(282, 7);
            this.RenameButton.Name = "RenameButton";
            this.RenameButton.Size = new System.Drawing.Size(75, 25);
            this.RenameButton.TabIndex = 1;
            this.RenameButton.Text = "Rename";
            this.RenameButton.UseVisualStyleBackColor = true;
            // 
            // RenameDirectoryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(364, 111);
            this.Controls.Add(this.DirectoryNameGroupBox);
            this.Controls.Add(this.FooterPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::NetleniumPackageTool.Properties.Resources.logo;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RenameDirectoryDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rename Directory";
            this.Shown += new System.EventHandler(this.RenameDirectoryDialog_Shown);
            this.DirectoryNameGroupBox.ResumeLayout(false);
            this.DirectoryNameGroupBox.PerformLayout();
            this.FooterPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox DirectoryNameGroupBox;
        private System.Windows.Forms.TextBox DirectoryNameTextBox;
        private System.Windows.Forms.Panel FooterPanel;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button RenameButton;
    }
}