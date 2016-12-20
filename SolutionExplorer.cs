using ExpressBase.Studio.Controls;
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

            IServiceClient client = new JsonServiceClient("http://localhost:53125/").WithCache();
            var fr = client.Get<FormResponse>("http://localhost:53125/forms?format=json");

            treeView1.SuspendLayout();

            foreach (Form dr in fr.Data)
            {
                var nodetemp = new TreeNode(dr.Name, 8, 8);
                nodetemp.Tag = dr.Id.ToString();
                nodetemp.Name = dr.Id.ToString();
                treeView1.Nodes[0].Nodes.Add(nodetemp);
            }

            treeView1.ExpandAll();
            treeView1.ResumeLayout(true);
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = e.Node;
            if (node != null)
            {
                int id = Convert.ToInt32(node.Tag);

                IServiceClient client = new JsonServiceClient("http://localhost:53125/").WithCache();
                var fr = client.Get<FormResponse>(string.Format("http://localhost:53125/forms/{0}", id));

                var _form = ProtoBuf_DeSerialize<EbFormControl>(fr.Data[0].Bytea);
                pDesignerMainForm pD = new pDesignerMainForm(this.MainForm, StudioFormTypes.Desktop);
                pD.Show(MainForm.DockPanel);
                pD.SetEB_Form(_form);
            }
        }

        protected override void OnRightToLeftLayoutChanged(EventArgs e)
        {
            treeView1.RightToLeftLayout = RightToLeftLayout;
        }

        public T ProtoBuf_DeSerialize<T>(byte[] bytea)
        {
            object obj = null;

            using (var mem2 = new MemoryStream(bytea))
            {
                obj = ProtoBuf.Serializer.Deserialize<T>(mem2);
            }

            return (T)obj;
        }
    }
}