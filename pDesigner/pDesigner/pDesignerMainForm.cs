namespace pF.pDesigner {

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.ComponentModel.Design;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing.Design;
    using System.Windows.Markup;
    using WeifenLuo.WinFormsUI.Docking;
    using ExpressBase.Studio;
    using System.Data.Common;
    using ExpressBase.Studio.Controls;
    using ServiceStack;
    using System.Net;
    using System.IO;

    public partial class pDesignerMainForm : DockContent
    {
        public ToolStrip ToolStrip
        {
            get { return toolStrip1; }
        }

        private string _version = string.Empty;
        public string Version {
            get {
                if( string.IsNullOrEmpty( _version ) ) {
                    //- Get the actual version of the file hosted in running assembly
                    System.Diagnostics.FileVersionInfo FVI = System.Diagnostics.FileVersionInfo.GetVersionInfo( System.Reflection.Assembly.GetExecutingAssembly().Location );
                    _version = FVI.ProductVersion;
                }
                return _version;
            }
        }

        public MainForm MainForm
        {
            get
            {
                return _parent;
            }
        }

        public pDesigner pDesignerCore = null;
        private IpDesigner IpDesignerCore = null;

        #region Init

        private MainForm _parent = null;
        private StudioFormTypes FormType = StudioFormTypes.Desktop;

        //- ctor
        public pDesignerMainForm(MainForm parent, StudioFormTypes form_type)
        {
            InitializeComponent();

            _parent = parent;
            FormType = form_type;
            pDesignerCore = new pDesigner(this);

            //- the control: (pDesigner)pDesignerCore 
            IpDesignerCore = this.pDesignerCore as IpDesigner;
            pDesignerCore.Parent = this.pnl4pDesigner;
        }

        private void pDesignerMainForm_Load( object sender, EventArgs e ) {

        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            Toolbox tb = (this.DockPanel.Parent as MainForm).Toolbox;
            if (tb == null || tb.IsDisposed)
                tb = new Toolbox();

            tb.Show(this.DockPanel);
            IpDesignerCore.Toolbox = tb.listBox1;

            if (this.FormType == StudioFormTypes.Desktop)
                (IpDesignerCore as IpDesigner).AddDesignSurface<EbFormControl>(600, 400, AlignmentModeEnum.SnapLines, new Size(1, 1));
            else if (this.FormType == StudioFormTypes.Mobile)
                (IpDesignerCore as IpDesigner).AddDesignSurface<EbFormControl>(414, 736, AlignmentModeEnum.SnapLines, new Size(1, 1));
            else if (this.FormType == StudioFormTypes.UserControl)
                (IpDesignerCore as IpDesigner).AddDesignSurface4Web<System.Web.UI.WebControls.Panel>(150, 150, AlignmentModeEnum.SnapLines, new Size(1, 1));
        }

        public void SetEB_Form(EbFormControl _form)
        {
            _form.DoDesignerLayout(IpDesignerCore as IpDesigner, _form.EbObject);
        }

        #endregion

        #region Menu commands

        private void toolStripMenuItemTabOrder_Click( object sender, EventArgs e )
        {
            IpDesignerCore.SwitchTabOrder();
        }

        #endregion

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            pDesignerCore.DesignSurfaceManager.UpdatePropertyGridHost(pDesignerCore.DesignSurfaceManager.ActiveDesignSurface);
            (IpDesignerCore as pDesigner).SetPropertyGridToParent();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            var x = (IpDesignerCore as pDesigner).Controls[0].Controls[0].Controls[0].Controls;
            IpDesignerCore.DeleteOnDesignSurface();
        }

        private void btnPaste_Click_1(object sender, EventArgs e)
        {
            IpDesignerCore.PasteOnDesignSurface();
        }

        private void btnCopy_Click_1(object sender, EventArgs e)
        {
            IpDesignerCore.CopyOnDesignSurface();
        }

        private void btnCut_Click_1(object sender, EventArgs e)
        {
            IpDesignerCore.CutOnDesignSurface();
        }

        private void btnRedo_Click_1(object sender, EventArgs e)
        {
            IpDesignerCore.RedoOnDesignSurface();
        }

        private void btnUndo_Click_1(object sender, EventArgs e)
        {
            IpDesignerCore.UndoOnDesignSurface();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var _form = (IpDesignerCore as pDesigner).Controls[0].Controls[0].Controls[0] as EbFormControl;
            _form.BeforeSerialization();

            IServiceClient client = new JsonServiceClient("http://localhost:53125/").WithCache();
            var f = new ExpressBase.Studio.Form
            {
                Id = _form.EbObject.Id,
                Name = _form.Name,
                Bytea = ProtoBuf_Serialize((_form as IEbControl).EbObject)
            };

            using (client.Post<HttpWebResponse>(f as object)) { }

            this.Close();
        }

        public byte[] ProtoBuf_Serialize(object obj)
        {
            byte[] buffer = null;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(memoryStream, obj);
                buffer = memoryStream.ToArray();
            }

            return buffer;
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
    }//end_class
}//end_namespace

//(IpDesignerCore as pDesigner).Controls[0].Controls[0].Controls[0].Controls;