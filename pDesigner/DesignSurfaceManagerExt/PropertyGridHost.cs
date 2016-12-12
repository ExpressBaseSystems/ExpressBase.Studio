namespace pF.DesignSurfaceManagerExt {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.ComponentModel.Design;
    using System.Collections;
    using System.Diagnostics;

    using pF.DesignSurfaceManagerExt;


public partial class PropertyGridHost : UserControl {
    private const string _Name_ = "PropertyGridHost";


    //- used to suppress events, 
    //- set it to True before changing a property that will cause an event to be raised
    private bool _bSuppressEvents = false;

    private DesignSurfaceManagerExt SurfaceManager { get;  set;}
    public ComboBox ComboBox { get{return pgrdComboBox;} }
	public PropertyGrid PropertyGrid{ get{return pgrdPropertyGrid;} }
	public object SelectedObject{
		get	{ return pgrdPropertyGrid.SelectedObject;}
		set	{ pgrdPropertyGrid.SelectedObject = value;}
	}
    
    //- ctor
    public PropertyGridHost(DesignSurfaceManagerExt surfaceManager) {
        const string _signature_ = _Name_ + @"::ctor()";

        InitializeComponent();
        this.Dock = DockStyle.Fill;

        //- the surface manager strictly tied with PropertyGridHost
        if( null == surfaceManager )
            throw new ArgumentNullException( "surfaceManager", _signature_ + " - Exception: invalid argument (null)!" );

        SurfaceManager = surfaceManager;

        pgrdPropertyGrid.ToolbarVisible = true;
        pgrdPropertyGrid.HelpVisible = true;


        //- the ComboBox is an OBSERVER of PropertyGridHost event: SelectedObjectsChanged
        //- everytime someone select a new object inside the PropertyGridHost
        //-                     |
        //-                     +---> the event PropertyGridHost.SelectedObjectsChanged is fired
        //-                                                                              |
        //-                           the ReloadComboBox() method is called <------------+
        //- 
        //-
        //-
        //- 
        pgrdPropertyGrid.SelectedObjectsChanged += ( object sender, System.EventArgs e ) =>
        {
            ReloadComboBox();
        };


        //- the PropertyGridHost is an OBSERVER of ComboBox event: SelectedIndexChanged
        //- everytime someone select a new object inside the ComboBox
        //-                     |
        //-                     +---> the event ComboBox.SelectedIndexChanged is fired
        //-                                                                         |
        //-     the OrientPropertyGridTowardsObject() method is called <------------+
        //- 
        //-
        pgrdComboBox.SelectedIndexChanged += ( object sender, System.EventArgs e )=>
        {
            if( _bSuppressEvents )
                return;
            OrientPropertyGridTowardsObject();
        };
    }


    private string TranslateComponentToName( Component comp ) {
        string sType = comp.GetType().ToString();
        if( string.IsNullOrEmpty( sType ) )
            return string.Empty;
        if( string.IsNullOrEmpty( comp.Site.Name ) )
            return string.Empty;

        sType = sType.Substring( sType.LastIndexOf( "." ) + 1 );
        return String.Format( "({0}) {1}", sType,comp.Site.Name );
    }


    //- rely on SurfaceManager.ActiveDesignSurface
    //- which effectively points to ACTIVE DesignSurface
    private void OrientPropertyGridTowardsObject() {
        //- IDesignerEventService provides a global eventing mechanism for designer events. With this mechanism,
        //- an application is informed when a designer becomes active. The service provides a collection of
        //- designers and a single place where global objects, such as the Properties window, can monitor selection
        //- change events.
        IDesignerEventService des = (IDesignerEventService) SurfaceManager.GetService( typeof( IDesignerEventService ) );
        if( null == des )
            return;
        IDesignerHost host = des.ActiveDesigner;


        //- get the ISelectionService from the active Designsurface
		//- and if we are not able to get it then it'sType better to exit
        ISelectionService iSel = host.GetService( typeof( ISelectionService ) ) as ISelectionService;
		if (iSel == null)
			return;

		//- get the name of the control selected from the comboBox
		//- and if we are not able to get it then it's better to exit
		string sName = pgrdComboBox.SelectedItem.ToString();
		if (string.IsNullOrEmpty(sName))
			return;


        //- save the collection of objects currently selected
      

		//- loop through the controls inside the current Designsurface
		//- if we find the one selected into the comboBox then 
		//- use the ISelectionService to select it 
		//- (and this will select it into the PropertyGridHost)
        ComponentCollection ctrlsExisting = host.Container.Components;
        Debug.Assert( 0 != ctrlsExisting.Count );
        foreach( Component comp in ctrlsExisting ) {
            string sItemText = TranslateComponentToName( comp );
			if ( sName == sItemText){
                Component[] arr = { comp };
				//- ISelectionService in action...
                iSel.SetSelectedComponents( arr );
                //this.SelectedObject = arr[0];
                break;
			}
		}//end_foreach

	}


    //- realy on SurfaceManager.ActiveDesignSurface
    //- which effectively points to ACTIVE DesignSurface
    public void ReloadComboBox() {
        _bSuppressEvents = true;
        try {
            //- IDesignerEventService provides a global eventing mechanism for designer events. With this mechanism,
            //- an application is informed when a designer becomes active. The service provides a collection of
            //- designers and a single place where global objects, such as the Properties window, can monitor selection
            //- change events.
            IDesignerEventService des = (IDesignerEventService) SurfaceManager.GetService( typeof( IDesignerEventService ) );
            if( null == des )
                return;
            IDesignerHost host = des.ActiveDesigner;


            object selectedObj = pgrdPropertyGrid.SelectedObject;
            if( null == selectedObj )
                return; //- don't reload at all


            //- get the name of the control selected from the comboBox
            //- and if we are not able to get it then it's better to exit
            string sName = string.Empty;
            if( selectedObj is Form ) {
                sName = ((Form) selectedObj).Name;
            }
            else if( selectedObj is Control ) {
                sName = ((Control) selectedObj).Site.Name;
            }
            if( string.IsNullOrEmpty( sName ) )
                return;



            //- prepare the data for reloading the combobox (begin)
            List<object> ctrlsToAdd = new List<object>();
            string pgrdComboBox_Text = string.Empty;
            try {
                ComponentCollection ctrlsExisting = host.Container.Components;
                Debug.Assert( 0 != ctrlsExisting.Count );


                foreach( Component comp in ctrlsExisting ) {
                    string sItemText = TranslateComponentToName( comp );
                    ctrlsToAdd.Add( sItemText );
                    if( sName == comp.Site.Name )
                        pgrdComboBox_Text = sItemText;
                }//end_foreach
            }
            catch( Exception ) {
                return; //- (rollback)
            }
            //- update the combobox (commit)
            pgrdComboBox.Items.Clear();
            pgrdComboBox.Items.AddRange( ctrlsToAdd.ToArray() );
            pgrdComboBox.Text = pgrdComboBox_Text;
        }
        finally {
            _bSuppressEvents = false;
        }
    }


    //- collapse the single GridItem 
    //- the one which has label equal to param "sGridItemLabel"
    public GridItem CollapseGridItem( string sGridItemLabel ) {
        return CollapseExpandGridItem(sGridItemLabel, false);
    }
    //- expand the single GridItem 
    //- the one which has label equal to param "sGridItemLabel"
    public GridItem ExpandGridItem( string sGridItemLabel ) {
        return CollapseExpandGridItem(sGridItemLabel, true);
    }

    private GridItem CollapseExpandGridItem( string sGridItemLabel, bool bExpanded ) {
        //- retrieve the root GridItem
        GridItem root = this.PropertyGrid.SelectedGridItem;
        if( null == root )
            return null;

        while( null != root.Parent )
            root = root.Parent;
       
        if( null == root ) 
            return null;

        //- and begin to search from the root
        foreach( GridItem g in root.GridItems ) {
            if( g.Label == sGridItemLabel ) {
                if( g.Expandable )
                    g.Expanded = bExpanded;
                return g;
            }//end_if
        }//end_for
        return null;
    }
      
       
}//end_class
}//end_namespace