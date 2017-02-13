using ExpressBase.Data;
using ExpressBase.Studio.Controls;
using ExpressBase.Studio.DesignerForms;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace ExpressBase.Studio
{
    public partial class Toolbox : ToolWindow
    {
        public Toolbox()
        {
            InitializeComponent();

            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox1.DrawItem += ListBox1_DrawItem;
            listBox1.BackColor = Color.LightGray;
            listBox1.ItemHeight = 24;
            listBox1.SelectionMode = SelectionMode.One;

            ToolboxItem toolPointer = new System.Drawing.Design.ToolboxItem();
            toolPointer.DisplayName = "<Pointer>";
            toolPointer.Bitmap = new System.Drawing.Bitmap(16, 16);
            listBox1.Items.Add(toolPointer);

            //- the controls
            listBox1.Items.Add(new ToolboxItem(typeof(EbButtonControl)) { DisplayName = "Button" });
            listBox1.Items.Add(new ToolboxItem(typeof(EbTextBoxControl)) { DisplayName = "TextBox", Bitmap = ExpressBase.Studio.Properties.Resources.txt });
            listBox1.Items.Add(new ToolboxItem(typeof(EbNumericControl)) { DisplayName = "Numeric" });
            listBox1.Items.Add(new ToolboxItem(typeof(EbDateControl)) { DisplayName = "Date", Bitmap = ExpressBase.Studio.Properties.Resources.dtime });
            listBox1.Items.Add(new ToolboxItem(typeof(EbComboBoxControl)) { DisplayName = "ComboBox", Bitmap = ExpressBase.Studio.Properties.Resources.cb });
            listBox1.Items.Add(new ToolboxItem(typeof(EbChartControl)) { DisplayName = "Chart" });
            listBox1.Items.Add(new ToolboxItem(typeof(EbDataGridViewControl)) { DisplayName = "DataGridView", Bitmap = ExpressBase.Studio.Properties.Resources.Grid });
            listBox1.Items.Add(new ToolboxItem(typeof(EbTableLayoutPanel)) { DisplayName = "TableLayout" });
            listBox1.Items.Add(new ToolboxItem(typeof(EbRadioGroupControl)) { DisplayName = "RadioGroup", Bitmap = ExpressBase.Studio.Properties.Resources.radio });

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

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            var item = listBox1.Items[e.Index] as ToolboxItem;
            ButtonRenderer.DrawButton(e.Graphics, e.Bounds, item.DisplayName,
                new Font(FontFamily.GenericSansSerif, 8f),
                item.Bitmap,
                new Rectangle(new Point(e.Bounds.Location.X + 10, e.Bounds.Location.Y + 5), new Size(16, 16)), false,
                System.Windows.Forms.VisualStyles.PushButtonState.Normal);
        }

        public void Redraw(ColumnColletion columns)
        {
            if (columns != null)
            {
                this.listBox1.Items.Clear();
                foreach (var col in columns)
                {
                    listBox1.Items.Add(new ToolboxItem(typeof(EbReportFieldControl)) { DisplayName = col.ColumnName });
                }
            }
        }
    }
}