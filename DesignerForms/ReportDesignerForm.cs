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
        private pF.pDesigner.pDesigner pDesignerCore1, pDesignerCore2, pDesignerCore3, pDesignerCore4, pDesignerCore5;

        internal override ToolStrip ToolStrip
        {
            get { return new ToolStrip(); }
        }

        //public pF.pDesigner.pDesigner pDesignerCore = null;
        //private IpDesigner IpDesignerCore = null;

        public ReportDesignerForm()
        {
            InitializeComponent();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            Toolbox tb = this.MainForm.Toolbox;
            if (tb == null || tb.IsDisposed)
                tb = new Toolbox();

            tb.Show(this.DockPanel);

            pDesignerCore1 = new pF.pDesigner.pDesigner(this.MainForm.PropertyWindow);
            pDesignerCore1.Parent = this.panelRHc;
            (pDesignerCore1 as IpDesigner).Toolbox = tb.listBox1;
            (pDesignerCore1 as IpDesigner).AddDesignSurface<EbReportPanel>(414, 736, AlignmentModeEnum.SnapLines, new Size(1, 1));

            pDesignerCore2 = new pF.pDesigner.pDesigner(this.MainForm.PropertyWindow);
            pDesignerCore2.Parent = this.panelPHc;
            (pDesignerCore2 as IpDesigner).Toolbox = tb.listBox1;
            (pDesignerCore2 as IpDesigner).AddDesignSurface<EbReportPanel>(414, 736, AlignmentModeEnum.SnapLines, new Size(1, 1));

            pDesignerCore3 = new pF.pDesigner.pDesigner(this.MainForm.PropertyWindow);
            pDesignerCore3.Parent = this.panelDTc;
            (pDesignerCore3 as IpDesigner).Toolbox = tb.listBox1;
            (pDesignerCore3 as IpDesigner).AddDesignSurface<EbReportPanel>(414, 736, AlignmentModeEnum.SnapLines, new Size(1, 1));

            pDesignerCore4 = new pF.pDesigner.pDesigner(this.MainForm.PropertyWindow);
            pDesignerCore4.Parent = this.panelPFc;
            (pDesignerCore4 as IpDesigner).Toolbox = tb.listBox1;
            (pDesignerCore4 as IpDesigner).AddDesignSurface<EbReportPanel>(414, 736, AlignmentModeEnum.SnapLines, new Size(1, 1));

            pDesignerCore5 = new pF.pDesigner.pDesigner(this.MainForm.PropertyWindow);
            pDesignerCore5.Parent = this.panelRFc;
            (pDesignerCore5 as IpDesigner).Toolbox = tb.listBox1;
            (pDesignerCore5 as IpDesigner).AddDesignSurface<EbReportPanel>(414, 736, AlignmentModeEnum.SnapLines, new Size(1, 1));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var _rH = (pDesignerCore1 as pF.pDesigner.pDesigner).Controls[0].Controls[0].Controls[0] as EbReportPanel;
            _rH.BeforeSerialization();

            var _pH = (pDesignerCore1 as pF.pDesigner.pDesigner).Controls[0].Controls[0].Controls[0] as EbReportPanel;
            _pH.BeforeSerialization();

            var _dT = (pDesignerCore1 as pF.pDesigner.pDesigner).Controls[0].Controls[0].Controls[0] as EbReportPanel;
            _dT.BeforeSerialization();

            var _pF = (pDesignerCore1 as pF.pDesigner.pDesigner).Controls[0].Controls[0].Controls[0] as EbReportPanel;
            _pF.BeforeSerialization();

            var _rF = (pDesignerCore1 as pF.pDesigner.pDesigner).Controls[0].Controls[0].Controls[0] as EbReportPanel;
            _rF.BeforeSerialization();

            EbReportDefinition _rd = new EbReportDefinition
            {
                ReportHeader = (_rH as IEbControl).EbControl,
                ReportFooter = (_rF as IEbControl).EbControl,
                PageHeader = (_pH as IEbControl).EbControl,
                PageFooter = (_pF as IEbControl).EbControl,
                Details = (_dT as IEbControl).EbControl
            };

            IServiceClient client = new JsonServiceClient("http://localhost:53125/").WithCache();
            var f = new ExpressBase.ServiceStack.EbObjectWrapper
            {
                Id = _rH.EbControl.Id,
                EbObjectType = ExpressBase.Objects.EbObjectType.Report,
                Name = _rH.Name,
                Bytea = EbSerializers.ProtoBuf_Serialize(_rd)
            };

            using (client.Post<HttpWebResponse>(f as object)) { }

            this.Close();
        }
    }
}
