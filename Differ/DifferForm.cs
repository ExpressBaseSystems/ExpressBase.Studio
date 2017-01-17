using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ExpressBase.Studio
{
    public partial class DifferForm : DockContent
    {
        public DifferForm()
        {
            InitializeComponent();
            this.Text = "Differ";
            webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            webBrowser2.DocumentCompleted += WebBrowser2_DocumentCompleted;
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.Document.Window.Scroll += ScrollHandler;
        }

        private void WebBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser2.Document.Window.Scroll += ScrollHandler;
        }

        private void ScrollHandler(object sender, HtmlElementEventArgs e)
        {
            var scrolledBrowser = sender as HtmlWindow;
            if (scrolledBrowser == null) return;

            // here you can see where I needed to distinguish the browser windows
            // none of the document, window etc properties matched the sender, so I
            // resorted to this hacky way
            WebBrowser otherBrowser = (scrolledBrowser == webBrowser1.Document.Window) ? webBrowser2 : webBrowser1;
            int y = scrolledBrowser.Document.Body.ScrollRectangle.Top;
            otherBrowser.Document.Body.ScrollTop = y;

            int x = scrolledBrowser.Document.Body.ScrollRectangle.Left;
            otherBrowser.Document.Body.ScrollLeft = x;
        }

        string spaceValue = "\u00B7";
        string tabValue = "\u00B7\u00B7";

        private void Form1_Load(object sender, System.EventArgs e)
        {
            var d = new Differ();
            var inlineBuilder = new SideBySideDiffBuilder(d);
            var diffmodel = inlineBuilder.BuildDiffModel(OldText, NewText);

            webBrowser1.DocumentText = GetHtml2Render(diffmodel.OldText);
            webBrowser2.DocumentText = GetHtml2Render(diffmodel.NewText);

            webBrowser1.Refresh();
            webBrowser2.Refresh();
        }

        private string GetHtml2Render(DiffPaneModel text)
        {
            string html = "<body class="+"'diffpane'"+"><table cellpadding='0' cellspacing='0' class='diffTable'>";

            //webbrowser1
            foreach (var diffLine in text.Lines)
            {
                html += "<tr>";
                html += "<td class='lineNumber'>";
                html += diffLine.Position.HasValue ? diffLine.Position.ToString() : "&nbsp;";
                html += "</td>";
                html += "<td class='line " + diffLine.Type.ToString() + "Line'>";
                html += "<span class='lineText'>";

                if (diffLine.Type == ChangeType.Deleted || diffLine.Type == ChangeType.Inserted || diffLine.Type == ChangeType.Unchanged)
                {
                    html += diffLine.Text.Replace(" ", spaceValue.ToString()).Replace("\t", tabValue.ToString());
                }
                else if (diffLine.Type == ChangeType.Modified)
                {
                    foreach (var character in diffLine.SubPieces)
                    {
                        if (character.Type == ChangeType.Imaginary) continue;
                        else
                        {
                            html += "<span class='" + character.Type.ToString() + "Character'>";
                            html += character.Text.Replace(" ", spaceValue.ToString()).Replace("\t", tabValue.ToString());
                            html += "</span>";
                        }
                    }
                }

                html += "</span>";
                html += "</td>";
                html += "</tr>";
            }

            html += "</table></body>";

            return css + html;
        }

        private const string OldText = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace diff
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DIFF());
        }
  {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DIFF());
        }
    }
}
";

        private const string NewText = @"using System;
using System.Collections.Generic;
using System.Linq.hai;
asdfv
using System.Windows.Forms;
using system.web;
namespace diff
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DIFF());
        }
  {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DIFF());
        }
    }


";
        private string css = @"<head><style>
#diffBar
{
	width: 3%;
	height: 100%;
	float: left;
	position:relative;
	background: #DDDDDD;
}

.diffBarLineLeft, .diffBarLineRight
{
	width: 50%;
	float:left;
	height:0px;
	cursor:pointer;
}

.inView
{
	background-repeat: repeat;
}

#activeBar
{
	position:absolute;
	top:0px;
	background-color:#6699FF;
	opacity:0.5;
	filter:alpha(opacity= '50');
}


#diffBox
{
	margin-left: auto;
	margin-right: auto;
	border: solid 2px #000000;
}


#leftPane, #rightPane
{
	float: left;
	width: 50%;
}

.diffHeader
{
	font-weight: bold;
	padding: 2px 0px 2px 10px;
	background-color: #FFFFFF;
	text-align: center;
}
.diffPane
{
	margin-right: 0px;
	padding: 0px;
	overflow: auto;
	font-family: Consolas;
    font-size:xx-small;

}

.diffTable
{
	width: 100%;
	height: 100%;
}

.line
{
	padding-left: .2em;
	white-space: nowrap;
	width: 50%;
}

.lineNumber
{
	padding: 0 .3em;
	background-color: #FFFFFF;
	text-align: right;
}

.InsertedLine
{
	background-color: lightgreen;
}

.ModifiedLine
{
	background-color: #DCDCFF;
}

.DeletedLine
{
	background-color: #FFC864;
}

.UnchangedLine
{
	background-color: #FFFFFF;
}

.ImaginaryLine
{
	background-color: #C8C8C8;
}

.InsertedCharacter
{
	background-color: lightgreen;
}

.DeletedCharacter
{
	background-color: #C86464;
}

.UnchangedCharacter
{
}

.ImaginaryCharacter
{
}

.clear
{
	clear: both;
}
</style></head>";
    }
}
