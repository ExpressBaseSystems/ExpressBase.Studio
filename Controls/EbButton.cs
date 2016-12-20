using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static pF.DesignSurfaceExt.DesignSurfaceExt;

namespace ExpressBase.Studio.Controls
{
    [ProtoBuf.ProtoContract()]
    public class EbButtonControl : Button, IEbControl
    {
        [ProtoBuf.ProtoMember(1)]
        public EbObject EbObject { get; set; }

        [ProtoBuf.ProtoMember(2)]
        [Browsable(false)]
        public IEbControl[] Controls2 { get; set; }

        [Obsolete("For protobuf-net serialization purposes only")]
        public EbButtonControl() { }

        [ProtoBuf.ProtoBeforeSerialization]
        private void BeforeSerialization()
        {
            this.EbObject.Size = this.Size;
            this.EbObject.Location = this.Location;
            this.EbObject.Dock = this.Dock;
        }

        [ProtoBuf.ProtoAfterDeserialization]
        private void AfterDeserialization()
        {
            if (this.EbObject == null)
                this.EbObject = new EbButton();

            //this.Name = this.EbObject.Name;
            //this.Size = this.EbObject.Size;
            //this.Location = this.EbObject.Location;
            //this.Dock = this.EbObject.Dock;
            //this.Visible = true;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbObject == null)
                this.EbObject = new EbButton();
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, IEbControl serialized_ctrl)
        {
            this.EbObject = serialized_ctrl.EbObject;
            this.Controls2 = serialized_ctrl.Controls2;
            this.Name = serialized_ctrl.EbObject.Name;
            this.Size = serialized_ctrl.EbObject.Size;
            this.Location = serialized_ctrl.EbObject.Location;
            this.Dock = serialized_ctrl.EbObject.Dock;
        }
    }
}
