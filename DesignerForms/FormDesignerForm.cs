using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ExpressBase.Studio.Controls;
using ServiceStack;
using System.Net;
using ExpressBase.Common;
using pF.pDesigner;
using ExpressBase.Objects.ServiceStack_Artifacts;

namespace ExpressBase.Studio.DesignerForms
{
    public partial class FormDesignerForm : BaseDesignerForm
    {
        internal override ToolStrip ToolStrip
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

        public pF.pDesigner.pDesigner pDesignerCore = null;
        private IpDesigner IpDesignerCore = null;

        #region Init

        private StudioFormTypes FormType = StudioFormTypes.Desktop;

        //- ctor
        public FormDesignerForm(MainForm parent, StudioFormTypes form_type)
        {
            InitializeComponent();

            base.MainForm = parent;
            FormType = form_type;
            pDesignerCore = new pF.pDesigner.pDesigner(this.MainForm.PropertyWindow);

            //- the control: (pDesigner)pDesignerCore 
            IpDesignerCore = this.pDesignerCore as IpDesigner;
            pDesignerCore.Parent = this.pnl4pDesigner;
        }

        private void pDesignerMainForm_Load( object sender, EventArgs e ) {

        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            pDesignerCore.PropertyGridHost.ReloadComboBox();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            Toolbox tb = this.MainForm.Toolbox;
            if (tb == null || tb.IsDisposed)
                tb = new Toolbox();

            tb.Show(this.DockPanel);
            IpDesignerCore.Toolbox = tb.listBox1;

            if (this.FormType == StudioFormTypes.Desktop)
                (IpDesignerCore as IpDesigner).AddDesignSurface<EbFormControl>(600, 400, AlignmentModeEnum.SnapLines, new Size(1, 1));
            else if (this.FormType == StudioFormTypes.Mobile)
                (IpDesignerCore as IpDesigner).AddDesignSurface<EbFormControl>(414, 736, AlignmentModeEnum.SnapLines, new Size(1, 1));
            else if (this.FormType == StudioFormTypes.UserControl)
                (IpDesignerCore as IpDesigner).AddDesignSurface<EbFormControl>(150, 150, AlignmentModeEnum.SnapLines, new Size(1, 1));
            else if (this.FormType == StudioFormTypes.Report)
                (IpDesignerCore as IpDesigner).AddDesignSurface<ReportDesignerForm>(414, 736, AlignmentModeEnum.SnapLines, new Size(1, 1));
        }

        public void SetEB_Form(EbFormControl _form)
        {
            _form.DoDesignerLayout(IpDesignerCore as IpDesigner, _form.EbControlContainer);
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
            (IpDesignerCore as pF.pDesigner.pDesigner).SetPropertyGridToParent();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            var x = (IpDesignerCore as pF.pDesigner.pDesigner).Controls[0].Controls[0].Controls[0].Controls;
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
            var _form = (IpDesignerCore as pF.pDesigner.pDesigner).Controls[0].Controls[0].Controls[0] as EbFormControl;
            _form.BeforeSerialization();

            IServiceClient client = new JsonServiceClient(CacheHelper.SERVICESTACK_URL).WithCache();
            var f = new EbObjectWrapper
            {
                Id = _form.EbControlContainer.Id,
                EbObjectType = ExpressBase.Objects.EbObjectType.Form,
                Name = _form.Name,
                Bytea = EbSerializers.ProtoBuf_Serialize(_form.EbControlContainer)
            };

            using (client.Post<HttpWebResponse>(f as object)) { }

            this.Close();
        }

        internal override pF.DesignSurfaceExt.DesignSurfaceExt2 ActiveDesignSurface
        {
            get
            {
                return this.pDesignerCore.ActiveDesignSurface;
            }
        }

        internal override pF.pDesigner.pDesigner DesignerCore
        {
            get
            {
                return this.pDesignerCore;
            }
        }

    }//end_class
}//end_namespace

//(IpDesignerCore as pDesigner).Controls[0].Controls[0].Controls[0].Controls;