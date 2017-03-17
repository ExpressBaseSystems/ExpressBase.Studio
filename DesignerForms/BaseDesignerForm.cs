using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ExpressBase.Studio.DesignerForms
{
    public class BaseDesignerForm : DockContent
    {
        internal MainForm MainForm { get; set; }

        internal virtual ToolStrip ToolStrip { get; }

        internal virtual pF.DesignSurfaceExt.DesignSurfaceExt2 ActiveDesignSurface { get; }

        internal virtual pF.pDesigner.pDesigner DesignerCore { get; }
    }
}
