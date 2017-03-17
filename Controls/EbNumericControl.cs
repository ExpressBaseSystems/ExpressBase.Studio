using ExpressBase.Objects;
using System;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    public class EbNumericControl : NumericUpDown, IEbControl
    {
        public EbControl EbControl { get; set; }

        public EbNumericControl()
        {
            this.EbControl = new EbNumeric(this);
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
            //this.Text = this.EbControl.Label; -- NEVER DO THIS, USE .Value INSTEAD
            this.Font = this.EbControl.Font;
            this.DecimalPlaces = (this.EbControl as EbNumeric).DecimalPlaces;
            this.Maximum = 9999999;
            this.Value = (this.EbControl as EbNumeric).Value;

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

