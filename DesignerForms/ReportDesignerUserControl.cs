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
        internal EbReportDefinition ReportDefinition { get; set; }

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

            this.ReportDefinition.ReportFooters.Sort();
            this.ReportDefinition.ReportFooters.Reverse();
            foreach (EbReportSection section in this.ReportDefinition.ReportFooters)
                AddSection(section);

            this.ReportDefinition.PageFooters.Sort();
            this.ReportDefinition.PageFooters.Reverse();
            foreach (EbReportSection section in this.ReportDefinition.PageFooters)
                AddSection(section);

            this.ReportDefinition.Details.Sort();
            this.ReportDefinition.Details.Reverse();
            foreach (EbReportSection section in this.ReportDefinition.Details)
                AddSection(section);

            this.ReportDefinition.PageHeaders.Sort();
            this.ReportDefinition.PageHeaders.Reverse();
            foreach (EbReportSection section in this.ReportDefinition.PageHeaders)
                AddSection(section);

            this.ReportDefinition.ReportHeaders.Sort();
            this.ReportDefinition.ReportHeaders.Reverse();
            foreach (EbReportSection section in this.ReportDefinition.ReportHeaders)
                AddSection(section);

            this.ResumeLayout(true);
        }

        private pF.pDesigner.pDesigner pDesignerCore1 { get; set; }

        private void AddSection(EbReportSection section)
        {
            if (section.Panel == null)
            {
                section.Panel = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 100,
                    BackColor = Color.Transparent
                };

                Color btnColor = Color.White;
                if (section.Type == EbReportSectionType.ReportHeader || section.Type == EbReportSectionType.ReportFooter)
                    btnColor = Color.LightSeaGreen;
                else if (section.Type == EbReportSectionType.PageHeader || section.Type == EbReportSectionType.PageFooter)
                    btnColor = Color.LightSkyBlue;
                else if (section.Type == EbReportSectionType.Detail)
                    btnColor = Color.SandyBrown;

                var sectionlbl = new Label { Dock = DockStyle.Left, BackColor = btnColor, Width = 35 };
                sectionlbl.Tag = section;
                sectionlbl.MouseClick += Sectionlbl_MouseClick;

                section.Panel.Controls.Add(sectionlbl);

                if (pDesignerCore1 == null)
                {
                    pDesignerCore1 = new pF.pDesigner.pDesigner(this.MainForm.PropertyWindow);
                    pDesignerCore1.BackColor = Color.Transparent;
                    pDesignerCore1.Parent = this;
                    (pDesignerCore1 as IpDesigner).Toolbox = this.MainForm.Toolbox.listBox1;
                }

                (pDesignerCore1 as IpDesigner).AddReportSectionDesignSurface(section.Panel, this);
            }

            (section.Panel.Controls[0] as Label).Text = section.Name;
            this.Controls.Add(section.Panel);
            this.Controls.Add(new Splitter { Dock = DockStyle.Top, BackColor = Color.DarkBlue, Width = 0 });
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
            if (_justNowClickedSection.Type == EbReportSectionType.ReportHeader)
                this.ReportDefinition.ReportHeaders.InsertAbove(_justNowClickedSection);
            else if (_justNowClickedSection.Type == EbReportSectionType.PageHeader)
                this.ReportDefinition.PageHeaders.InsertAbove(_justNowClickedSection);
            else if (_justNowClickedSection.Type == EbReportSectionType.Detail)
                this.ReportDefinition.Details.InsertAbove(_justNowClickedSection);
            else if (_justNowClickedSection.Type == EbReportSectionType.PageFooter)
                this.ReportDefinition.PageFooters.InsertAbove(_justNowClickedSection);
            else if (_justNowClickedSection.Type == EbReportSectionType.ReportFooter)
                this.ReportDefinition.ReportFooters.InsertAbove(_justNowClickedSection);

            this.InitSections();
        }

        private void ItemInsertBelow_Click(object sender, EventArgs e)
        {
            if (_justNowClickedSection.Type == EbReportSectionType.ReportHeader)
                this.ReportDefinition.ReportHeaders.InsertBelow(_justNowClickedSection);
            else if (_justNowClickedSection.Type == EbReportSectionType.PageHeader)
                this.ReportDefinition.PageHeaders.InsertBelow(_justNowClickedSection);
            else if (_justNowClickedSection.Type == EbReportSectionType.Detail)
                this.ReportDefinition.Details.InsertBelow(_justNowClickedSection);
            else if (_justNowClickedSection.Type == EbReportSectionType.PageFooter)
                this.ReportDefinition.PageFooters.InsertBelow(_justNowClickedSection);
            else if (_justNowClickedSection.Type == EbReportSectionType.ReportFooter)
                this.ReportDefinition.ReportFooters.InsertBelow(_justNowClickedSection);

            this.InitSections();
        }

        private void ItemDeleteSection_Click(object sender, EventArgs e)
        {
            if (_justNowClickedSection.Type == EbReportSectionType.ReportHeader)
                this.ReportDefinition.ReportHeaders.DeleteSection(_justNowClickedSection);
            else if (_justNowClickedSection.Type == EbReportSectionType.PageHeader)
                this.ReportDefinition.PageHeaders.DeleteSection(_justNowClickedSection);
            else if (_justNowClickedSection.Type == EbReportSectionType.Detail)
                this.ReportDefinition.Details.DeleteSection(_justNowClickedSection);
            else if (_justNowClickedSection.Type == EbReportSectionType.PageFooter)
                this.ReportDefinition.PageFooters.DeleteSection(_justNowClickedSection);
            else if (_justNowClickedSection.Type == EbReportSectionType.ReportFooter)
                this.ReportDefinition.ReportFooters.DeleteSection(_justNowClickedSection);

            this.InitSections();
        }

        protected override void OnResize(EventArgs e)
        {
            this.SuspendLayout();
            base.OnResize(e);
            this.ResumeLayout(true);
        }

        internal pF.DesignSurfaceExt.DesignSurfaceExt2 ActiveDesignSurface
        {
            get
            {
                return pDesignerCore1.ActiveDesignSurface;
            }
        }

        internal pF.pDesigner.pDesigner DesignerCore
        {
            get
            {
                return this.pDesignerCore1;
            }
        }
    }
}
