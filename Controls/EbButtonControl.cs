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
            this.EbControl = new EbButton(this);
        }

        public void BeforeSerialization()
        {
            if (this.EbControl.Parent == null)
                this.EbControl.Parent = this;

            this.EbControl.TargetType = this.GetType().FullName;

            this.EbControl.Left = this.Location.X;
            this.EbControl.Top = this.Location.Y;
            this.EbControl.Height = this.Size.Height;
            this.EbControl.Width = this.Size.Width;
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControl serialized_ctrl)
        {
            this.EbControl = serialized_ctrl;
            this.DoDesignerRefresh();
        }

        public void DoDesignerRefresh()
        {
            this.Text = (!string.IsNullOrEmpty(this.EbControl.Label)) ? this.EbControl.Label : this.EbControl.Name;

            this.Name = this.EbControl.Name;
            this.Font = this.EbControl.Font;
            if (this.EbControl.CellPositionColumn == 0 && this.EbControl.CellPositionRow == 0)
            {
                this.Location = new System.Drawing.Point(this.EbControl.Left, this.EbControl.Top);
                this.Size = new System.Drawing.Size(this.EbControl.Width, this.EbControl.Height);
            }
            else
                this.Dock = DockStyle.Fill;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", this.Name, this.EbControl.GetType().Name); 
        }
    }
}
