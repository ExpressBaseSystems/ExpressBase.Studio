using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using ExpressBase.Studio.Controls;
using ExpressBase.Studio.pDesigner;

namespace pF.DesignSurfaceExt {

//- this class adds to a .NET 
//-     DesignSurface instance
//- the following facilities:
//-     * TabOrder
//-     * UndoEngine
//-     * Cut/Copy/Paste/Delete commands
//-
//- DesignSurfaceExt
//-     |
//-     +--|DesignSurface|
//-     |
//-     +--|TabOrder|
//-     |
//-     +--|UndoEngine|
//-     |
//-     +--|Cut/Copy/Paste/Delete commands|
//-
public class DesignSurfaceExt : DesignSurface, IDesignSurfaceExt {
    private const string _Name_ = "DesignSurfaceExt";

    

    #region IDesignSurfaceExt Members

    public void SwitchTabOrder() {
        if ( false == IsTabOrderMode ) {
            InvokeTabOrder();
        }
        else {
            DisposeTabOrder();
        }
    }

    public void UseSnapLines() {
        IServiceContainer serviceProvider = this.GetService ( typeof ( IServiceContainer ) ) as IServiceContainer;
        DesignerOptionService opsService = serviceProvider.GetService ( typeof ( DesignerOptionService ) ) as DesignerOptionService;
        if ( null != opsService ) {
            serviceProvider.RemoveService ( typeof ( DesignerOptionService ) );
        }
        DesignerOptionService opsService2 = new DesignerOptionServiceExt4SnapLines();
        serviceProvider.AddService ( typeof ( DesignerOptionService ), opsService2 );
    }

    public void UseGrid ( Size gridSize ) {
        IServiceContainer serviceProvider = this.GetService ( typeof ( IServiceContainer ) ) as IServiceContainer;
        DesignerOptionService opsService = serviceProvider.GetService ( typeof ( DesignerOptionService ) ) as DesignerOptionService;
        if ( null != opsService ) {
            serviceProvider.RemoveService ( typeof ( DesignerOptionService ) );
        }
        DesignerOptionService opsService2 = new DesignerOptionServiceExt4Grid ( gridSize );
        serviceProvider.AddService ( typeof ( DesignerOptionService ), opsService2 );
    }

    public void UseGridWithoutSnapping ( Size gridSize ) {
        IServiceContainer serviceProvider = this.GetService ( typeof ( IServiceContainer ) ) as IServiceContainer;
        DesignerOptionService opsService = serviceProvider.GetService ( typeof ( DesignerOptionService ) ) as DesignerOptionService;
        if ( null != opsService ) {
            serviceProvider.RemoveService ( typeof ( DesignerOptionService ) );
        }
        DesignerOptionService opsService2 = new DesignerOptionServiceExt4GridWithoutSnapping ( gridSize );
        serviceProvider.AddService ( typeof ( DesignerOptionService ), opsService2 );
    }

    public void UseNoGuides() {
        IServiceContainer serviceProvider = this.GetService ( typeof ( IServiceContainer ) ) as IServiceContainer;
        DesignerOptionService opsService = serviceProvider.GetService ( typeof ( DesignerOptionService ) ) as DesignerOptionService;
        if ( null != opsService ) {
            serviceProvider.RemoveService ( typeof ( DesignerOptionService ) );
        }
        DesignerOptionService opsService2 = new DesignerOptionServiceExt4NoGuides();
        serviceProvider.AddService ( typeof ( DesignerOptionService ), opsService2 );

    }

    public UndoEngineExt GetUndoEngineExt() {
        return this._undoEngine;
    }

    private IComponent CreateRootComponentCore ( Type controlType, Size controlSize, DesignerLoader loader ) {
        const string _signature_ = _Name_ + @"::CreateRootComponentCore()";
        try {
            //- step.1
            //- get the IDesignerHost
            //- if we are not not able to get it 
            //- then rollback (return without do nothing)
            IDesignerHost host = GetIDesignerHost();
            if ( null == host ) return null;
            //- check if the root component has already been set
            //- if so then rollback (return without do nothing)
            if( null != host.RootComponent ) return null;
            //-
            //-
            //- step.2
            //- create a new root component and initialize it via its designer
            //- if the component has not a designer
            //- then rollback (return without do nothing)
            //- else do the initialization
            if( null != loader ) {
                this.BeginLoad( loader );
                if( this.LoadErrors.Count > 0 )
                    throw new Exception( _signature_ + " - Exception: the BeginLoad(loader) failed!" );
            }
            else {
                this.BeginLoad( controlType );
                if( this.LoadErrors.Count > 0 )
                    throw new Exception( _signature_ + " - Exception: the BeginLoad(Type) failed! Some error during " + controlType.ToString() + " loading" );
            }
            //-
            //-
            //- step.3
            //- try to modify the Size of the object just created
            IDesignerHost ihost = GetIDesignerHost();
            //- Set the backcolor and the Size
            Control ctrl = null;
            if( host.RootComponent is  Form ) {
                ctrl = this.View as Control;
                ctrl.BackColor = Color.LightGray;
                //- set the Size
                PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties( ctrl );
                //- Sets a PropertyDescriptor to the specific property
                PropertyDescriptor pdS = pdc.Find( "Size", false );
                if( null != pdS )
                    pdS.SetValue( ihost.RootComponent, controlSize );
            }
            else if( host.RootComponent is UserControl ) {
                ctrl = this.View as Control;
                ctrl.BackColor = Color.Gray;
                //- set the Size
                PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties( ctrl );
                //- Sets a PropertyDescriptor to the specific property
                PropertyDescriptor pdS = pdc.Find( "Size", false );
                if( null != pdS )
                    pdS.SetValue( ihost.RootComponent, controlSize );
            }
            else if(  host.RootComponent is  Control ) {
                ctrl = this.View as Control;
                ctrl.BackColor = Color.LightGray;
                //- set the Size
                PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties( ctrl );
                //- Sets a PropertyDescriptor to the specific property
                PropertyDescriptor pdS = pdc.Find( "Size", false );
                if( null != pdS )
                    pdS.SetValue( ihost.RootComponent, controlSize );
            }
            else if(  host.RootComponent is  Component ) {
                ctrl = this.View as Control;
                ctrl.BackColor = Color.White;
                //- don't set the Size
            }
            else {
                //- Undefined Host Type
                ctrl = this.View as Control;
                ctrl.BackColor = Color.Red;
            }

            return ihost.RootComponent;
        }//end_try
        catch( Exception exx ) {
            Debug.WriteLine( exx.Message );
            if( null != exx.InnerException )
                Debug.WriteLine( exx.InnerException.Message );
            
            throw;
        }//end_catch
    }

    public IComponent CreateRootComponent( Type controlType, Size controlSize ) {
        return CreateRootComponentCore( controlType, controlSize, null );
    }

    public IComponent CreateRootComponent( DesignerLoader loader, Size controlSize ) {
        return CreateRootComponentCore( null, controlSize, loader );
    }
    
    public Control CreateControl ( Type controlType, Size controlSize, Point controlLocation ) {
        try {
            //- step.1
            //- get the IDesignerHost
            //- if we are not able to get it 
            //- then rollback (return without do nothing)
            IDesignerHost host = GetIDesignerHost();
            if ( null == host ) return null;
            //- check if the root component has already been set
            //- if not so then rollback (return without do nothing)
            if( null == host.RootComponent ) return null;
            //-
            //-
            //- step.2
            //- create a new component and initialize it via its designer
            //- if the component has not a designer
            //- then rollback (return without do nothing)
            //- else do the initialization
            IComponent newComp = host.CreateComponent ( controlType );
            if ( null == newComp ) return null;
            IDesigner designer = host.GetDesigner ( newComp );
            if ( null == designer ) return null;
            if ( designer is IComponentInitializer )
                ( ( IComponentInitializer ) designer ).InitializeNewComponent ( null );
            //-
            //-
            //- step.3
            //- try to modify the Size/Location of the object just created
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties ( newComp );
            //- Sets a PropertyDescriptor to the specific property.
            PropertyDescriptor pdS = pdc.Find ( "Size", false );
            if ( null != pdS )
                pdS.SetValue ( newComp, controlSize );
            PropertyDescriptor pdL = pdc.Find ( "Location", false );
            if ( null != pdL )
                pdL.SetValue ( newComp, controlLocation );
            //-
            //-
            //- step.4
            //- commit the Creation Operation
            //- adding the control to the DesignSurface's root component
            //- and return the control just created to let further initializations
            ( ( Control ) newComp ).Parent = host.RootComponent as Control;
            return newComp as Control;
        }//end_try
        catch( Exception exx ) {
            Debug.WriteLine( exx.Message );
            if( null != exx.InnerException )
                Debug.WriteLine( exx.InnerException.Message );
            
            throw;
        }//end_catch
    }

    public IDesignerHost GetIDesignerHost() {
        return ( IDesignerHost ) ( this.GetService ( typeof ( IDesignerHost ) ) );
    }

    public Control GetView() {
        Control ctrl = this.View as Control;
        if( null == ctrl )
            return null;
        ctrl.Dock = DockStyle.Fill;
        return ctrl;
    }

        public void SetRootComponent(EbForm _form)
        {
            foreach (Control c in _form.Controls)
            {
                var ctrl = CreateControl(c.GetType(), c.Size, c.Location);
                ctrl.Name = c.Name;
                if (c is EbTabControl)
                    (ctrl as EbTabControl).TabPages = (c as EbTabControl).TabPages;
                if (c is EbTabPage)
                    (ctrl as EbTabPage).Controls2 = (c as EbTabPage).Controls2;
            }
        }

    #endregion



    #region TabOrder
    
    private TabOrderHooker _tabOrder = null;
    private bool _tabOrderMode = false;
    
    public bool IsTabOrderMode {
        get { return _tabOrderMode;  }
    }

    public TabOrderHooker TabOrder {
        get {
            if ( _tabOrder == null )
                _tabOrder = new TabOrderHooker();
            return _tabOrder;
        }
        set {  _tabOrder = value;  }
    }

    public void InvokeTabOrder() {
        TabOrder.HookTabOrder ( this.GetIDesignerHost() );
        _tabOrderMode = true;
    }

    public void DisposeTabOrder() {
        TabOrder.HookTabOrder ( this.GetIDesignerHost() );
        _tabOrderMode = false;
    }
    #endregion

        #region  UndoEngine

        private UndoEngineExt _undoEngine = null;
        private NameCreationServiceImp _nameCreationService = null;
        private DesignerSerializationServiceImpl _designerSerializationService = null;
        private CodeDomComponentSerializationService _codeDomComponentSerializationService = null;

        #endregion

        #region ctors

        //- ctors
        //- Summary:
        //-     Initializes a new instance of the System.ComponentModel.Design.DesignSurface
        //-     class.
        //-
        //- Exceptions:
        //-   System.ObjectDisposedException:
        //-     The System.ComponentModel.Design.IDesignerHost attached to the System.ComponentModel.Design.DesignSurface
        //-     has been disposed.
        public DesignSurfaceExt() : base() { InitServices(); }
    //-
    //- Summary:
    //-     Initializes a new instance of the System.ComponentModel.Design.DesignSurface
    //-     class.
    //-
    //- Parameters:
    //-   parentProvider:
    //-     The parent service provider, or null if there is no parent used to resolve
    //-     services.
    //-
    //- Exceptions:
    //-   System.ObjectDisposedException:
    //-     The System.ComponentModel.Design.IDesignerHost attached to the System.ComponentModel.Design.DesignSurface
    //-     has been disposed.
    public DesignSurfaceExt( IServiceProvider parentProvider ) : base( parentProvider ) { InitServices(); }
    //-
    //- Summary:
    //-     Initializes a new instance of the System.ComponentModel.Design.DesignSurface
    //-     class.
    //-
    //- Parameters:
    //-   rootComponentType:
    //-     The type of root component to create.
    //-
    //- Exceptions:
    //-   System.ArgumentNullException:
    //-     rootComponent is null.
    //-
    //-   System.ObjectDisposedException:
    //-     The System.ComponentModel.Design.IDesignerHost attached to the System.ComponentModel.Design.DesignSurface
    //-     has been disposed.
    public DesignSurfaceExt( Type rootComponentType ) : base( rootComponentType ) { InitServices(); }
    //-
    //- Summary:
    //-     Initializes a new instance of the System.ComponentModel.Design.DesignSurface
    //-     class.
    //-
    //- Parameters:
    //-   parentProvider:
    //-     The parent service provider, or null if there is no parent used to resolve
    //-     services.
    //-
    //-   rootComponentType:
    //-     The type of root component to create.
    //-
    //- Exceptions:
    //-   System.ArgumentNullException:
    //-     rootComponent is null.
    //-
    //-   System.ObjectDisposedException:
    //-     The System.ComponentModel.Design.IDesignerHost attached to the System.ComponentModel.Design.DesignSurface
    //-     has been disposed.
    public DesignSurfaceExt( IServiceProvider parentProvider, Type rootComponentType ) : base( parentProvider, rootComponentType ) { InitServices(); }


    //- The DesignSurface class provides several design-time services automatically.
    //- The DesignSurface class adds all of its services in its constructor.
    //- Most of these services can be overridden by replacing them in the
    //- protected ServiceContainer property.To replace a service, override the constructor,
    //- call base, and make any changes through the protected ServiceContainer property.
    private void InitServices() {
        //- each DesignSurface has its own default services
        //- We can leave the default services in their present state,
        //- or we can remove them and replace them with our own.
        //- Now add our own services using IServiceContainer
        //-
        //-
        //- Note
        //- before loading the root control in the design surface
        //- we must add an instance of naming service to the service container.
        //- otherwise the root component did not have a name and this caused
        //- troubles when we try to use the UndoEngine
        //-
        //-
        //- 1. NameCreationService
        _nameCreationService = new NameCreationServiceImp();
        if( _nameCreationService != null ) {
            this.ServiceContainer.RemoveService( typeof( INameCreationService ), false );
            this.ServiceContainer.AddService( typeof( INameCreationService ), _nameCreationService );
        }
        //-
        //-
        //- 2. CodeDomComponentSerializationService
        _codeDomComponentSerializationService = new CodeDomComponentSerializationService( this.ServiceContainer );
        if( _codeDomComponentSerializationService != null ) {
            //- the CodeDomComponentSerializationService is ready to be replaced
            this.ServiceContainer.RemoveService( typeof( ComponentSerializationService ), false );
            this.ServiceContainer.AddService( typeof( ComponentSerializationService ), _codeDomComponentSerializationService );
        }
        //-
        //-
        //- 3. IDesignerSerializationService
        _designerSerializationService = new DesignerSerializationServiceImpl( this.ServiceContainer );
        if( _designerSerializationService != null ) {
            //- the IDesignerSerializationService is ready to be replaced
            this.ServiceContainer.RemoveService( typeof( IDesignerSerializationService ), false );
            this.ServiceContainer.AddService( typeof( IDesignerSerializationService ), _designerSerializationService );
        }
        //-
        //-
        //- 4. UndoEngine
        _undoEngine = new UndoEngineExt( this.ServiceContainer );
        //- disable the UndoEngine
        _undoEngine.Enabled = false;
        if( _undoEngine != null ) {
            //- the UndoEngine is ready to be replaced
            this.ServiceContainer.RemoveService( typeof( UndoEngine ), false );
            this.ServiceContainer.AddService( typeof( UndoEngine ), _undoEngine );
        }
        //-
        //-
        //- 5. IMenuCommandService
            this.ServiceContainer.AddService( typeof( IMenuCommandService ), new MenuCommandService( this ) );
            this.ServiceContainer.AddService(typeof(ITypeDiscoveryService), new TypeDiscoveryService());
        }
    
    #endregion

    //- do some Edit menu command using the MenuCommandServiceImp
    public void DoAction( string command ) {
        if( string.IsNullOrEmpty( command ) ) return;

        IMenuCommandService ims = this.GetService( typeof( IMenuCommandService ) ) as IMenuCommandService;
        if( null == ims ) return;


        try {
            switch( command.ToUpper() ) {
                case "CUT" :
                    ims.GlobalInvoke( StandardCommands.Cut );
                    break;
                case "COPY" :
                    ims.GlobalInvoke( StandardCommands.Copy );
                    break;
                case "PASTE":
                    ims.GlobalInvoke( StandardCommands.Paste );
                    break;
                case "DELETE":
                    ims.GlobalInvoke( StandardCommands.Delete );
                    break;
                default:
                    // do nothing;
                    break;
            }//end_switch
        }//end_try
        catch( Exception exx ) {
            Debug.WriteLine( exx.Message );
            if( null != exx.InnerException )
                Debug.WriteLine( exx.InnerException.Message );

            throw;
        }//end_catch
    }
 
}//end_class
}//end_namespace
