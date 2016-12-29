using System;
using pF.pDesigner;
using ExpressBase.UI;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    public class EbDataGridViewControl : System.Windows.Forms.DataGridView, IEbControl
    {
        public EbControl EbControl { get; set; }

        public EbDataGridViewControl() { }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbControl == null)
                this.EbControl = new EbDataGridView();

            this.EbControl.Name = this.Name;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        //required
        public void BeforeSerialization()
        {
            this.EbControl.TargetType = this.GetType().FullName;
            this.EbControl.Name = this.Name;
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControl serialized_ctrl)
        {
            this.EbControl = serialized_ctrl;
            this.Name = serialized_ctrl.Name;
            this.Dock = DockStyle.Fill;
            this.Text = serialized_ctrl.Label;
        }

        public void DoDesignerRefresh()
        {
            this.Name = this.EbControl.Name;
            this.Dock = DockStyle.Fill;
            this.Text = this.EbControl.Label;
        }
    }
}
