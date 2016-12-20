using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressBase.Studio.Controls
{
    [ProtoBuf.ProtoContract]
    public class EbPasswordControl : System.Windows.Forms.TextBox, IEbControl
    {
        public EbObject EbObject { get; set; }

        [ProtoBuf.ProtoMember(2)]
        [Browsable(false)]
        public IEbControl[] Controls2 { get; set; }

        //for protobuf-net
        public EbPasswordControl() : base()
        {
            base.PasswordChar = '*';
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, IEbControl serialized_ctrl)
        {
            //var ctrl = designer.ActiveDesignSurface.CreateControl(this.GetType(), this.Size, this.Location) as System.Windows.Forms.Control;
            //ctrl.Name = this.Name;
        }
    }
}
