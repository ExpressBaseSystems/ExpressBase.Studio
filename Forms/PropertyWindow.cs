using pF.DesignSurfaceManagerExt;
using System.Windows.Forms;

namespace ExpressBase.Studio
{
    public partial class PropertyWindow : ToolWindow
    {
        internal PropertyGridHost PropertyGridHost { get; set; }

        public PropertyWindow()
        {
            InitializeComponent();
            //comboBox.SelectedIndex = 0;
            //propertyGrid.SelectedObject = propertyGrid;
        }
    }
}