using ExpressBase.Objects;
using ExpressBase.Studio.Controls;
using ExpressBase.Studio.DesignerForms;
using pF.DesignSurfaceExt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressBase.Studio.ControlContainers
{
    public class EbReportPanel: Form, IEbControlContainer
    {
        public EbControlContainer EbControlContainer { get; set; }

        internal ReportDesignerUserControl ReportDesignerUserControl { get; set; }

        public EbReportPanel()
        {
            this.EbControlContainer = new EbForm(this);
            this.Dock = DockStyle.Fill;
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = System.Drawing.Color.Transparent;
        }

        //required for serialization
        public void BeforeSerialization()
        {
            this.EbControlContainer.TargetType = this.GetType().FullName;
            //this.EbControlContainer.Controls = new List<EbControl>();
            //foreach (IEbControl e in this.Controls)
            //{
            //    e.BeforeSerialization();
            //    this.EbControlContainer.Controls.Add(e.EbControl);
            //}
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbControlContainer == null)
                this.EbControlContainer = new EbForm();

            this.Dock = DockStyle.Fill;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            this.UpdateControls();
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            this.UpdateControls();
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

        protected override void OnResize(EventArgs e)
        {
            this.SuspendLayout();
            base.OnResize(e);
            this.ResumeLayout(true);
        }

        private void UpdateControls()
        {
            if (this.EbControlContainer.Controls == null)
                this.EbControlContainer.Controls = new List<EbControl>();
            else
                this.EbControlContainer.Controls.Clear();

            foreach (IEbControl e in this.Controls)
            {
                e.BeforeSerialization();
                this.EbControlContainer.Controls.Add(e.EbControl);
            }
        }
    }
}
