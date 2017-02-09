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

        private List<string> sections { get; set; }

        internal MainForm MainForm { get; set; }

        internal ToolStrip ToolStrip { get; }

        public ReportDesignerUserControl(EbReportDefinition def)
        {
            InitializeComponent();
            this.ReportDefinition = def;
            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;

            sections = new List<string>();
        }

        internal ReportDesignerUserControl(EbReportDefinition def, params string[] sectionnames)
        {
            InitializeComponent();
            this.ReportDefinition = def;
            this.BackColor = Color.Transparent;

            if (sectionnames == null & sectionnames.Length != 5)
                throw new NotSupportedException("You can't use this");

            sections = new List<string>();
            this.sections.AddRange(sectionnames.Reverse());
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (this.sections.Count == 0)
                this.sections.AddRange(new string[] { "RH1", "RH2"});

            this.InitSections();
        }

        private void InitSections()
        {
            this.Height = (int)this.ReportDefinition.PaperSize.Height;
            this.Width = (int)this.ReportDefinition.PaperSize.Width;

            Toolbox tb = this.MainForm.Toolbox;
            if (tb == null || tb.IsDisposed)
                tb = new Toolbox();

            this.SuspendLayout();

            foreach (string section in this.sections)
            {
                var panel = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 100,
                    BackColor = Color.Transparent
                };

                var btn = new Label { Dock = DockStyle.Left, Text = section, BackColor = Color.LightBlue };
                btn.MouseClick += Btn_MouseClick;

                panel.Controls.Add(btn);

                var pDesignerCore1 = new pF.pDesigner.pDesigner(this.MainForm.PropertyWindow);
                pDesignerCore1.BackColor = Color.Transparent;
                pDesignerCore1.Parent = panel;
                (pDesignerCore1 as IpDesigner).Toolbox = tb.listBox1;
                (pDesignerCore1 as IpDesigner).AddDesignSurface<EbReportPanel>(600, 100, AlignmentModeEnum.SnapLines, new Size(1, 1));

                this.Controls.Add(panel);
                this.Controls.Add(new Splitter { Dock = DockStyle.Top, BackColor = Color.DarkBlue, Width = 1 });
            }

            this.ResumeLayout(true);
        }

        private Label _justNowClickedSectionButton = null;

        private void Btn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _justNowClickedSectionButton = sender as Label;

                var item1 = new MenuItem("Insert Section Above");
                var item2 = new MenuItem("Delete Section");
                item1.Click += Item1_Click;
                item2.Click += Item2_Click;

                (sender as Label).ContextMenu = new ContextMenu(new MenuItem[] { item1, item2 });
            }
        }

        private void Item2_Click(object sender, EventArgs e)
        {
            if (_justNowClickedSectionButton.Text == "RH")
            {
                var _childUC = new ReportDesignerUserControl(this.ReportDefinition, new string[] { "RH1", "RH2" });
                _childUC.Dock = DockStyle.Fill;
                _childUC.MainForm = this.MainForm;
                _justNowClickedSectionButton.Parent.Controls.RemoveAt(1);
                _justNowClickedSectionButton.Parent.Controls.Add(new Panel { Height = _justNowClickedSectionButton.Height });
                _justNowClickedSectionButton.Parent.Controls[1].Controls.Add(_childUC);
            }
        }

        private void Item1_Click(object sender, EventArgs e)
        {
            
        }

        protected override void OnResize(EventArgs e)
        {
            this.SuspendLayout();
            base.OnResize(e);
            this.ResumeLayout(true);
        }
    }
}
