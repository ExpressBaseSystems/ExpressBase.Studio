using ExpressBase.Objects;
using ExpressBase.Studio.Controls;
using pF.DesignSurfaceExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressBase.Studio.ControlContainers
{
    public class EbReportPanel: Form, IEbControlContainer
    {
        public EbControlContainer EbControlContainer { get; set; }

        public EbReportPanel()
        {
            this.EbControlContainer = new EbForm();
            this.Dock = DockStyle.Fill;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        //required for serialization
        public void BeforeSerialization()
        {
            this.EbControlContainer.TargetType = this.GetType().FullName;
            this.EbControlContainer.Controls = new List<EbControl>();
            foreach (IEbControl e in this.Controls)
            {
                e.BeforeSerialization();
                this.EbControlContainer.Controls.Add(e.EbControl);
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbControlContainer == null)
                this.EbControlContainer = new EbForm();

            this.Dock = DockStyle.Fill;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControlContainer serialized_ctrl)
        {
            ((designer.ActiveDesignSurface as IDesignSurfaceExt).RootComponent as EbReportPanel).EbControlContainer = serialized_ctrl;
            foreach (EbControl c in serialized_ctrl.Controls)
            {
                var ctrl = designer.ActiveDesignSurface.CreateControl(Type.GetType(c.TargetType)) as System.Windows.Forms.Control;
                (ctrl as IEbControl).DoDesignerLayout(designer, c);
            }
        }

        public void DoDesignerRefresh() { }
    }
}
