using ExpressBase.Objects;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    public class EbTableLayoutPanel: TableLayoutPanel, IEbControlContainer
    {
        public EbControlContainer EbControlContainer { get; set; }

        public EbTableLayoutPanel() { }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbControlContainer == null)
                this.EbControlContainer = new EbTableLayout();

            this.EbControlContainer.Name = this.Name;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        public void BeforeSerialization()
        {
            this.EbControlContainer.TargetType = this.GetType().FullName;
            this.EbControlContainer.Name = this.Name;
            this.EbControlContainer.Controls = new List<EbControl>();
            foreach (IEbControl e in this.Controls)
            {
                e.BeforeSerialization();
                var position = this.GetCellPosition(e as Control);
                e.EbControl.CellPositionRow = position.Row;
                e.EbControl.CellPositionColumn = position.Column;
                this.EbControlContainer.Controls.Add(e.EbControl);
            }

            (this.EbControlContainer as EbTableLayout).Columns = new List<EbTableLayoutColumn>();
            foreach (ColumnStyle style in this.ColumnStyles)
                (this.EbControlContainer as EbTableLayout).Columns.Add(new EbTableLayoutColumn { Index=this.ColumnStyles.IndexOf(style), Width=Convert.ToInt32(style.Width) });

            (this.EbControlContainer as EbTableLayout).Rows = new List<EbTableRow>();
            foreach (RowStyle style in this.RowStyles)
                (this.EbControlContainer as EbTableLayout).Rows.Add(new EbTableRow { Index = this.RowStyles.IndexOf(style), Height = Convert.ToInt32(style.Height) });
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControlContainer serialized_ctrl)
        {
            this.EbControlContainer = serialized_ctrl;
            this.Name = serialized_ctrl.Name;
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            this.SuspendLayout();
            this.ColumnCount = 0;
            this.RowCount = 0;
            this.ColumnStyles.Clear();
            this.RowStyles.Clear();
            
            foreach (EbTableLayoutColumn c in (serialized_ctrl as EbTableLayout).Columns)
                this.ColumnStyles.Add(new ColumnStyle { SizeType = SizeType.Percent, Width = c.Width });

            foreach (EbTableRow r in (serialized_ctrl as EbTableLayout).Rows)
                this.RowStyles.Add(new RowStyle { SizeType = SizeType.Percent, Height = r.Height });

            this.ColumnCount = (serialized_ctrl as EbTableLayout).Columns.Count;
            this.RowCount = (serialized_ctrl as EbTableLayout).Rows.Count;

            if (serialized_ctrl.Controls != null)
            {
                foreach (EbControl c in serialized_ctrl.Controls)
                {
                    var ctrl = designer.ActiveDesignSurface.CreateControl(Type.GetType(c.TargetType)) as System.Windows.Forms.Control;
                    ctrl.Parent = this;
                    this.SetCellPosition(ctrl, new TableLayoutPanelCellPosition(c.CellPositionColumn, c.CellPositionRow));
                    (ctrl as IEbControl).DoDesignerLayout(designer, c);
                }
            }

            this.ResumeLayout(true);
        }

        public void DoDesignerRefresh()
        {
            this.Name = this.EbControlContainer.Name;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
        }
    }
}
