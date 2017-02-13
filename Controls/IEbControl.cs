using ExpressBase.Objects;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    public interface IEbControl
    {
        EbControl EbControl { get; set; }

        void BeforeSerialization();

        void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControl serialized_ctrl);

        void DoDesignerRefresh();

        string ToString();
    }

    public interface IEbControlContainer
    {
        EbControlContainer EbControlContainer { get; set; }

        void BeforeSerialization();

        void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControlContainer serialized_ctrl);

        void DoDesignerRefresh();
    }
}