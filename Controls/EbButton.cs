using ExpressBase.UI;
using System;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    public class EbButtonControl : Button, IEbControl
    {
        public EbControl EbControl { get; set; }

        public EbButtonControl() { }

        //required
        public void BeforeSerialization()
        {
            this.EbControl.TargetType = this.GetType().FullName;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbControl == null)
                this.EbControl = new EbButton();
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
