using ExpressBase.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressBase.Studio.DesignerForms
{
    public partial class ReportSettingsForm : Form
    {
        private EbReportDefinition ReportDefinition { get; set; }

        public ReportSettingsForm()
        {
            InitializeComponent();
            cmbPageSize.SelectedIndexChanged += CmbPageSize_SelectedIndexChanged;

            cmbPageSize.BeginUpdate();

            foreach (System.Reflection.FieldInfo finfo in typeof(iTextSharp.text.PageSize).GetFields())
            {
                if (finfo.FieldType == typeof(iTextSharp.text.Rectangle))
                {
                    var rect = (iTextSharp.text.Rectangle)finfo.GetValue(null);
                    cmbPageSize.Items.Add(new EbReportPaperSize(finfo.Name, rect.Width, rect.Height));
                }
            }

            cmbPageSize.EndUpdate();

            this.ReportDefinition = new EbReportDefinition();
        }

        private void CmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPageSize.SelectedItem != null)
                ReportDefinition.PaperSize = cmbPageSize.SelectedItem as EbReportPaperSize;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReportDesignerForm pD = new ReportDesignerForm(this.ReportDefinition);
            pD.MainForm = this.Owner as MainForm;
            pD.Show((this.Owner as MainForm).DockPanel);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
