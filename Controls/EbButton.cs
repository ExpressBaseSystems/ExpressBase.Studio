using System;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    public class EbButtonControl : Button, IEbControl
    {
        public EbObject EbObject { get; set; }

        public EbButtonControl() { }

        //required
        public void BeforeSerialization()
        {
            this.EbObject.Size = this.Size;
            this.EbObject.Location = this.Location;
            this.EbObject.Dock = this.Dock;
            this.EbObject.TargetType = this.GetType().FullName;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbObject == null)
                this.EbObject = new EbButton();
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbObject serialized_ctrl)
        {
            this.EbObject = serialized_ctrl;
            this.Name = serialized_ctrl.Name;
            this.Size = serialized_ctrl.Size;
            this.Location = serialized_ctrl.Location;
            this.Dock = serialized_ctrl.Dock;
            this.Text = serialized_ctrl.Label;
        }

        public void DoDesignerRefresh()
        {
            this.Name = this.EbObject.Name;
            this.Size = this.EbObject.Size;
            this.Location = this.EbObject.Location;
            this.Dock = this.EbObject.Dock;
            this.Text = this.EbObject.Label;
        }
    }
}
