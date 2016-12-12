using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    [ProtoBuf.ProtoContract]
    public class EbDataGridView : System.Windows.Forms.DataGridView, IEbControl
    {
        public EbDataGridView()
        {
        }
    }
}
