using System;
using pF.pDesigner;

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

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, IEbControl serialized_ctrl)
        {
            throw new NotImplementedException();
        }

        public void DoDesignerRefresh() { }

        public void BeforeSerialization()
        {
            //throw new NotImplementedException();
        }

        public void DoDesignerLayout(IpDesigner designer, EbObject serialized_ctrl)
        {
            throw new NotImplementedException();
        }
    }
}
