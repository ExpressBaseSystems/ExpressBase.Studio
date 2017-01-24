using ExpressBase.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    public class EbSplitter: Splitter, IEbControl
    {
        public EbControl EbControl { get; set; }

        public EbSplitter()
        {
        }

        //required for serialization
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

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControl serialized_ctrl)
        {
        }

        public void DoDesignerRefresh() { }
    }
}
