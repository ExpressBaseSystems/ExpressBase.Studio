namespace pF.pDesigner {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.ComponentModel.Design;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing.Design;

    using DesignSurfaceExt;
    using DesignSurfaceManagerExt;


    public enum AlignmentModeEnum : int { SnapLines = 0, Grid, GridWithoutSnapping, NoGuides };


    //- Interface used to
    //-     * hosts: Toolbox;DesignSurfaces;PropertyGrid
    //-     * add/remove DesignSurfaces
    //-     * perform editing actions on active DesignSurface
    //-
    public interface IpDesigner {

        //- controls accessing section  -----------------------------------------------------------
        //-     +-------------+-----------------------------+-----------+
        //-     |toolboxItem1 | ____ ____ ____              |           |
        //-     |toolboxItem2 ||____|____|____|___________  +-----------+
        //-     |toolboxItem3 ||                          | |     |     |
        //-     |             ||                          | |     |     |
        //-     |  TOOLBOX    ||      DESIGNSURFACES      | | PROPERTY  |
        //-     |             ||                          | |   GRID    |
        //-     |             ||__________________________| |     |     |
        //-     +-------------+-----------------------------+-----------+
        ListBox Toolbox { get; set; }                       //- TOOLBOX
        //TabControl TabControlHostingDesignSurfaces { get; } //- DESIGNSURFACES HOST
        PropertyGridHost PropertyGridHost { get; }          //- PROPERTYGRID






        //- DesignSurfaces management section -----------------------------------------------------
        DesignSurfaceExt2 ActiveDesignSurface { get; }
        //- Create the DesignSurface and the rootComponent (a .NET Control)
        //- using IDesignSurfaceExt.CreateRootComponent() 
        //- if the alignmentMode doesn't use the GRID, then the gridSize param is ignored
        //- Note:
        //-     the generics param is used to know which type of control to use as RootComponent
        //-     TT is requested to be derived from .NET Control class 
        DesignSurfaceExt2 AddDesignSurface<TT>(
                                               int startingFormWidth, int startingFormHeight,
                                               AlignmentModeEnum alignmentMode, Size gridSize
                                              ) where TT : Control;

        DesignSurfaceExt2 AddDesignSurface4Web<TT>(
                                               int startingFormWidth, int startingFormHeight,
                                               AlignmentModeEnum alignmentMode, Size gridSize
                                              ) where TT : System.Web.UI.Control;

        void RemoveDesignSurface ( DesignSurfaceExt2 activeSurface );







        //- Editing section  ----------------------------------------------------------------------
        void UndoOnDesignSurface();
        void RedoOnDesignSurface();
        void CutOnDesignSurface();
        void CopyOnDesignSurface();
        void PasteOnDesignSurface();
        void DeleteOnDesignSurface();
        void SwitchTabOrder();

    }//end_interface
}//end_namespace
