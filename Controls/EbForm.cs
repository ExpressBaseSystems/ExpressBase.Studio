using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    [ProtoBuf.ProtoContract]
    public class EbForm : System.Windows.Forms.Form
    {
        [ProtoBuf.ProtoMember(1)]
        new public string Name { get; set; }

        [ProtoBuf.ProtoMember(2)]
        [Browsable(false)]
        public IEbControl[] Controls2
        {
            get
            {
                IEbControl[] ca = new IEbControl[this.Controls.Count];
                this.Controls.CopyTo(ca, 0);
                return ca;
            }
            set
            {
                foreach (IEbControl c in value)
                    this.Controls.Add((Control)c);
            }
        }
    }
}
