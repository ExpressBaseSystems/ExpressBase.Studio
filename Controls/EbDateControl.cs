using ExpressBase.Objects;
using System;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    public class EbDateControl : DateTimePicker, IEbControl
    {
        public EbControl EbControl { get; set; }

        public EbDateControl()
        {
            this.EbControl = new EbDate(this);
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
            this.BeforeSerialization();
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControl serialized_ctrl)
        {
            this.EbControl = serialized_ctrl;
            this.DoDesignerRefresh();
        }

        public void DoDesignerRefresh()
        {
            this.Name = this.EbControl.Name;
            //this.Text = this.EbControl.Label;
            this.Font = this.EbControl.Font;
            //this.MinDate = (this.EbControl as EbDate).Min;
            //this.MaxDate = (this.EbControl as EbDate).Max;
            //this.Value = (this.EbControl as EbDate).Value;

            if (this.EbControl.CellPositionColumn == 0 && this.EbControl.CellPositionRow == 0)
            {
                this.EbControl.Left = this.Location.X;
                this.EbControl.Top = this.Location.Y;
                this.EbControl.Height = this.Size.Height;
                this.EbControl.Width = this.Size.Width;
            }
            else
                this.Dock = DockStyle.Fill;
        }
    }
}


