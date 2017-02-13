using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pF.pDesigner;
using ExpressBase.Studio.ControlContainers;
using ExpressBase.Objects;

namespace ExpressBase.Studio.DesignerForms
{
    public partial class ReportDesignerUserControl : UserControl
    {
        private EbReportDefinition ReportDefinition { get; set; }

        internal MainForm MainForm { get; set; }

        internal ToolStrip ToolStrip { get; }

        public ReportDesignerUserControl(EbReportDefinition def)
        {
            InitializeComponent();
            this.ReportDefinition = def;
            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            this.InitSections();
        }

        private void InitSections()
        {
            this.Height = (int)this.ReportDefinition.PaperSize.Height;
            this.Width = (int)this.ReportDefinition.PaperSize.Width;

            this.SuspendLayout();
            this.Controls.Clear();

            this.ReportDefinition.ReportHeaders.Sort();
            foreach (EbReportSection section in this.ReportDefinition.ReportHeaders)
                AddSection(section);

            this.ReportDefinition.PageHeaders.Sort();
            foreach (EbReportSection section in this.ReportDefinition.PageHeaders)
                AddSection(section);

            this.ReportDefinition.Details.Sort();
            foreach (EbReportSection section in this.ReportDefinition.Details)
                AddSection(section);

            this.ReportDefinition.PageFooters.Sort();
            foreach (EbReportSection section in this.ReportDefinition.PageFooters)
                AddSection(section);

            this.ReportDefinition.ReportFooters.Sort();
            foreach (EbReportSection section in this.ReportDefinition.ReportFooters)
                AddSection(section);

            this.ResumeLayout(true);
        }

        private void AddSection(EbReportSection section)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.Transparent
            };

            Color btnColor = Color.White;
            if (section is EbReportHeaderSection || section is EbReportFooterSection)
                btnColor = Color.LightSeaGreen;
            else if (section is EbReportPageHeaderSection || section is EbReportPageFooterSection)
                btnColor = Color.LightSkyBlue;
            else if (section is EbReportDetailSection)
                btnColor = Color.SandyBrown;

            var sectionlbl = new Label { Dock = DockStyle.Left, Text = section.Name, BackColor = btnColor, Width = 35 };
            sectionlbl.Tag = section;
            sectionlbl.MouseClick += Sectionlbl_MouseClick;

            panel.Controls.Add(sectionlbl);

            var pDesignerCore1 = new pF.pDesigner.pDesigner(this.MainForm.PropertyWindow);
            pDesignerCore1.BackColor = Color.Transparent;
            pDesignerCore1.Parent = panel;
            (pDesignerCore1 as IpDesigner).Toolbox = this.MainForm.Toolbox.listBox1;
            (pDesignerCore1 as IpDesigner).AddDesignSurface<EbReportPanel>(600, 100, AlignmentModeEnum.SnapLines, new Size(1, 1));

            this.Controls.Add(panel);
            this.Controls.Add(new Splitter { Dock = DockStyle.Top, BackColor = Color.DarkBlue, Width = 1 });
        }

        private EbReportSection _justNowClickedSection = null;

        private void Sectionlbl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _justNowClickedSection = (sender as Label).Tag as EbReportSection;

                var itemInsertAbove = new MenuItem("Insert Section Above");
                var itemInsertBelow = new MenuItem("Insert Section Below");
                var itemDeleteSection = new MenuItem("Delete Section");
                itemInsertAbove.Click += ItemInsertAbove_Click;
                itemInsertBelow.Click += ItemInsertBelow_Click;
                itemDeleteSection.Click += ItemDeleteSection_Click;

                (sender as Label).ContextMenu = new ContextMenu(new MenuItem[] { itemInsertAbove, itemInsertBelow, itemDeleteSection });
            }
        }

        private void ItemInsertAbove_Click(object sender, EventArgs e)
        {
            if (_justNowClickedSection is EbReportHeaderSection)
                this.ReportDefinition.ReportHeaders.InsertBefore(_justNowClickedSection as EbReportHeaderSection);
            else if (_justNowClickedSection is EbReportPageHeaderSection)
                this.ReportDefinition.PageHeaders.InsertBefore(_justNowClickedSection as EbReportPageHeaderSection);
            else if (_justNowClickedSection is EbReportDetailSection)
                this.ReportDefinition.Details.InsertBefore(_justNowClickedSection as EbReportDetailSection);
            else if (_justNowClickedSection is EbReportPageFooterSection)
                this.ReportDefinition.PageFooters.InsertBefore(_justNowClickedSection as EbReportPageFooterSection);
            else if (_justNowClickedSection is EbReportFooterSection)
                this.ReportDefinition.ReportFooters.InsertBefore(_justNowClickedSection as EbReportFooterSection);

            this.InitSections();
        }

        private void ItemInsertBelow_Click(object sender, EventArgs e)
        {
            if (_justNowClickedSection is EbReportHeaderSection)
                this.ReportDefinition.ReportHeaders.InsertAfter(_justNowClickedSection as EbReportHeaderSection);
            else if (_justNowClickedSection is EbReportPageHeaderSection)
                this.ReportDefinition.PageHeaders.InsertAfter(_justNowClickedSection as EbReportPageHeaderSection);
            else if (_justNowClickedSection is EbReportDetailSection)
                this.ReportDefinition.Details.InsertAfter(_justNowClickedSection as EbReportDetailSection);
            else if (_justNowClickedSection is EbReportPageFooterSection)
                this.ReportDefinition.PageFooters.InsertAfter(_justNowClickedSection as EbReportPageFooterSection);
            else if (_justNowClickedSection is EbReportFooterSection)
                this.ReportDefinition.ReportFooters.InsertAfter(_justNowClickedSection as EbReportFooterSection);

            this.InitSections();
        }

        private void ItemDeleteSection_Click(object sender, EventArgs e)
        {
            if (_justNowClickedSection is EbReportHeaderSection)
                this.ReportDefinition.ReportHeaders.DeleteSection(_justNowClickedSection as EbReportHeaderSection);
            else if (_justNowClickedSection is EbReportPageHeaderSection)
                this.ReportDefinition.PageHeaders.DeleteSection(_justNowClickedSection as EbReportPageHeaderSection);
            else if (_justNowClickedSection is EbReportDetailSection)
                this.ReportDefinition.Details.DeleteSection(_justNowClickedSection as EbReportDetailSection);
            else if (_justNowClickedSection is EbReportPageFooterSection)
                this.ReportDefinition.PageFooters.DeleteSection(_justNowClickedSection as EbReportPageFooterSection);
            else if (_justNowClickedSection is EbReportFooterSection)
                this.ReportDefinition.ReportFooters.DeleteSection(_justNowClickedSection as EbReportFooterSection);

            this.InitSections();
        }

        protected override void OnResize(EventArgs e)
        {
            this.SuspendLayout();
            base.OnResize(e);
            this.ResumeLayout(true);
        }
    }
}
