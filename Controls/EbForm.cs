using ExpressBase.UI;
using pF.DesignSurfaceExt;
using System;
using System.Collections.Generic;

namespace ExpressBase.Studio.Controls
{
    public class EbFormControl : System.Windows.Forms.Form, IEbControl
    {
        public EbControl EbControl { get; set; }

        public EbFormControl() { }

        //required for serialization
        public void BeforeSerialization()
        {
            this.EbControl.TargetType = this.GetType().FullName;
            this.EbControl.Controls = new List<EbControl>();
            foreach (IEbControl e in this.Controls)
            {
                e.BeforeSerialization();
                this.EbControl.Controls.Add(e.EbControl);
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbControl == null)
                this.EbControl = new EbButton();
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControl serialized_ctrl)
        {
            ((designer.ActiveDesignSurface as IDesignSurfaceExt).RootComponent as EbFormControl).EbControl = serialized_ctrl;
            foreach (EbControl c in serialized_ctrl.Controls)
            {
                var ctrl = designer.ActiveDesignSurface.CreateControl(Type.GetType(c.TargetType)) as System.Windows.Forms.Control;
                (ctrl as IEbControl).DoDesignerLayout(designer, c);
            }
        }

        public void DoDesignerRefresh() { }
    }
}
