using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Diagnostics;
using ExpressBase.Objects;
using ExpressBase.Studio.Controls;
using ExpressBase.Studio.ControlContainers;
using ExpressBase.Studio.DesignerForms;

namespace pF.DesignSurfaceManagerExt
{
    public partial class PropertyGridHost : UserControl
    {
        //- set it to True before changing a property that will cause an event to be raised
        private bool _bSuppressEvents = false;

        private DesignSurfaceManagerExt SurfaceManager { get;  set;}

        public ComboBox ComboBox { get{return pgrdComboBox;} }

	    public PropertyGrid PropertyGrid{ get{return pgrdPropertyGrid;} }

	    public object SelectedObject
        {
		    get	{ return pgrdPropertyGrid.SelectedObject;}
		    set	{ pgrdPropertyGrid.SelectedObject = value;}
	    }
    
        public PropertyGridHost(DesignSurfaceManagerExt surfaceManager)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            this.SurfaceManager = surfaceManager;

            pgrdPropertyGrid.ToolbarVisible = true;
            pgrdPropertyGrid.HelpVisible = true;

            pgrdPropertyGrid.SelectedObjectsChanged += ( object sender, System.EventArgs e ) =>
            {
                ReloadComboBox();

                if (pgrdPropertyGrid.SelectedObjects.Count() == 1)
                {
                    this.SelectedObject = pgrdPropertyGrid.SelectedObject;

                    if (pgrdPropertyGrid.SelectedObject is EbControl && (pgrdPropertyGrid.SelectedObject as EbControl).Parent != null)
                        pgrdComboBox.SelectedIndex = pgrdComboBox.Items.IndexOf((pgrdPropertyGrid.SelectedObject as EbControl).Parent);
                }

                OrientPropertyGridTowardsObject();
            };

            pgrdPropertyGrid.PropertyValueChanged += (object s, PropertyValueChangedEventArgs e) =>
            {
                if (this.SelectedObject != null && pgrdComboBox.SelectedItem != null)
                {
                    if (pgrdComboBox.SelectedItem is IEbControlContainer)
                        (pgrdComboBox.SelectedItem as IEbControlContainer).DoDesignerRefresh();

                    if (pgrdComboBox.SelectedItem is IEbControl)
                        (pgrdComboBox.SelectedItem as IEbControl).DoDesignerRefresh();
                }
            };

            pgrdComboBox.SelectedIndexChanged += ( object sender, System.EventArgs e )=>
            {
                if( _bSuppressEvents )
                    return;

                if ((pgrdComboBox.Items[pgrdComboBox.SelectedIndex] is IEbControl))
                    this.SelectedObject = (pgrdComboBox.Items[pgrdComboBox.SelectedIndex] as IEbControl).EbControl;
            };
        }

        private string TranslateComponentToName( Component comp )
        {
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
        private void OrientPropertyGridTowardsObject()
        {
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
               object sName = pgrdComboBox.SelectedItem;
            //string sName = (pgrdComboBox.SelectedItem as EbControl).EbControl.Name;
            //if (string.IsNullOrEmpty(sName))
            //	return;


                //- save the collection of objects currently selected


                //- loop through the controls inside the current Designsurface
                //- if we find the one selected into the comboBox then 
                //- use the ISelectionService to select it 
                //- (and this will select it into the PropertyGridHost)
                ComponentCollection ctrlsExisting = host.Container.Components;
            Debug.Assert( 0 != ctrlsExisting.Count );
            foreach( Component comp in ctrlsExisting ) {
                string sItemText = TranslateComponentToName( comp );
			    if ( sName == comp){
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
        public void ReloadComboBox()
        {
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
                //string sName = string.Empty;
                //if( selectedObj is Form ) {
                //    sName = ((Form) selectedObj).Name;
                //}
                //else if( selectedObj is Control ) {
                //    sName = ((Control) selectedObj).Site.Name;
                //}
                //if( string.IsNullOrEmpty( sName ) )
                //    return;

                //- prepare the data for reloading the combobox (begin)
                List<object> ctrlsToAdd = new List<object>();
                object pgrdComboBox_Text = null;
                try
                {
                    ComponentCollection ctrlsExisting = null;
                    if (host.Container.Components.Count > 0 && host.Container.Components[0] is EbReportPanel)
                        ctrlsExisting = ((host.Container.Components[0] as EbReportPanel).ParentForm as ReportDesignerForm).DesignerCore.ActiveDesignSurface.ComponentContainer.Components;
                    //else if ((selectedObj is EbForm) && ((selectedObj as EbForm).Parent is EbReportPanel))
                    //    ctrlsExisting = (selectedObj as EbReportPanel).Container.Components;
                    else
                        ctrlsExisting = host.Container.Components;

                    //ComponentCollection ctrlsExisting = (selectedObj is EbReportPanel) ? (selectedObj as EbReportPanel).Container.Components : host.Container.Components;
                    Debug.Assert( 0 != ctrlsExisting.Count );

                    foreach( Component comp in ctrlsExisting ) {
                        //string sItemText = TranslateComponentToName( comp );
                        ctrlsToAdd.Add(comp);
                        if(selectedObj == comp)
                            pgrdComboBox_Text = comp;
                    }

                    
                }
                catch( Exception ) {
                    return; //- (rollback)
                }
                //- update the combobox (commit)
                pgrdComboBox.Items.Clear();
                    pgrdComboBox.Items.AddRange( ctrlsToAdd.ToArray() );
                    //pgrdComboBox.DataSource = ctrlsToAdd;
                    //pgrdComboBox.Update();
                pgrdComboBox.SelectedItem = pgrdComboBox_Text;
            }
            finally {
                _bSuppressEvents = false;
            }
        }

        public GridItem CollapseGridItem( string sGridItemLabel )
        {
            return CollapseExpandGridItem(sGridItemLabel, false);
        }

        public GridItem ExpandGridItem( string sGridItemLabel )
        {
            return CollapseExpandGridItem(sGridItemLabel, true);
        }

        private GridItem CollapseExpandGridItem( string sGridItemLabel, bool bExpanded )
        {
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