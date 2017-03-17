using ExpressBase.Objects;
using pF.DesignSurfaceExt;
using System;
using System.Collections.Generic;
using pF.pDesigner;

namespace ExpressBase.Studio.Controls
{
    public class EbFormControl : System.Windows.Forms.Form, IEbControlContainer
    {
        public EbControlContainer EbControlContainer { get; set; }

        public EbFormControl()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        //required for serialization
        public void BeforeSerialization()
        {
            this.EbControlContainer.TargetType = this.GetType().FullName;
            this.EbControlContainer.Controls = new List<EbControl>();
            foreach (System.Windows.Forms.Control e in this.Controls)
            {
                if (e is IEbControlContainer)
                {
                    (e as IEbControlContainer).BeforeSerialization();
                    this.EbControlContainer.Controls.Add((e as IEbControlContainer).EbControlContainer);
                }
                else if (e is IEbControl)
                {
                    (e as IEbControl).BeforeSerialization();
                    this.EbControlContainer.Controls.Add((e as IEbControl).EbControl);
                }
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbControlContainer == null)
                this.EbControlContainer = new EbForm();
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControlContainer serialized_ctrl)
        {
            ((designer.ActiveDesignSurface as IDesignSurfaceExt).RootComponent as EbFormControl).EbControlContainer = serialized_ctrl;
            foreach (EbControl c in serialized_ctrl.Controls)
            {
                var ctrl = designer.ActiveDesignSurface.CreateControl(Type.GetType(c.TargetType)) as System.Windows.Forms.Control;
                if (ctrl is IEbControlContainer)
                    (ctrl as IEbControlContainer).DoDesignerLayout(designer, c as EbControlContainer);
                else if (ctrl is IEbControl)
                    (ctrl as IEbControl).DoDesignerLayout(designer, c);
            }
        }

        public void DoDesignerRefresh() { }
    }
}
