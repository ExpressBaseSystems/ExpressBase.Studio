using ExpressBase.Objects;
using System;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    public class EbButtonControl : Button, IEbControl
    {
        public EbControl EbControl { get; set; }

        public EbButtonControl()
        {
            this.EbControl = new EbButton();
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
                this.EbControl = new EbButton();
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
            //this.Dock = DockStyle.Fill;
            this.Text = this.EbControl.Label;
            this.Font = this.EbControl.Font;
            //this.BackColor = this.EbControl.BackColor;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", this.Name, this.EbControl.GetType().Name); 
        }
    }
}
