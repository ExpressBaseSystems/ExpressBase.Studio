namespace pF.DesignSurfaceManagerExt {

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
    using System.Collections;

    using pF.DesignSurfaceExt;
    using ExpressBase.Studio.Controls;

    //- this class manages a collection of DesignSurfaceExt2 instances
    //- this class adds to
    //-     DesignSurfaceExt2 instances
    //- the following facilities:
    //-     * PropertyGridHost 
    //-
    //- DesignSurfaceExt2
    //-     |
    //-     +--|PropertyGridHost|
    //-     |
    //-     +--|Toolbox|
    //-     |
    //-     +--|ContextMenu|
    //-     |
    //-     +--|DesignSurfaceExt|
    //-             |
    //-             +--|DesignSurface|
    //-             |
    //-             +--|TabOrder|
    //-             |
    //-             +--|UndoEngine|
    //-             |
    //-             +--|Cut/Copy/Paste/Delete commands|
    //-
    //-
    //- [Note FROM MSDN]:
    //- The ActiveDesignSurface property should be set 
    //- by the designer's Type user interface 
    //- whenever a designer becomes the active window!
    //- That is to say:
    //-   the DesignSurfaceManagerExt is an OBSERVER of UI event: SelectedTab/SelectedIndex Changed
    //- usage:
    //       //- SelectedIndexChanged event fires when the TabControls SelectedIndex or SelectedTab value changes. 
    //       //- give the focus to the DesigneSurface accordingly to te selected TabPage and sync the propertyGrid
    //       this.tabControl1.SelectedIndexChanged += ( object sender, EventArgs e ) => {
    //                TabControl tabCtrl = sender as TabControl;
    //                mgr.ActiveDesignSurface = (DesignSurfaceExt2) mgr.DesignSurfaces[tabCtrl.SelectedIndex];
    //       };
    //-
    public class DesignSurfaceManagerExt : DesignSurfaceManager {
    private const string _Name_ = "DesignSurfaceManagerExt";

    //- this List<> is necessary to be able to delete the DesignSurfaces previously created
    //- Note: 
    //-     the DesignSurfaceManager.DesignSurfaces Property is a collection of design surfaces 
    //-     that are currently hosted by the DesignSurfaceManager but is readonly
    private List<DesignSurfaceExt2> DesignSurfaceExt2Collection = new List<DesignSurfaceExt2>();

    public PropertyGridHost PropertyGridHost { get;  private set; }


    #region ctors
    //- ctors
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.Design.DesignSurfaceManager
    //     class.
    public DesignSurfaceManagerExt() : base() { Init();  }
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.Design.DesignSurfaceManager
    //     class.
    //
    // Parameters:
    //   parentProvider:
    //     A parent service provider. Service requests are forwarded to this provider
    //     if they cannot be resolved by the design surface manager.
    public DesignSurfaceManagerExt( IServiceProvider parentProvider ) : base( parentProvider ) { Init(); }
    //-
    private void Init(){
        this.PropertyGridHost = new PropertyGridHost( this );
        //- add the PropertyGridHost and ComboBox as services
        //- to let them available for every DesignSurface
        //- (the DesignSurface need a PropertyGridHost/ComboBox not a the UserControl hosting them so
        //- we provide the PropertyGridHost/ComboBox embedded inside our UserControl PropertyGridExt)
        this.ServiceContainer.AddService( typeof( PropertyGrid ), PropertyGridHost.PropertyGrid );
        this.ServiceContainer.AddService( typeof( ComboBox ), PropertyGridHost.ComboBox );
  
        
        this.ActiveDesignSurfaceChanged += ( object sender, ActiveDesignSurfaceChangedEventArgs e ) =>
        {
            DesignSurfaceExt2 surface = e.NewSurface as DesignSurfaceExt2;
            if( null == surface )
                return;

            UpdatePropertyGridHost( surface );
        };
    }

    public void UpdatePropertyGridHost( DesignSurfaceExt2 surface ) {
            if (surface == null)
                return;
        IDesignerHost host = (IDesignerHost) surface.GetService( typeof( IDesignerHost ) );
        if( null == host)
            return;
        if( null == host.RootComponent ) 
            return;

        //- sync the PropertyGridHost
        this.PropertyGridHost.SelectedObject = host.RootComponent;
        this.PropertyGridHost.ReloadComboBox();
    }

    #endregion

    //- The CreateDesignSurfaceCore method is called by both CreateDesignSurface methods
    //- It is the implementation that actually creates the design surface
    //- The default implementation just returns a new DesignSurface, we override 
    //- this method to provide a custom object that derives from the DesignSurface class
    //- i.e. a new DesignSurfaceExt2 instance
    protected override DesignSurface CreateDesignSurfaceCore( IServiceProvider parentProvider ) {
        return new DesignSurfaceExt2( parentProvider );
    }

    

    //- Gets a new DesignSurfaceExt2 
    //- and loads it with the appropriate type of root component. 
    public DesignSurfaceExt2 CreateDesignSurfaceExt2() {
        //- with a DesignSurfaceManager class, is useless to add new services 
        //- to every design surface we are about to create,
        //- because of the "IServiceProvider" parameter of CreateDesignSurface(IServiceProvider) Method.
        //- This param let every design surface created 
        //- to use the services of the DesignSurfaceManager.
        //- A new merged service provider will be created that will first ask 
        //- this provider for a service, and then delegate any failures 
        //- to the design surface manager object. 
        //- Note:
        //-     the following line of code create a brand new DesignSurface which is added 
        //-     to the Designsurfeces collection, 
        //-     i.e. the property "this.DesignSurfaces" ( the .Count in incremented by one)
        DesignSurfaceExt2 surface = (DesignSurfaceExt2) (this.CreateDesignSurface( this.ServiceContainer ));

        
        //- each time a brand new DesignSurface is created,
        //- subscribe our handler to its SelectionService.SelectionChanged event
        //- to sync the PropertyGridHost
        ISelectionService selectionService = (ISelectionService)(surface.GetService(typeof(ISelectionService)));
        if( null != selectionService ) {
            selectionService.SelectionChanged += ( object sender, EventArgs e ) =>
            {
                ISelectionService selectService = sender as ISelectionService;
                if( null == selectService )
                    return;

                if( 0 == selectService.SelectionCount )
                    return;

                //- Sync the PropertyGridHost
                PropertyGrid propertyGrid = (PropertyGrid) this.GetService( typeof( PropertyGrid ) );
                if( null == propertyGrid )
                    return;

                ArrayList comps = new ArrayList();
                var x = selectService.GetSelectedComponents();
                foreach (Control c in x)
                {
                    if (c is IEbControl)
                        comps.Add((c as IEbControl).EbControl);
                }
                //comps.AddRange( selectService.GetSelectedComponents() );
                propertyGrid.SelectedObjects = comps.ToArray();
            };
        }
        DesignSurfaceExt2Collection.Add( surface );
        this.ActiveDesignSurface = surface;

        //- and return the DesignSurface (to let the its BeginLoad() method to be called)
        return surface;
    }

        public void SyncPropertyGridHost()
        {
            if (null == this.ActiveDesignSurface)
                return;

            ISelectionService selectService = (ISelectionService)(this.ActiveDesignSurface.GetService(typeof(ISelectionService)));
            if (null == selectService)
                return;

            if (0 == selectService.SelectionCount)
                return;

            PropertyGrid propertyGrid = (PropertyGrid)this.GetService(typeof(PropertyGrid));
            if (null == propertyGrid)
                return;

            ArrayList comps = new ArrayList();
            var x = selectService.GetSelectedComponents();
            foreach (Control c in x)
            {
                if (c is IEbControl)
                    comps.Add((c as IEbControl).EbControl);
            }
            //comps.AddRange(selectService.GetSelectedComponents());
            propertyGrid.SelectedObjects = comps.ToArray();
        }

    public void DeleteDesignSurfaceExt2(DesignSurfaceExt2 item) {
        DesignSurfaceExt2Collection.Remove( item );
        try {
            item.Dispose();
        }
        catch( Exception ex) {
            System.Diagnostics.Debug.WriteLine( ex.Message );
        }
        int currentIndex = DesignSurfaceExt2Collection.Count - 1;
        if( currentIndex >= 0 )
            ActiveDesignSurface = DesignSurfaceExt2Collection[currentIndex];
        else
            ActiveDesignSurface = null;
    }

    public void DeleteDesignSurfaceExt2( int index  ) {
        DesignSurfaceExt2 item = DesignSurfaceExt2Collection[index];
        DesignSurfaceExt2Collection.RemoveAt( index );
        try {
            item.Dispose();
        }
        catch( Exception ex ) {
            System.Diagnostics.Debug.WriteLine( ex.Message );
        }
        int currentIndex = DesignSurfaceExt2Collection.Count - 1;
        if( currentIndex >= 0 )
            ActiveDesignSurface = DesignSurfaceExt2Collection[currentIndex];
        else
            ActiveDesignSurface = null;
    }


    //- loop through all the collection of DesignSurface 
    //- to find out a brand new Form name
    public string GetValidFormName() {
        //- we choose to use "Form_" with an underscore char as trailer 
        //- because the .NET design services provide a name of type: "FormN"
        //- with N=1,2,3,4,...
        //- therefore using a "Form", without an underscore char as trailer,
        //- cause some troubles when we have to decide if a name is used or not
        //- using a different building pattern (with the underscore) avoid this issue
        string newFormNameHeader  = "Form_";
        int newFormNametrailer = -1;
        string newFormName = string.Empty;
        bool isNew = true;
        do {
            isNew = true;
            newFormNametrailer++;
            newFormName = newFormNameHeader + newFormNametrailer;
            foreach( DesignSurfaceExt2 item in DesignSurfaceExt2Collection ) {
                string currentFormName = item.GetIDesignerHost().RootComponent.Site.Name;
                isNew &= ((newFormName == currentFormName) ? false : true);
            }//end_foreach
            
        } while( !isNew ); 
        return newFormName;
    }





    public new DesignSurfaceExt2 ActiveDesignSurface {
        get { return base.ActiveDesignSurface as DesignSurfaceExt2; }
        set { base.ActiveDesignSurface = value; }
    }
       
}//end_class
}//end_namespace
