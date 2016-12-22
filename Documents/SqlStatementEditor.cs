using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ExpressBase.Studio
{
    public partial class SqlStatementEditor : DockContent
    {
        public SqlStatementEditor()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            IServiceClient client = new JsonServiceClient("http://localhost:53125/").WithCache();
            var f = new ExpressBase.Studio.EbDataSource
            {
                Name = txtName.Text.Trim(),
                Sql = this.scintilla1.Text.Trim()
            };

            using (client.Post<HttpWebResponse>(f)) { }

            this.Close();
        }
    }
}
