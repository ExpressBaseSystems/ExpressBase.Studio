using ExpressBase.Objects;
using System;

namespace ExpressBase.Studio.Controls
{
    public class EbChartControl : System.Windows.Forms.DataVisualization.Charting.Chart, IEbControl
    {
        public EbControl EbControl { get; set; }

        public EbChartControl() { }

        //required
        public void BeforeSerialization()
        {
            this.EbControl.TargetType = this.GetType().FullName;
            this.EbControl.Name = this.Name;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbControl == null)
                this.EbControl = new EbChart();

            this.EbControl.Name = this.Name;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControl serialized_ctrl)
        {
            this.EbControl = serialized_ctrl;
            this.Name = serialized_ctrl.Name;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Text = serialized_ctrl.Label;
        }

        public void DoDesignerRefresh()
        {
            this.Name = this.EbControl.Name;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Text = this.EbControl.Label;
        }
    }
}
