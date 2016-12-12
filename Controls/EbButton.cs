using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ExpressBase.Studio.Controls
{
    [ProtoBuf.ProtoContract]
    public class EbButton : Button, IEbControl
    {
        [ProtoBuf.ProtoMember(1)]
        public string Formula { get; set; }

        [ProtoBuf.ProtoMember(2)]
        new public string Name { get; set; }

        [ProtoBuf.ProtoMember(3)]
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

        [ProtoBuf.ProtoMember(4)]
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
        public override string Text { get; set; }

        [Browsable(false)]
        public override System.Drawing.ContentAlignment TextAlign { get; set; }
    }
}
