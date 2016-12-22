using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Design;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using ExpressBase.Studio.Controls;

namespace ExpressBase.Studio.Controls
{
    #region EB_TabControl Class

    /// <summary>
    /// Summary description for EB_TabControl.
    /// </summary>
    [ProtoBuf.ProtoContract]
    [ToolboxBitmap(typeof(System.Windows.Forms.TabControl)), Designer(typeof(Designers.EbTabControlDesigner))]
    public class EbTabControl : System.Windows.Forms.TabControl, IEbControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public EbObject EbObject { get; set; }

        [ProtoBuf.ProtoMember(2)]
        [Browsable(false)]
        public IEbControl[] Controls2 { get; set; }

        public EbTabControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        public event EB_TabControlEventHandler SelectedIndexChanging;
        public TabPage HotTab = null;

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }


        #endregion

        #region Properties

        [Editor(typeof(EB_TabPageCollectionEditor), typeof(UITypeEditor))]
        public new TabPageCollection TabPages
        {
            get
            {
                return base.TabPages;
            }
            set
            {
                base.TabPages.Clear();
                foreach (EbTabPage t in (value as TabPageCollection))
                {
                    base.TabPages.Add(t);
                }
            }
        }

        [ProtoBuf.ProtoMember(1)]
        [Browsable(false)]
        public EbTabPage[] TabPages2
        {
            get
            {
                EbTabPage[] ca = new EbTabPage[this.Controls.Count];
                this.Controls.CopyTo(ca, 0);
                return ca;
            }
            set
            {
                foreach (EbTabPage c in value)
                    this.Controls.Add((Control)c);
            }
        }

        [ProtoBuf.ProtoMember(2)]
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

        [ProtoBuf.ProtoMember(3)]
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

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, IEbControl serialized_ctrl)
        {
            //var ctrl = designer.ActiveDesignSurface.CreateControl(this.GetType(), this.Size, this.Location) as System.Windows.Forms.Control;
            //ctrl.Name = this.Name;
        }

        public void DoDesignerRefresh() { }

        #endregion

        #region EB_TabPageCollectionEditor

        internal class EB_TabPageCollectionEditor : CollectionEditor
        {
            public EB_TabPageCollectionEditor(System.Type type) : base(type)
            {
            }

            protected override Type CreateCollectionItemType()
            {
                return typeof(EbTabPage);
            }
        }

        #endregion

        #region Interop for SelectedIndexChanging event 

        [StructLayout(LayoutKind.Sequential)]
        private struct NMHDR
        {
            public IntPtr HWND;
            public uint idFrom;
            public int code;
            public override String ToString()
            {
                return String.Format("Hwnd: {0}, ControlID: {1}, Code: {2}", HWND, idFrom, code);
            }
        }

        private const int TCN_FIRST = 0 - 550;
        private const int TCN_SELCHANGING = (TCN_FIRST - 2);

        private const int WM_USER = 0x400;
        private const int WM_NOTIFY = 0x4E;
        private const int WM_REFLECT = WM_USER + 0x1C00;

        #endregion

        #region SelectedIndexChanging event Implementation

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (WM_REFLECT + WM_NOTIFY))
            {
                NMHDR hdr = (NMHDR)(Marshal.PtrToStructure(m.LParam, typeof(NMHDR)));
                if (hdr.code == TCN_SELCHANGING)
                {
                    if (HotTab != null)
                    {
                        EB_TabControlEventArgs e = new EB_TabControlEventArgs(HotTab, Controls.IndexOf(HotTab));
                        if (SelectedIndexChanging != null)
                            SelectedIndexChanging(this, e);
                        if (e.Cancel || !HotTab.Enabled)
                        {
                            m.Result = new IntPtr(1);
                            return;
                        }
                    }
                }
            }
            base.WndProc(ref m);
        }


        #endregion

        #region HotTab Immplementation

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            HotTab = TestTab(new Point(e.X, e.Y));
        }


        #endregion

        #region Custom Methods

        public void InsertTabPage(TabPage tabpage, int index)
        {
            if (index < 0 || index > TabCount)
                throw new ArgumentException("Index out of Range.");

            TabPages.Add(tabpage);

            if (index < TabCount - 1)
            {
                do
                    SwapTabPages(tabpage, (TabPages[TabPages.IndexOf(tabpage) - 1]));
                while (TabPages.IndexOf(tabpage) != index);
            }

            SelectedTab = tabpage;
        }


        public void SwapTabPages(TabPage tp1, TabPage tp2)
        {
            if (!TabPages.Contains(tp1) || !TabPages.Contains(tp2))
                throw new ArgumentException("TabPages must be in the TabCotrols TabPageCollection.");
            int Index1 = TabPages.IndexOf(tp1);
            int Index2 = TabPages.IndexOf(tp2);
            TabPages[Index1] = tp2;
            TabPages[Index2] = tp1;
        }


        private TabPage TestTab(Point pt)
        {
            for (int index = 0; index <= TabCount - 1; index++)
            {
                if (GetTabRect(index).Contains(pt.X, pt.Y))
                    return TabPages[index];
            }
            return null;
        }


        #endregion

    }

    #region SelectedIndexChanging EventArgs Class/Delegate

    public class EB_TabControlEventArgs : EventArgs
    {
        private TabPage m_TabPage = null;
        private int m_TabPageIndex = -1;
        public bool Cancel = false;

        public TabPage tabPage
        {
            get
            {
                return m_TabPage;
            }
        }

        public int TabPageIndex
        {
            get
            {
                return m_TabPageIndex;
            }
        }

        public EB_TabControlEventArgs(TabPage tabPage, int TabPageIndex)
        {
            m_TabPage = tabPage;
            m_TabPageIndex = TabPageIndex;
        }
    }

    public delegate void EB_TabControlEventHandler(Object sender, EB_TabControlEventArgs e);

    #endregion

    #endregion

    #region EB_TabPage Class

    [ProtoBuf.ProtoContract]
    [Designer(typeof(System.Windows.Forms.Design.ScrollableControlDesigner))]
    public class EbTabPage : TabPage
    {
        #region API Declares

        [DllImport("Comctl32.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int DllGetVersion(ref DLLVERSIONINFO pdvi);

        [DllImport("uxtheme.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool IsAppThemed();

        [DllImport("uxtheme.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern IntPtr OpenThemeData(IntPtr hwnd, String pszClassList);

        [DllImport("uxtheme.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int GetThemePartSize(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref Rectangle prc, THEMESIZE eSize, ref Size psz);

        [DllImport("uxtheme.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int DrawThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref Rectangle pRect, IntPtr pClipRect);

        [DllImport("uxtheme.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CloseThemeData(IntPtr htheme);

        private struct DLLVERSIONINFO
        {
            public int cbSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformID;
            public DLLVERSIONINFO(Control ctrl)
            {
                cbSize = Marshal.SizeOf(typeof(DLLVERSIONINFO));
                dwMajorVersion = 0;
                dwMinorVersion = 0;
                dwBuildNumber = 0;
                dwPlatformID = 0;
            }
        }


        private enum THEMESIZE
        {
            TS_MIN,
            TS_TRUE,
            TS_DRAW
        }


        private const int TABP_BODY = 10;
        private const int WM_THEMECHANGED = 0x31A;

        #endregion

        #region Properties

        private bool bStyled = true;
        private Brush m_Brush;

        public EbObject EbObject { get; set; }

        private bool AppIsXPThemed
        {
            //IsAppThemed will return True if the App is not using visual
            //Styles but It's TitleBar is drawn with Visual Style(i.e. a
            //manifest resource has not been supplied). To overcome this
            //problem we must also check which version of ComCtl32.dll is
            //being used. Since ComCtl32.dll version 6 is exclusive to
            //WindowsXP, we do not need to check the OSVersion.
            get
            {
                DLLVERSIONINFO dllVer = new DLLVERSIONINFO(this);
                DllGetVersion(ref dllVer);
                if (dllVer.dwMajorVersion >= 6) return IsAppThemed();
                return false;
            }
        }

        [Category("Appearance")]
        [Description("Enables/Disables Visual Styles on the TabPage. Valid only in WidowsXP.")]
        [DefaultValue(true)]
        public bool EnableVisualStyles
        {
            get
            {
                return bStyled;
            }
            set
            {
                if (bStyled == value) return;
                bStyled = value;
                Invalidate(true);
            }
        }

        //[ProtoBuf.ProtoMember(1)]
        //[Browsable(false)]
        //public IEbControl[] Controls2
        //{
        //    get
        //    {
        //        IEbControl[] ca = new IEbControl[this.Controls.Count];
        //        this.Controls.CopyTo(ca, 0);
        //        return ca;
        //    }
        //    set
        //    {
        //        foreach (IEbControl c in value)
        //            this.Controls.Add((Control)c);
        //    }
        //}

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbObject serialized_ctrl)
        {
            //var ctrl = designer.ActiveDesignSurface.CreateControl(this.GetType(), this.Size, this.Location) as System.Windows.Forms.Control;
            //ctrl.Name = this.Name;
        }

        public void DoDesignerRefresh() { }

        #endregion

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public EbTabPage()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
        }

        public EbTabPage(String Text) : base()
        {
            base.Text = Text;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.Disposed += new EventHandler(EB_TabPage_Disposed);
        }


        #endregion

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (EnableVisualStyles && AppIsXPThemed)
            {
                if (m_Brush == null) SetTabBrush();
                //Paint the TabPage with our Brush.
                pevent.Graphics.FillRectangle(m_Brush, ClientRectangle);
            }
            else
                //Call the default Paint Event.
                base.OnPaintBackground(pevent);
        }

        private void SetTabBrush()
        {
            IntPtr hdc;
            IntPtr hTheme;
            Size sz = new Size(0, 0);
            Bitmap bmp;
            int h = Height;

            //Open the theme data for the Tab Class.
            hTheme = OpenThemeData(Handle, "TAB");

            //Get the size of the Active Theme's TabPage Bitmap.
            Rectangle displayrect = DisplayRectangle;
            GetThemePartSize(hTheme, IntPtr.Zero, TABP_BODY, 0, ref displayrect, THEMESIZE.TS_TRUE, ref sz);

            //If the TabPage is taller than the bitmap then we'll get a
            //nasty block efect so we'll check for that and correct.
            if (h > sz.Height) sz.Height = h;
            //Create a new bitmap of the correct size.
            bmp = new Bitmap(sz.Width, sz.Height);
            //Create a Graphics object from our bitmap so we can
            //draw to it.
            Graphics g = Graphics.FromImage(bmp);

            //Get the handle to the Graphics Object's DC for API usage.
            hdc = g.GetHdc(); //Hidden member of Graphics

            Rectangle bmpRect = new Rectangle(0, 0, sz.Width, sz.Height);
            //Draw to the Bitmaps Graphics Object.
            DrawThemeBackground(hTheme, hdc, TABP_BODY, 0, ref bmpRect, IntPtr.Zero);

            //Release the DC to Windows.
            g.ReleaseHdc(hdc); //Hidden member of Graphics

            //Close the theme data for the Tab Class.
            CloseThemeData(hTheme);

            //Create a BitmapBrush.
            m_Brush = new TextureBrush(bmp);

            //Clean Up
            bmp.Dispose();
            g.Dispose();

        }

        private void EB_TabPage_Disposed(Object sender, System.EventArgs e)
        {
            //Get rid of the brush if we created one.
            if (m_Brush != null) m_Brush.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (AppIsXPThemed) SetTabBrush();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_THEMECHANGED)
                SetTabBrush();
        }

        //Have to take responsibility for drawing TabItems for this method to be useful.
        //Protected Overrides Function ProcessMnemonic(ByVal charCode As Char) As Boolean
        //    If IsMnemonic(charCode, Text) Then
        //        DirectCast(Parent, TabControl).SelectedTab = Me
        //        Return True
        //    End If
        //    Return False
        //End Function
    }

    #endregion
}


namespace Designers
{

    internal class EbTabControlDesigner : System.Windows.Forms.Design.ParentControlDesigner
    {

        #region Private Instance Variables

        private DesignerVerbCollection m_verbs = new DesignerVerbCollection();
        private IDesignerHost m_DesignerHost;
        private ISelectionService m_SelectionService;

        #endregion

        public EbTabControlDesigner() : base()
        {
            DesignerVerb verb1 = new DesignerVerb("Add Tab", new EventHandler(OnAddPage));
            DesignerVerb verb2 = new DesignerVerb("Insert Tab", new EventHandler(OnInsertPage));
            DesignerVerb verb3 = new DesignerVerb("Remove Tab", new EventHandler(OnRemovePage));
            m_verbs.AddRange(new DesignerVerb[] { verb1, verb2, verb3 });
        }


        #region Properties

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (m_verbs.Count == 3)
                {
                    EbTabControl MyControl = (EbTabControl)Control;
                    if (MyControl.TabCount > 0)
                    {
                        m_verbs[1].Enabled = true;
                        m_verbs[2].Enabled = true;
                    }
                    else
                    {
                        m_verbs[1].Enabled = false;
                        m_verbs[2].Enabled = false;
                    }
                }
                return m_verbs;
            }
        }


        public IDesignerHost DesignerHost
        {
            get
            {
                if (m_DesignerHost == null)
                    m_DesignerHost = (IDesignerHost)(GetService(typeof(IDesignerHost)));

                return m_DesignerHost;
            }
        }


        public ISelectionService SelectionService
        {
            get
            {
                if (m_SelectionService == null)
                    m_SelectionService = (ISelectionService)(this.GetService(typeof(ISelectionService)));
                return m_SelectionService;
            }
        }


        #endregion

        void OnAddPage(Object sender, EventArgs e)
        {
            EbTabControl ParentControl = (EbTabControl)Control;
            Control.ControlCollection oldTabs = ParentControl.Controls;

            RaiseComponentChanging(TypeDescriptor.GetProperties(ParentControl)["TabPages"]);

            EbTabPage P = (EbTabPage)(DesignerHost.CreateComponent(typeof(EbTabPage)));
            P.Text = P.Name;
            ParentControl.TabPages.Add(P);

            RaiseComponentChanged(TypeDescriptor.GetProperties(ParentControl)["TabPages"], oldTabs, ParentControl.TabPages);
            ParentControl.SelectedTab = P;

            SetVerbs();

        }


        void OnInsertPage(Object sender, EventArgs e)
        {
            EbTabControl ParentControl = (EbTabControl)Control;
            Control.ControlCollection oldTabs = ParentControl.Controls;
            int Index = ParentControl.SelectedIndex;

            RaiseComponentChanging(TypeDescriptor.GetProperties(ParentControl)["TabPages"]);

            EbTabPage P = (EbTabPage)(DesignerHost.CreateComponent(typeof(EbTabPage)));
            P.Text = P.Name;

            EbTabPage[] tpc = new EbTabPage[ParentControl.TabCount];
            //Starting at our Insert Position, store and remove all the tabpages.
            for (int i = Index; i <= tpc.Length - 1; i++)
            {
                tpc[i] = (EbTabPage)ParentControl.TabPages[Index];
                ParentControl.TabPages.Remove(ParentControl.TabPages[Index]);
            }
            //add the tabpage to be inserted.
            ParentControl.TabPages.Add(P);
            //then re-add the original tabpages.
            for (int i = Index; i <= tpc.Length - 1; i++)
            {
                ParentControl.TabPages.Add(tpc[i]);
            }

            RaiseComponentChanged(TypeDescriptor.GetProperties(ParentControl)["TabPages"], oldTabs, ParentControl.TabPages);
            ParentControl.SelectedTab = P;

            SetVerbs();

        }


        void OnRemovePage(Object sender, EventArgs e)
        {
            EbTabControl ParentControl = (EbTabControl)Control;
            Control.ControlCollection oldTabs = ParentControl.Controls;

            if (ParentControl.SelectedIndex < 0) return;

            RaiseComponentChanging(TypeDescriptor.GetProperties(ParentControl)["TabPages"]);

            DesignerHost.DestroyComponent(ParentControl.TabPages[ParentControl.SelectedIndex]);

            RaiseComponentChanged(TypeDescriptor.GetProperties(ParentControl)["TabPages"], oldTabs, ParentControl.TabPages);

            SelectionService.SetSelectedComponents(new IComponent[] { ParentControl }, SelectionTypes.Auto);

            SetVerbs();

        }


        private void SetVerbs()
        {
            EbTabControl ParentControl = (EbTabControl)Control;

            switch (ParentControl.TabPages.Count)
            {
                case 0:
                    Verbs[1].Enabled = false;
                    Verbs[2].Enabled = false;
                    break;
                case 1:
                    Verbs[1].Enabled = false;
                    Verbs[2].Enabled = true;
                    break;
                default:
                    Verbs[1].Enabled = true;
                    Verbs[2].Enabled = true;
                    break;
            }
        }

        private const int WM_NCHITTEST = 0x84;

        private const int HTTRANSPARENT = -1;
        private const int HTCLIENT = 1;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
            {
                //select tabcontrol when Tabcontrol clicked outside of TabItem.
                if (m.Result.ToInt32() == HTTRANSPARENT)
                    m.Result = (IntPtr)HTCLIENT;
            }

        }

        private enum TabControlHitTest
        {
            TCHT_NOWHERE = 1,
            TCHT_ONITEMICON = 2,
            TCHT_ONITEMLABEL = 4,
            TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL
        }

        private const int TCM_HITTEST = 0x130D;

        private struct TCHITTESTINFO
        {
            public Point pt;
            public TabControlHitTest flags;
        }

        protected override bool GetHitTest(Point point)
        {
            if (this.SelectionService.PrimarySelection == this.Control)
            {
                TCHITTESTINFO hti = new TCHITTESTINFO();

                hti.pt = this.Control.PointToClient(point);
                hti.flags = 0;

                Message m = new Message();
                m.HWnd = this.Control.Handle;
                m.Msg = TCM_HITTEST;

                IntPtr lparam = Marshal.AllocHGlobal(Marshal.SizeOf(hti));
                Marshal.StructureToPtr(hti, lparam, false);
                m.LParam = lparam;

                base.WndProc(ref m);
                Marshal.FreeHGlobal(lparam);

                if (m.Result.ToInt32() != -1)
                    return hti.flags != TabControlHitTest.TCHT_NOWHERE;

            }

            return false;
        }


        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            //Don't want DrawGrid dots.
        }


        //Fix the AllSizable selectiorule on DockStyle.Fill
        public override System.Windows.Forms.Design.SelectionRules SelectionRules
        {
            get
            {
                if (Control.Dock == DockStyle.Fill)
                    return System.Windows.Forms.Design.SelectionRules.Visible;
                return base.SelectionRules;
            }
        }


    }

}
