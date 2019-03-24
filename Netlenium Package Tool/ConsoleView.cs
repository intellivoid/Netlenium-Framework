using ConEmu.WinForms;
using System.Windows.Forms;

namespace NetleniumPackageTool
{
    /// <summary>
    /// Console View
    /// </summary>
    public partial class ConsoleView : Form
    {
        /// <summary>
        /// ConEmu Control
        /// </summary>
        private ConEmuControl conemu;

        /// <summary>
        /// ConEmu Session Control
        /// </summary>
        private ConEmuSession session;

        /// <summary>
        /// Console Public Constructor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="command"></param>
        public ConsoleView(string title, string command, bool closeAfterCompleted = false)
        {
            InitializeComponent();
            this.Text = $"Netlenium Package Tool - {title}";
           
            this.Controls.Add(conemu = new ConEmuControl() {
                Dock = DockStyle.Fill,
                IsStatusbarVisible = true,
            });

            UpdateSize();

            conemu.AutoStartInfo.ConsoleProcessCommandLine = command;

            if (closeAfterCompleted == true)
            {
                conemu.AutoStartInfo.WhenConsoleProcessExits = WhenConsoleProcessExits.CloseConsoleEmulator;
            }
            else
            {
                conemu.AutoStartInfo.WhenConsoleProcessExits = WhenConsoleProcessExits.KeepConsoleEmulator;
            }

            session = conemu.Start(conemu.AutoStartInfo);

            session.ConsoleEmulatorClosed += delegate
            {
                this.Close();
            };

            session.ConsoleProcessExited += delegate
            {
                if (closeAfterCompleted == true)
                {
                    this.Close();
                }
            };
        }

        /// <summary>
        /// Closes the session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsoleView_FormClosing(object sender, FormClosingEventArgs e)
        {
            session.KillConsoleProcessAsync();
            session.CloseConsoleEmulator();
        }

        /// <summary>
        /// Update the console size
        /// </summary>
        private void UpdateSize()
        {
            conemu.ClientSize = this.ClientSize;
            conemu.MinimumSize = this.ClientSize;
            conemu.MaximumSize = this.ClientSize;
        }

        /// <summary>
        /// Resize Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsoleView_Resize(object sender, System.EventArgs e)
        {
            UpdateSize();
        }
        
    }
}
