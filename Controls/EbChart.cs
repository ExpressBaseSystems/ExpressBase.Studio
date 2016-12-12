using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressBase.Studio.Controls
{
    [ProtoBuf.ProtoContract]
    public class EB_Chart : System.Windows.Forms.DataVisualization.Charting.Chart, IEbControl
    {
    }
}
