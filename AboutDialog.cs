using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ExpressBase.Studio
{
    public partial class AboutDialog : System.Windows.Forms.Form
    {
        public AboutDialog()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
        }

        private void AboutDialog_Load(object sender, EventArgs e)
        {
            labelAppVersion.Text = typeof(MainForm).Assembly.GetName().Version.ToString();
            labelLibVersion.Text = typeof(DockPanel).Assembly.GetName().Version.ToString();
        }
    }
}