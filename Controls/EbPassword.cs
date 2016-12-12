using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressBase.Studio.Controls
{
    [ProtoBuf.ProtoContract]
    public class EbPassword : System.Windows.Forms.TextBox, IEbControl
    {
        public EbPassword() : base()
        {
            base.PasswordChar = '*';
        }
    }
}
