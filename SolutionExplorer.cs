using ExpressBase.Common;
using ExpressBase.ServiceStack;
using ExpressBase.Studio.Controls;
using ExpressBase.UI;
using pF.pDesigner;
using ServiceStack;
using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace ExpressBase.Studio
{
    public partial class SolutionExplorer : ToolWindow
    {
        private MainForm MainForm { get; set; }

        public SolutionExplorer(MainForm parent)
        {
            InitializeComponent();
            this.MainForm = parent;

            treeView1.NodeMouseDoubleClick += TreeView1_NodeMouseDoubleClick;

            RefreshTreeView();
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = e.Node;
            if (node != null)
            {
                int id = Convert.ToInt32(node.Tag);

                IServiceClient client = new JsonServiceClient("http://localhost:53125/").WithCache();
                var fr = client.Get<EbObjectResponse>(string.Format("http://localhost:53125/ebo/{0}", id));

                var _formEbObject = EbSerializers.ProtoBuf_DeSerialize<EbObject>(fr.Data[0].Bytea);
                _formEbObject.EbObjectType = fr.Data[0].EbObjectType;
                _formEbObject.Name = fr.Data[0].Name;
                _formEbObject.Id = fr.Data[0].Id;

                if (_formEbObject.EbObjectType == EbObjectType.Form)
                {
                    pDesignerMainForm pD = new pDesignerMainForm(this.MainForm, StudioFormTypes.Desktop);
                    pD.Show(MainForm.DockPanel);
                    var _form = new EbFormControl();
                    _form.EbControl = _formEbObject as EbControl;
                    _form.EbControl.Id = id;
                    pD.SetEB_Form(_form);
                }
                else if (_formEbObject.EbObjectType == EbObjectType.DataSource)
                {
                    SqlStatementEditor ed = new SqlStatementEditor();
                    ed.Set(id, _formEbObject.Name, (_formEbObject as EbDataSource).Sql);
                    ed.Show(MainForm.DockPanel);
                }
            }
        }

        protected override void OnRightToLeftLayoutChanged(EventArgs e)
        {
            treeView1.RightToLeftLayout = RightToLeftLayout;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void RefreshTreeView()
        {
            IServiceClient client = new JsonServiceClient("http://localhost:53125/").WithCache();
            var fr = client.Get<EbObjectResponse>("http://localhost:53125/ebo?format=json");

            treeView1.SuspendLayout();
            treeView1.Nodes[0].Nodes.Clear();

            foreach (EbObjectWrapper dr in fr.Data)
            {
                var nodetemp = new TreeNode(string.Format("{0} ({1})", dr.Name, dr.Id), 8, 8);
                nodetemp.Tag = dr.Id.ToString();
                nodetemp.Name = dr.Id.ToString();
                treeView1.Nodes[0].Nodes.Add(nodetemp);
            }

            treeView1.ExpandAll();
            treeView1.ResumeLayout(true);
        }
    }
}