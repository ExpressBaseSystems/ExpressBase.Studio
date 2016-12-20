using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressBase.Studio.Controls
{
    [ProtoBuf.ProtoContract]
    public class EbChartControl : System.Windows.Forms.DataVisualization.Charting.Chart, IEbControl
    {
        public EbObject EbObject { get; set; }

        [ProtoBuf.ProtoMember(2)]
        [Browsable(false)]
        public IEbControl[] Controls2 { get; set; }

        //for protobuf-net
        public EbChartControl() { }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, IEbControl serialized_ctrl)
        {
            //var ctrl = designer.ActiveDesignSurface.CreateControl(this.GetType(), this.Size, this.Location) as System.Windows.Forms.Control;
            //ctrl.Name = this.Name;
        }
    }
}
