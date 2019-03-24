namespace NetleniumPackageTool
{
    partial class ConsoleView
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
            this.SuspendLayout();
            // 
            // ConsoleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(664, 410);
            this.Icon = global::NetleniumPackageTool.Properties.Resources.terminal_icon;
            this.Name = "ConsoleView";
            this.Text = "ConsoleView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConsoleView_FormClosing);
            this.ResizeBegin += new System.EventHandler(this.ConsoleView_Resize);
            this.ResizeEnd += new System.EventHandler(this.ConsoleView_Resize);
            this.Resize += new System.EventHandler(this.ConsoleView_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}