using ExpressBase.Data;
using ExpressBase.Objects;
using ExpressBase.ServiceStack;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
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

            this.PopulateDataSources();

            this.ReportDefinition = new EbReportDefinition();
        }

        private void CmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPageSize.SelectedItem != null)
                ReportDefinition.PaperSize = cmbPageSize.SelectedItem as EbReportPaperSize;
        }

        private void btnOpenReportDesigner_Click(object sender, EventArgs e)
        {
            if (cmbEbDataSource.SelectedItem == null)
                return;

            this.Cursor = Cursors.WaitCursor;
            this.ReportDefinition.EbDataSourceId = (cmbEbDataSource.SelectedItem as EbObjectWrapper).Id;

            JsonServiceClient client = new JsonServiceClient("http://localhost:53125/");
            var resp = client.Get<DataSourceColumnsResponse>(new DataSourceColumnsRequest { Id = this.ReportDefinition.EbDataSourceId });
            //var resp = client.Get<DataSourceColumnsResponse>(string.Format("http://localhost:53125/ds/columns/{0}", this.ReportDefinition.EbDataSourceId));
            this.ReportDefinition.ColumnColletion = resp.Columns;

            ReportDesignerForm pD = new ReportDesignerForm(this.ReportDefinition);
            pD.MainForm = this.Owner as MainForm;
            pD.Show((this.Owner as MainForm).DockPanel);
            this.Cursor = Cursors.Default;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopulateDataSources()
        {
            IServiceClient client = new JsonServiceClient("http://localhost:53125/").WithCache();
            var fr = client.Get<EbObjectResponse>("http://localhost:53125/ebo?format=json");

            cmbEbDataSource.DisplayMember = "Name";
            cmbEbDataSource.ValueMember = "Id";
            cmbEbDataSource.BeginUpdate();

            foreach (EbObjectWrapper dr in fr.Data)
            {
                if (dr.EbObjectType == EbObjectType.DataSource)
                    cmbEbDataSource.Items.Add(dr);
            }

            cmbEbDataSource.EndUpdate();
        }
    }

    public class DataSourceColumnsRequest : IReturn<DataSourceColumnsResponse>
    {
        public int Id { get; set; }

        public string SearchText { get; set; }

        public string OrderByDirection { get; set; }

        public string SelectedColumnName { get; set; }
    }

    public class DataSourceColumnsResponse
    {
        [DataMember(Order = 1)]
        public ColumnColletion Columns { get; set; }
    }
}
