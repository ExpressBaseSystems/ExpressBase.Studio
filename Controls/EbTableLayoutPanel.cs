using ExpressBase.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    public class EbTableLayoutPanel: TableLayoutPanel, IEbControl
    {
        public EbControl EbControl { get; set; }

        public EbTableLayoutPanel() { }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbControl == null)
                this.EbControl = new EbTableLayout();

            this.EbControl.Name = this.Name;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            (this.EbControl as EbTableLayout).ColumnCount = this.ColumnCount;
            (this.EbControl as EbTableLayout).RowCount = this.RowCount;
        }

        public void BeforeSerialization()
        {
            this.EbControl.TargetType = this.GetType().FullName;
            this.EbControl.Name = this.Name;
            (this.EbControl as EbTableLayout).ColumnCount = this.ColumnCount;
            (this.EbControl as EbTableLayout).RowCount = this.RowCount;
            this.EbControl.Controls = new List<EbControl>();
            foreach (IEbControl e in this.Controls)
            {
                e.BeforeSerialization();
                var position = this.GetCellPosition(e as Control);
                e.EbControl.CellPositionRow = position.Row;
                e.EbControl.CellPositionColumn = position.Column;
                this.EbControl.Controls.Add(e.EbControl);
            }

            (this.EbControl as EbTableLayout).Columns = new List<EbTableColumn>();
            foreach (ColumnStyle style in this.ColumnStyles)
                (this.EbControl as EbTableLayout).Columns.Add(new EbTableColumn { Index=this.ColumnStyles.IndexOf(style), Width=Convert.ToInt32(style.Width) });
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControl serialized_ctrl)
        {
            this.EbControl = serialized_ctrl;
            this.RowCount = (serialized_ctrl as EbTableLayout).RowCount;
            this.ColumnCount = (serialized_ctrl as EbTableLayout).ColumnCount;
            this.Name = serialized_ctrl.Name;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Refresh();

            if (serialized_ctrl.Controls != null)
            {
                foreach (EbControl c in serialized_ctrl.Controls)
                {
                    var ctrl = designer.ActiveDesignSurface.CreateControl(Type.GetType(c.TargetType)) as System.Windows.Forms.Control;
                    ctrl.Parent = this;
                    this.SetCellPosition(ctrl, new TableLayoutPanelCellPosition(c.CellPositionRow, c.CellPositionColumn));
                    (ctrl as IEbControl).DoDesignerLayout(designer, c);
                }
            }
        }

        public void DoDesignerRefresh()
        {
            this.Name = this.EbControl.Name;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
        }
    }
}
