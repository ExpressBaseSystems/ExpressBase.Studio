namespace ExpressBase.Studio.DesignerForms
{
    partial class ReportSettingsForm
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
            this.cmbPageSize = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenReportDesigner = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.intBottom = new System.Windows.Forms.NumericUpDown();
            this.intRight = new System.Windows.Forms.NumericUpDown();
            this.intLeft = new System.Windows.Forms.NumericUpDown();
            this.intTop = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radLandscape = new System.Windows.Forms.RadioButton();
            this.radPortrait = new System.Windows.Forms.RadioButton();
            this.cmbEbDataSource = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intTop)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbPageSize
            // 
            this.cmbPageSize.FormattingEnabled = true;
            this.cmbPageSize.Location = new System.Drawing.Point(38, 35);
            this.cmbPageSize.Name = "cmbPageSize";
            this.cmbPageSize.Size = new System.Drawing.Size(299, 21);
            this.cmbPageSize.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Page Size";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnOpenReportDesigner
            // 
            this.btnOpenReportDesigner.Location = new System.Drawing.Point(38, 492);
            this.btnOpenReportDesigner.Name = "btnOpenReportDesigner";
            this.btnOpenReportDesigner.Size = new System.Drawing.Size(212, 23);
            this.btnOpenReportDesigner.TabIndex = 3;
            this.btnOpenReportDesigner.Text = "Open Report Designer";
            this.btnOpenReportDesigner.UseVisualStyleBackColor = true;
            this.btnOpenReportDesigner.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.intBottom);
            this.groupBox1.Controls.Add(this.intRight);
            this.groupBox1.Controls.Add(this.intLeft);
            this.groupBox1.Controls.Add(this.intTop);
            this.groupBox1.Location = new System.Drawing.Point(38, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 127);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Margins";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Bottom";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(162, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Right";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(162, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Left";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Top";
            // 
            // intBottom
            // 
            this.intBottom.DecimalPlaces = 2;
            this.intBottom.Location = new System.Drawing.Point(17, 96);
            this.intBottom.Name = "intBottom";
            this.intBottom.Size = new System.Drawing.Size(120, 20);
            this.intBottom.TabIndex = 0;
            this.intBottom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // intRight
            // 
            this.intRight.DecimalPlaces = 2;
            this.intRight.Location = new System.Drawing.Point(165, 96);
            this.intRight.Name = "intRight";
            this.intRight.Size = new System.Drawing.Size(120, 20);
            this.intRight.TabIndex = 0;
            this.intRight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // intLeft
            // 
            this.intLeft.DecimalPlaces = 2;
            this.intLeft.Location = new System.Drawing.Point(165, 35);
            this.intLeft.Name = "intLeft";
            this.intLeft.Size = new System.Drawing.Size(120, 20);
            this.intLeft.TabIndex = 0;
            this.intLeft.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // intTop
            // 
            this.intTop.DecimalPlaces = 2;
            this.intTop.Location = new System.Drawing.Point(14, 35);
            this.intTop.Name = "intTop";
            this.intTop.Size = new System.Drawing.Size(120, 20);
            this.intTop.TabIndex = 0;
            this.intTop.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(262, 492);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radLandscape);
            this.groupBox2.Controls.Add(this.radPortrait);
            this.groupBox2.Location = new System.Drawing.Point(38, 62);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(299, 48);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Orientation";
            // 
            // radLandscape
            // 
            this.radLandscape.AutoSize = true;
            this.radLandscape.Location = new System.Drawing.Point(165, 19);
            this.radLandscape.Name = "radLandscape";
            this.radLandscape.Size = new System.Drawing.Size(78, 17);
            this.radLandscape.TabIndex = 1;
            this.radLandscape.TabStop = true;
            this.radLandscape.Text = "Landscape";
            this.radLandscape.UseVisualStyleBackColor = true;
            // 
            // radPortrait
            // 
            this.radPortrait.AutoSize = true;
            this.radPortrait.Checked = true;
            this.radPortrait.Location = new System.Drawing.Point(79, 20);
            this.radPortrait.Name = "radPortrait";
            this.radPortrait.Size = new System.Drawing.Size(58, 17);
            this.radPortrait.TabIndex = 0;
            this.radPortrait.TabStop = true;
            this.radPortrait.Text = "Portrait";
            this.radPortrait.UseVisualStyleBackColor = true;
            // 
            // cmbEbDataSource
            // 
            this.cmbEbDataSource.FormattingEnabled = true;
            this.cmbEbDataSource.Location = new System.Drawing.Point(38, 278);
            this.cmbEbDataSource.Name = "cmbEbDataSource";
            this.cmbEbDataSource.Size = new System.Drawing.Size(299, 21);
            this.cmbEbDataSource.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 262);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "ExpressBase DataSource";
            // 
            // ReportSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 536);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbEbDataSource);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOpenReportDesigner);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPageSize);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportSettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intTop)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPageSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnOpenReportDesigner;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown intBottom;
        private System.Windows.Forms.NumericUpDown intRight;
        private System.Windows.Forms.NumericUpDown intLeft;
        private System.Windows.Forms.NumericUpDown intTop;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radLandscape;
        private System.Windows.Forms.RadioButton radPortrait;
        private System.Windows.Forms.ComboBox cmbEbDataSource;
        private System.Windows.Forms.Label label6;
    }
}