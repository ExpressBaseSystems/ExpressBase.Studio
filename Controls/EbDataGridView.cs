using System;
using pF.pDesigner;
using ExpressBase.UI;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    public class EbDataGridViewControl : System.Windows.Forms.DataGridView, IEbControl
    {
        public EbObject EbObject { get; set; }

        public EbDataGridViewControl() { }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbObject == null)
                this.EbObject = new EbDataGridView();
        }

        //required
        public void BeforeSerialization()
        {
            this.EbObject.TargetType = this.GetType().FullName;
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbObject serialized_ctrl)
        {
            this.EbObject = serialized_ctrl;
            this.Name = serialized_ctrl.Name;
            this.Dock = DockStyle.Fill;
            this.Text = serialized_ctrl.Label;
        }

        public void DoDesignerRefresh()
        {
            this.Name = this.EbObject.Name;
            this.Dock = DockStyle.Fill;
            this.Text = this.EbObject.Label;
        }
    }
}
