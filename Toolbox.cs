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
            listBox1.Items.Add(new ToolboxItem(typeof(EbButtonControl)) { DisplayName = "Button" });
            listBox1.Items.Add(new ToolboxItem(typeof(EbTextBoxControl)) { DisplayName = "TextBox" });
            listBox1.Items.Add(new ToolboxItem(typeof(EbNumericControl)) { DisplayName = "Numeric" });
            listBox1.Items.Add(new ToolboxItem(typeof(EbDateControl)) { DisplayName = "Date" });
            listBox1.Items.Add(new ToolboxItem(typeof(EbComboBoxControl)) { DisplayName = "ComboBox" });
            listBox1.Items.Add(new ToolboxItem(typeof(EbChartControl)) { DisplayName = "Chart" });
            listBox1.Items.Add(new ToolboxItem(typeof(EbDataGridViewControl)) { DisplayName = "DataGridView" });
            listBox1.Items.Add(new ToolboxItem(typeof(EbTableLayoutPanel)) { DisplayName = "TableLayout" });
            listBox1.Items.Add(new ToolboxItem(typeof(EbRadioGroupControl)) { DisplayName = "RadioGroup" });

            //listBox1.Items.Add(new ToolboxItem(typeof(EbTabControl)));
            //listBox1.Items.Add(new ToolboxItem(typeof(OpenFileDialog)));
            //listBox1.Items.Add(new ToolboxItem(typeof(CheckBox)));
            //listBox1.Items.Add(new ToolboxItem(typeof(RadioButton)));
            //listBox1.Items.Add(new ToolboxItem(typeof(GroupBox)));
            //listBox1.Items.Add(new ToolboxItem(typeof(PictureBox)));
            //listBox1.Items.Add(new ToolboxItem(typeof(RichTextBox)));
            //listBox1.Items.Add(new ToolboxItem(typeof(Label)));
            //listBox1.Items.Add(new ToolboxItem(typeof(LinkLabel)));
        }
    }
}