using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressBase.Objects;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    public class EbComboBoxControl : System.Windows.Forms.ComboBox, IEbControl
    {
        public EbControl EbControl { get; set; }

        public EbComboBoxControl()
        {
            this.EbControl = new EbComboBox(this);
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

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControl serialized_ctrl)
        {
            this.EbControl = serialized_ctrl;
            this.EbControl.Parent = this;
            this.DoDesignerRefresh();
        }

        public void DoDesignerRefresh()
        {
            if (!string.IsNullOrEmpty((this.EbControl as EbComboBox).Text))
                this.Text = (this.EbControl as EbComboBox).Text;
            if ((this.EbControl as EbComboBox).Value > 0)
                this.SelectedValue = (this.EbControl as EbComboBox).Value;

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
    }
}

