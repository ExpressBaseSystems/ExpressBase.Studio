using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    [ProtoBuf.ProtoContract]
    [ProtoBuf.ProtoInclude(1, typeof(EbButtonControl))]
    [ProtoBuf.ProtoInclude(2, typeof(EbTableLayoutPanel))]
    public interface IEbControl
    {
        EbObject EbObject { get; set; }

        IEbControl[] Controls2 { get; set; }

        void DoDesignerLayout(pF.pDesigner.IpDesigner designer, IEbControl serialized_ctrl);

        void DoDesignerRefresh();
    }

    [ProtoBuf.ProtoContract]
    [ProtoBuf.ProtoInclude(1000, typeof(EbButton))]
    [ProtoBuf.ProtoInclude(1001, typeof(EbTableLayout))]
    public abstract class EbObject
    {
        [ProtoBuf.ProtoMember(1)]
        public string Name { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public string Label { get; set; }

        [ProtoBuf.ProtoMember(3)]
        public string HelpText { get; set; }

        [ProtoBuf.ProtoMember(4)]
        public string ToolTipText { get; set; }

        [ProtoBuf.ProtoMember(5)]
        public DockStyle Dock { get; set; }

        [ProtoBuf.ProtoMember(6)]
        public int CellPositionRow { get; set; }

        [ProtoBuf.ProtoMember(7)]
        public int CellPositionColumn { get; set; }

        public Size Size { get; set; }

        [ProtoBuf.ProtoMember(8)]
        [Browsable(false)]
        public string SizeSerialized
        {
            get { return Size.ToString(); }
            set
            {
                string[] coords = value.Replace("{Width=", string.Empty).Replace("Height=", string.Empty).Replace("}", string.Empty).Split(',');
                Size = new Size(int.Parse(coords[0]), int.Parse(coords[1]));
            }
        }

        public Point Location { get; set; }

        [ProtoBuf.ProtoMember(9)]
        [Browsable(false)]
        public string LocationSerialized
        {
            get { return Location.ToString(); }
            set
            {
                string[] coords = value.Replace("{X=", string.Empty).Replace("Y=", string.Empty).Replace("}", string.Empty).Split(',');
                Location = new Point(int.Parse(coords[0]), int.Parse(coords[1]));
            }
        }

        [Browsable(false)]
        public IEbControl IEbControl { get; set; }

        public EbObject()
        {
            this.Dock = DockStyle.Fill;
        }

        public EbObject(IEbControl parent)
        {
            this.IEbControl = parent;
        }
    }

    [ProtoBuf.ProtoContract]
    public class EbButton : EbObject
    {
        public EbButton() { }
        public EbButton(IEbControl parent) : base(parent) { }
    }

    [ProtoBuf.ProtoContract]
    public class EbTableLayout : EbObject
    {
        [ProtoBuf.ProtoMember(1)]
        public int RowCount { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public int ColumnCount { get; set; }

        public EbTableLayout() { }
        public EbTableLayout(IEbControl parent) : base(parent) { }
    }

    [ProtoBuf.ProtoContract]
    public class EbChart : EbObject
    {
        public EbChart() { }
        public EbChart(IEbControl parent) : base(parent) { }
    }
}