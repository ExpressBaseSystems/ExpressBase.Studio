using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    [ProtoBuf.ProtoContract]
    public class EbFormControl : System.Windows.Forms.Form, IEbControl
    {
        [ProtoBuf.ProtoMember(1)]
        public EbObject EbObject { get; set; }

        [ProtoBuf.ProtoMember(2)]
        [Browsable(false)]
        public IEbControl[] Controls2 { get; set; }

        [Obsolete("For protobuf-net serialization purposes only")]
        public EbFormControl() { }

        [ProtoBuf.ProtoBeforeSerialization]
        private void BeforeSerialization()
        {
            this.Controls2 = new IEbControl[this.Controls.Count];
            this.Controls.CopyTo(this.Controls2, 0);
        }

        [ProtoBuf.ProtoAfterDeserialization]
        private void AfterDeserialization()
        {
            if (this.Controls2 == null)
                this.Controls2 = new IEbControl[0];
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbObject == null)
                this.EbObject = new EbButton();
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, IEbControl serialized_ctrl)
        {
            foreach (IEbControl c in this.Controls2)
            {
                var ctrl = designer.ActiveDesignSurface.CreateControl(c.GetType(), c.EbObject.Size, c.EbObject.Location) as System.Windows.Forms.Control;
                (ctrl as IEbControl).DoDesignerLayout(designer, c);
            }
        }

        public void DoDesignerRefresh() { }
    }
}
