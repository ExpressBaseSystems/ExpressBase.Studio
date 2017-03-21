using ExpressBase.Common;
using ExpressBase.Objects;
using ExpressBase.Objects.ServiceStack_Artifacts;
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
        private int Id { get; set; }

        public SqlStatementEditor()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            IServiceClient client = new JsonServiceClient(CacheHelper.SERVICESTACK_URL).WithCache();
            var f = new EbObjectWrapper
            {
                Id = this.Id,
                EbObjectType = Objects.EbObjectType.DataSource,
                Name = txtName.Text.Trim(),
                Bytea = EbSerializers.ProtoBuf_Serialize(new EbDataSource
                    {
                        Id = this.Id,
                        Name = txtName.Text.Trim(),
                        Sql = this.scintilla1.Text.Trim()
                    })
            };

            using (client.Post<HttpWebResponse>(f)) { }

            this.Close();
        }

        public void Set(int id, string name, string sql)
        {
            this.Id = id;
            this.txtName.Text = name;
            this.scintilla1.Text = sql;
        }
    }
}
