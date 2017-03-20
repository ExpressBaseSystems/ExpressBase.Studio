using ExpressBase.Common;
using ExpressBase.Studio.Controls;
using ExpressBase.Objects;
using pF.pDesigner;
using ServiceStack;
using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Net;
using System.Windows.Forms;
using ExpressBase.Studio.DesignerForms;
using ExpressBase.Objects.ServiceStack_Artifacts;

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
                var fr = client.Get<EbObjectResponse>(new EbObjectRequest() { Id = id, Token=MainForm.JwtToken });

                var _formEbObject = EbSerializers.ProtoBuf_DeSerialize<EbObject>(fr.Data[0].Bytea);
                _formEbObject.EbObjectType = fr.Data[0].EbObjectType;
                _formEbObject.Name = fr.Data[0].Name;
                _formEbObject.Id = fr.Data[0].Id;

                if (_formEbObject.EbObjectType == EbObjectType.Form)
                {
                    var _form_name = string.Format("form_{0}", id);
                    Form fc = Application.OpenForms[_form_name];
                    if (fc == null)
                    {
                        FormDesignerForm pD = new FormDesignerForm(this.MainForm, StudioFormTypes.Desktop);
                        pD.Name = _form_name;
                        pD.Show(MainForm.DockPanel);
                        var _form = new EbFormControl();
                        _form.EbControlContainer = _formEbObject as EbControlContainer;
                        _form.EbControlContainer.Id = id;
                        pD.SetEB_Form(_form);
                    }
                    else
                    {
                        fc.Activate();
                    }
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
            var fr = client.Get<EbObjectResponse>(new EbObjectRequest() { Id = 0, Token = MainForm.JwtToken });

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