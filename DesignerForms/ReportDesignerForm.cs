using System.Windows.Forms;
using pF.pDesigner;
using System;
using System.Drawing;
using ExpressBase.Studio.Controls;
using ExpressBase.Studio.ControlContainers;
using ServiceStack;
using ExpressBase.Common;
using System.Net;
using ExpressBase.Objects;

namespace ExpressBase.Studio.DesignerForms
{
    public partial class ReportDesignerForm : BaseDesignerForm
    {
        private EbReportDefinition ReportDefinition { get; set; }

        internal override ToolStrip ToolStrip
        {
            get { return new ToolStrip(); }
        }

        public ReportDesignerForm(EbReportDefinition def)
        {
            InitializeComponent();
            this.ReportDefinition = def;
            this.DoubleBuffered = true;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            Toolbox tb = this.MainForm.Toolbox;
            if (tb == null || tb.IsDisposed)
                tb = new Toolbox();

            tb.Redraw(this.ReportDefinition.ColumnColletion);
            tb.Show(this.DockPanel);
            this.AutoScroll = true;

            var _uc = new ReportDesignerUserControl(this.ReportDefinition);
            _uc.Dock = DockStyle.Top;
            _uc.MainForm = this.MainForm;

            this.Controls.Add(_uc);
        }

        protected override void OnResize(EventArgs e)
        {
            this.SuspendLayout();
            base.OnResize(e);
            this.ResumeLayout(true);
        }
    }
}

//private pF.pDesigner.pDesigner pDesignerCore1, pDesignerCore2, pDesignerCore3, pDesignerCore4, pDesignerCore5;
//private void toolStripButton1_Click(object sender, EventArgs e)
//{
//    var _rH = (pDesignerCore1 as pF.pDesigner.pDesigner).Controls[0].Controls[0].Controls[0] as EbReportPanel;
//    _rH.BeforeSerialization();

//    var _pH = (pDesignerCore1 as pF.pDesigner.pDesigner).Controls[0].Controls[0].Controls[0] as EbReportPanel;
//    _pH.BeforeSerialization();

//    var _dT = (pDesignerCore1 as pF.pDesigner.pDesigner).Controls[0].Controls[0].Controls[0] as EbReportPanel;
//    _dT.BeforeSerialization();

//    var _pF = (pDesignerCore1 as pF.pDesigner.pDesigner).Controls[0].Controls[0].Controls[0] as EbReportPanel;
//    _pF.BeforeSerialization();

//    var _rF = (pDesignerCore1 as pF.pDesigner.pDesigner).Controls[0].Controls[0].Controls[0] as EbReportPanel;
//    _rF.BeforeSerialization();

//    EbReportDefinition _rd = new EbReportDefinition
//    {
//        ReportHeader = (_rH as IEbControl).EbControl,
//        ReportFooter = (_rF as IEbControl).EbControl,
//        PageHeader = (_pH as IEbControl).EbControl,
//        PageFooter = (_pF as IEbControl).EbControl,
//        Details = (_dT as IEbControl).EbControl
//    };

//    IServiceClient client = new JsonServiceClient("http://localhost:53125/").WithCache();
//    var f = new ExpressBase.ServiceStack.EbObjectWrapper
//    {
//        Id = _rH.EbControlContainer.Id,
//        EbObjectType = ExpressBase.Objects.EbObjectType.Report,
//        Name = _rH.Name,
//        Bytea = EbSerializers.ProtoBuf_Serialize(_rd)
//    };

//    using (client.Post<HttpWebResponse>(f as object)) { }

//    this.Close();
//}

