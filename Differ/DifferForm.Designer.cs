using FastColoredTextBoxNS;

namespace ExpressBase.Studio
{
    partial class DifferForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DifferForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fastcoloredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.fastcoloredTextBox2 = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastcoloredTextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastcoloredTextBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fastcoloredTextBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fastcoloredTextBox2);
            this.splitContainer1.Size = new System.Drawing.Size(712, 470);
            this.splitContainer1.SplitterDistance = 341;
            this.splitContainer1.TabIndex = 0;
            // 
            // fastcoloredTextBox1
            // 
            this.fastcoloredTextBox1.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fastcoloredTextBox1.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.fastcoloredTextBox1.AutoSize = true;
            this.fastcoloredTextBox1.BackBrush = null;
            this.fastcoloredTextBox1.CharHeight = 14;
            this.fastcoloredTextBox1.CharWidth = 8;
            this.fastcoloredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastcoloredTextBox1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastcoloredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastcoloredTextBox1.FoldingIndicatorColor = System.Drawing.Color.LightGreen;
            this.fastcoloredTextBox1.IsReplaceMode = false;
            this.fastcoloredTextBox1.Location = new System.Drawing.Point(0, 0);
            this.fastcoloredTextBox1.Name = "fastcoloredTextBox1";
            this.fastcoloredTextBox1.Paddings = new System.Windows.Forms.Padding(0);
            this.fastcoloredTextBox1.ReadOnly = true;
            this.fastcoloredTextBox1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastcoloredTextBox1.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastcoloredTextBox1.ServiceColors")));
            this.fastcoloredTextBox1.Size = new System.Drawing.Size(341, 470);
            this.fastcoloredTextBox1.TabIndex = 0;
            this.fastcoloredTextBox1.TabLength = 1;
            this.fastcoloredTextBox1.Zoom = 100;
            // 
            // fastcoloredTextBox2
            // 
            this.fastcoloredTextBox2.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fastcoloredTextBox2.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.fastcoloredTextBox2.BackBrush = null;
            this.fastcoloredTextBox2.CharHeight = 14;
            this.fastcoloredTextBox2.CharWidth = 8;
            this.fastcoloredTextBox2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastcoloredTextBox2.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastcoloredTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastcoloredTextBox2.IsReplaceMode = false;
            this.fastcoloredTextBox2.Location = new System.Drawing.Point(0, 0);
            this.fastcoloredTextBox2.Name = "fastcoloredTextBox2";
            this.fastcoloredTextBox2.Paddings = new System.Windows.Forms.Padding(0);
            this.fastcoloredTextBox2.ReadOnly = true;
            this.fastcoloredTextBox2.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastcoloredTextBox2.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastcoloredTextBox2.ServiceColors")));
            this.fastcoloredTextBox2.Size = new System.Drawing.Size(367, 470);
            this.fastcoloredTextBox2.TabIndex = 0;
            this.fastcoloredTextBox2.Zoom = 100;            
            
            // DIFF
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 470);
            this.Controls.Add(this.splitContainer1);
            this.Name = "DIFF";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastcoloredTextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastcoloredTextBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private FastColoredTextBox fastcoloredTextBox2;
        private FastColoredTextBox fastcoloredTextBox1;
    }
}

