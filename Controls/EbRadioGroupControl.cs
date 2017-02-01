using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressBase.Objects;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace ExpressBase.Studio.Controls
{
    public class EbRadioGroupControl : GroupBox, IEbControl
    {
        public EbControl EbControl { get; set; }

        public EbRadioGroupControl()
        {
            this.EbControl = new EbRadioGroup();
        }

        //required
        public void BeforeSerialization()
        {
            this.EbControl.TargetType = this.GetType().FullName;

            this.EbControl.Left = this.Location.X;
            this.EbControl.Top = this.Location.Y;
            this.EbControl.Height = this.Size.Height;
            this.EbControl.Width = this.Size.Width;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbControl == null)
                this.EbControl = new EbRadioGroup();
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControl serialized_ctrl)
        {
            this.EbControl = serialized_ctrl;
            this.Name = serialized_ctrl.Name;
            this.Text = serialized_ctrl.Label;

            if (this.EbControl.CellPositionColumn > 0 && this.EbControl.CellPositionRow > 0)
                this.Dock = DockStyle.Fill;
            else
            {
                this.Location = new System.Drawing.Point(this.EbControl.Left, this.EbControl.Top);
                this.Size = new System.Drawing.Size(this.EbControl.Width, this.EbControl.Height);
            }
        }

        public void DoDesignerRefresh()
        {
            this.Name = this.EbControl.Name;
            this.Dock = DockStyle.Fill;
            this.Text = this.EbControl.Label;
        }
    }
}
