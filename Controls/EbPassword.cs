using ExpressBase.Objects;
using System;

namespace ExpressBase.Studio.Controls
{
    public class EbPasswordControl : System.Windows.Forms.TextBox, IEbControl
    {
        public EbControl EbControl { get; set; }

        //for protobuf-net
        public EbPasswordControl() : base()
        {
            base.PasswordChar = '*';
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControl serialized_ctrl)
        {
            throw new NotImplementedException();
        }

        public void DoDesignerRefresh() { }

        public void BeforeSerialization()
        {
            throw new NotImplementedException();
        }
    }
}
