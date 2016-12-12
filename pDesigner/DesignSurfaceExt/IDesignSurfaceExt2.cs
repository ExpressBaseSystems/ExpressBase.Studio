using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;

namespace pF.DesignSurfaceExt {
    public interface IDesignSurfaceExt2 : IDesignSurfaceExt{
        //- Get the IDesignerHost of the .NET 2.0 DesignSurface
        ToolboxServiceImp GetIToolboxService();
        void EnableDragandDrop();
    }
}
