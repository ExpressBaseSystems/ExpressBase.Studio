using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExpressBase.Objects;
using pF.pDesigner;

namespace ExpressBase.Studio.Controls
{
    public class EbSplitContainer : SplitContainer, IEbControl
    {
        public EbControl EbControl { get; set; }

        public void BeforeSerialization()
        {
            this.EbControl.TargetType = this.GetType().FullName;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbControl == null)
                this.EbControl = new EbButton();
        }

        public void DoDesignerLayout(IpDesigner designer, EbControl serialized_ctrl)
        {
            
        }

        public void DoDesignerRefresh()
        {
            
        }
    }
}
