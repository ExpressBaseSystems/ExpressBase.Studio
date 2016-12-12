namespace pF.DesignSurfaceManagerExt {
    partial class PropertyGridHost {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing ) {
            if( disposing && (components != null) ) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.pgrdPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.pgrdComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // pgrdPropertyGrid
            // 
            this.pgrdPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgrdPropertyGrid.Location = new System.Drawing.Point( 0, 24 );
            this.pgrdPropertyGrid.Name = "pgrdPropertyGrid";
            this.pgrdPropertyGrid.Size = new System.Drawing.Size( 203, 298 );
            this.pgrdPropertyGrid.TabIndex = 3;
            // 
            // pgrdComboBox
            // 
            this.pgrdComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pgrdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pgrdComboBox.FormattingEnabled = true;
            this.pgrdComboBox.Location = new System.Drawing.Point( 0, 0 );
            this.pgrdComboBox.Name = "pgrdComboBox";
            this.pgrdComboBox.Size = new System.Drawing.Size( 203, 24 );
            this.pgrdComboBox.TabIndex = 2;
            // 
            // PropertyGridHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 8F, 16F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.pgrdPropertyGrid );
            this.Controls.Add( this.pgrdComboBox );
            this.Name = "PropertyGridHost";
            this.Size = new System.Drawing.Size( 203, 322 );
            this.ResumeLayout( false );

        }

        #endregion

        protected System.Windows.Forms.PropertyGrid pgrdPropertyGrid;
        protected System.Windows.Forms.ComboBox pgrdComboBox;

    }
}
