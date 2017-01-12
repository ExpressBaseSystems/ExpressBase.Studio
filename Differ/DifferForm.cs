using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using FastColoredTextBoxNS;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ExpressBase.Studio
{
    public partial class DifferForm : DockContent
    {
        private Style greenStyle;
        private Style redStyle;
        private Style whiteStyle;
        private bool scrollflag1 = true;
        private bool scrollflag2 = true;

        public DifferForm()
        {
            InitializeComponent();
            this.Text = "Differ";
            greenStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Lime)));
            redStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Red)));
            whiteStyle= new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Transparent)));

            fastcoloredTextBox1.Scroll += FastcoloredTextBox1_Scroll;
            fastcoloredTextBox2.Scroll += FastcoloredTextBox2_Scroll;
        }

        private void FastcoloredTextBox1_Scroll(object sender, ScrollEventArgs e)
        {
            if (scrollflag1 == true)
            {
                scrollflag2 = false;
                fastcoloredTextBox2.OnScroll(e, false);
                scrollflag2 = true;
            }
        }

        private void FastcoloredTextBox2_Scroll(object sender, ScrollEventArgs e)
        {
            if (scrollflag2 == true)
            {
                scrollflag1 = false;  
                fastcoloredTextBox1.OnScroll(e, false);
                scrollflag1 = true;
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            var d = new Differ();
            var inlineBuilder = new SideBySideDiffBuilder(d);
            var diffmodel = inlineBuilder.BuildDiffModel(OldText, NewText);
            string spaceValue = "\u00B7";
            string tabValue = "\u00B7\u00B7";

            //richTextBox1
            foreach (var diffLine in diffmodel.OldText.Lines)
            {
                if (!string.IsNullOrEmpty(diffLine.Text))
                {
                    if (diffLine.Type == ChangeType.Deleted || diffLine.Type == ChangeType.Unchanged)
                    {
                        Style selectionColor = (diffLine.Type == ChangeType.Deleted) ? redStyle : whiteStyle;
                        fastcoloredTextBox1.AppendText(diffLine.Text.Replace(" ", spaceValue).Replace("\t", tabValue) + "\n", selectionColor);
                    }
                    else if (diffLine.Type == ChangeType.Modified)
                    {
                        foreach (var character in diffLine.SubPieces)
                        {
                            if (character.Type == ChangeType.Imaginary) continue;
                            Style selectionColor = (character.Type == ChangeType.Deleted) ? redStyle : whiteStyle;
                            fastcoloredTextBox1.AppendText(character.Text.Replace(" ", spaceValue.ToString()), selectionColor);
                        }
                    }
                }
            }

            //richTextBox2
            foreach (var diffLine in diffmodel.NewText.Lines)
            {
                if (!string.IsNullOrEmpty(diffLine.Text))
                {
                    if (diffLine.Type == ChangeType.Inserted || diffLine.Type == ChangeType.Unchanged)
                    {
                        Style selectionColor = (diffLine.Type == ChangeType.Inserted) ? greenStyle : whiteStyle;
                        fastcoloredTextBox2.AppendText(diffLine.Text.Replace(" ", spaceValue).Replace("\t", tabValue) + "\n", selectionColor);
                    }
                    else if (diffLine.Type == ChangeType.Modified)
                    {
                        foreach (var character in diffLine.SubPieces)
                        {
                            if (character.Type == ChangeType.Imaginary) continue;
                            Style selectionColor = (character.Type == ChangeType.Inserted) ? greenStyle : whiteStyle;
                            fastcoloredTextBox2.AppendText(character.Text.Replace(" ", spaceValue.ToString()), selectionColor);
                        }
                    }
                    else if (diffLine.Type == ChangeType.Deleted)
                    {
                        fastcoloredTextBox2.AppendText("\r\n");
                    }
                }
            }
        }

        private const string OldText = @"using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Tester.DiffMergeStuffs;

namespace Tester
{
    public partial class DiffMergeSample : Form
    {
        int updating;
        Style greenStyle;
        Style redStyle;

        public DiffMergeSample()
        {
            InitializeComponent();
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Tester.DiffMergeStuffs;

namespace Tester
{
    public partial class DiffMergeSample : Form
    {
        int updating;
        Style greenStyle;
        Style redStyle;

        public DiffMergeSample()
        {
            InitializeComponent();
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Tester.DiffMergeStuffs;

namespace Tester
{
    public partial class DiffMergeSample : Form
    {
        int updating;
        Style greenStyle;
        Style redStyle;

        public DiffMergeSample()
        {
            InitializeComponent();
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Tester.DiffMergeStuffs;

namespace Tester
{
    public partial class DiffMergeSample : Form
    {
        int updating;
        Style greenStyle;
        Style redStyle;

        //public DiffMergeSample()//
        {
            InitializeComponent();

";

        private const string NewText = @"using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Tester.DiffMergeStuffs;

namespace Tester
{
    public partial class DiffMergeSample : Form
    {
        int updating;
         Style redStyle;
        Style greenStyle;
        Style redStyle;

        public DiffMergeSample()
        {
            InitializeComponent();
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Tester.DiffMergeStuffs;

namespace Tester
{
    public partial class DiffMergeSample : Form
    {
        int updating;
        Style greenStyle;
        Style redStyle;

        public DiffMergeSample()
        {
            InitializeComponent();
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Tester.DiffMergeStuffs;

namespace Tester
{using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Tester.DiffMergeStuffs;

namespace Tester
{
    public partial class DiffMergeSample : Form
    {
        int updating;
        Style greenStyle;
        Style redStyle;

        public DiffMergeSample()
        {
            InitializeComponent();using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Tester.DiffMergeStuffs;

namespace Tester
{
    public partial class DiffMergeSample : Form
    {
        int updating;
        Style greenStyle;
        Style redStyle;

        public DiffMergeSample()
        {
            InitializeComponent();
    public partial class DiffMergeSample : Form
    {
        int updating;
        Style greenStyle;
        Style redStyle;

        public DiffMergeSample()
        {
            InitializeComponent();
";
  }
}
