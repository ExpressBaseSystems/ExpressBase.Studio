using System;
using System.Collections.Generic;

namespace ExpressBase.Studio.Controls
{
    public class EbFormControl : System.Windows.Forms.Form, IEbControl
    {
        public EbObject EbObject { get; set; }

        public EbFormControl() { }

        //required for serialization
        public void BeforeSerialization()
        {
            this.EbObject.TargetType = this.GetType().FullName;
            this.EbObject.Controls = new List<EbObject>();
            foreach (IEbControl e in this.Controls)
            {
                e.BeforeSerialization();
                this.EbObject.Controls.Add(e.EbObject);
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbObject == null)
                this.EbObject = new EbButton();
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbObject serialized_ctrl)
        {
            foreach (EbObject c in serialized_ctrl.Controls)
            {
                var ctrl = designer.ActiveDesignSurface.CreateControl(Type.GetType(c.TargetType), c.Size, c.Location) as System.Windows.Forms.Control;
                (ctrl as IEbControl).DoDesignerLayout(designer, c);
            }
        }

        public void DoDesignerRefresh() { }
    }
}
