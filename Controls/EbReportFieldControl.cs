using ExpressBase.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    public class EbReportFieldControl: System.Windows.Forms.TextBox, IEbControl
    {
        public EbControl EbControl { get; set; }

        public EbReportFieldControl()
        {
            this.EbControl = new EbTextBox();
            this.EbControl.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
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
            this.Text = this.EbControl.Label;
            this.Font = this.EbControl.Font;
        }
    }
}
