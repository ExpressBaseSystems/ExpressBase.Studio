using ExpressBase.Studio.Controls;
using ExpressBase.Studio.DesignerForms;
using System.Drawing.Design;
using System.Windows.Forms;

namespace ExpressBase.Studio
{
    public partial class Toolbox : ToolWindow
    {
        public Toolbox()
        {
            InitializeComponent();

            ToolboxItem toolPointer = new System.Drawing.Design.ToolboxItem();
            toolPointer.DisplayName = "<Pointer>";
            toolPointer.Bitmap = new System.Drawing.Bitmap(16, 16);
            listBox1.Items.Add(toolPointer);
            //- the controls
            listBox1.Items.Add(new ToolboxItem(typeof(EbButtonControl)));
            listBox1.Items.Add(new ToolboxItem(typeof(ListView)));
            listBox1.Items.Add(new ToolboxItem(typeof(TreeView)));
            listBox1.Items.Add(new ToolboxItem(typeof(EbTextBoxControl)));
            listBox1.Items.Add(new ToolboxItem(typeof(EbPasswordControl)));
            listBox1.Items.Add(new ToolboxItem(typeof(NumericUpDown)));
            listBox1.Items.Add(new ToolboxItem(typeof(MaskedTextBox)));
            listBox1.Items.Add(new ToolboxItem(typeof(RadioButton)));
            listBox1.Items.Add(new ToolboxItem(typeof(DateTimePicker)));
            listBox1.Items.Add(new ToolboxItem(typeof(PictureBox)));
            listBox1.Items.Add(new ToolboxItem(typeof(RichTextBox)));
            listBox1.Items.Add(new ToolboxItem(typeof(Label)));
            listBox1.Items.Add(new ToolboxItem(typeof(LinkLabel)));
            listBox1.Items.Add(new ToolboxItem(typeof(OpenFileDialog)));
            listBox1.Items.Add(new ToolboxItem(typeof(CheckBox)));
            listBox1.Items.Add(new ToolboxItem(typeof(ComboBox)));
            listBox1.Items.Add(new ToolboxItem(typeof(GroupBox)));
            listBox1.Items.Add(new ToolboxItem(typeof(ImageList)));
            //listBox1.Items.Add(new ToolboxItem(typeof(EbTabControl)));
            listBox1.Items.Add(new ToolboxItem(typeof(EbChartControl)));
            listBox1.Items.Add(new ToolboxItem(typeof(EbDataGridViewControl)));
            listBox1.Items.Add(new ToolboxItem(typeof(EbTableLayoutPanel)));
            listBox1.Items.Add(new ToolboxItem(typeof(EbSplitContainer)));
        }
    }
}