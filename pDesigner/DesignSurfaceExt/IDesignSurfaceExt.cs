﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.ComponentModel.Design.Serialization;

namespace pF.DesignSurfaceExt {
public interface IDesignSurfaceExt {

    //- perform Cut/Copy/Paste/Delete commands
    void DoAction( string command );

    //- de/activate the TabOrder facility
    void SwitchTabOrder();

    //- select the controls alignement mode
    void UseSnapLines();
    void UseGrid ( System.Drawing.Size gridSize );
    void UseGridWithoutSnapping ( System.Drawing.Size gridSize );
    void UseNoGuides();

    //- method usefull to create control without the ToolBox facility
    IComponent CreateRootComponent ( Type controlType, Size controlSize );
    IComponent CreateRootComponent( DesignerLoader loader, Size controlSize );
    Control CreateControl ( Type controlType, Size controlSize, Point controlLocation );

    //- Get the UndoEngineExtended object
    UndoEngineExt GetUndoEngineExt();

    //- Get the IDesignerHost of the .NET 2.0 DesignSurface
    IDesignerHost GetIDesignerHost();

    //- the HostControl of the .NET 2.0 DesignSurface is just a Control
    //- you can manipulate this Control just like any other WinForms Control
    //- (you can dock it and add it to another Control just to display it)
    //- Get the HostControl
    Control GetView();

}//end_interface

}//end_namespace
