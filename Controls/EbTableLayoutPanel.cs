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
        }

        public void BeforeSerialization()
        {
            this.EbControl.TargetType = this.GetType().FullName;
            this.EbControl.Controls = new List<EbControl>();
            foreach (IEbControl e in this.Controls)
            {
                e.BeforeSerialization();
                var position = this.GetCellPosition(e as Control);
                e.EbControl.CellPositionRow = position.Row;
                e.EbControl.CellPositionColumn = position.Column;
                this.EbControl.Controls.Add(e.EbControl);
            }

            (this.EbControl as EbTableLayout).ColumnCount = this.ColumnCount;
            (this.EbControl as EbTableLayout).RowCount = this.RowCount;

            //foreach (Control c in this.Controls)
            //{
            //    var position = this.GetCellPosition(c);
            //    (this.EbControl as EbTableLayout).CellPositionRow = position.Row;
            //    (this.EbControl as EbTableLayout).CellPositionColumn = position.Column;
            //}
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControl serialized_ctrl)
        {
            this.RowCount = (serialized_ctrl as EbTableLayout).RowCount;
            this.ColumnCount = (serialized_ctrl as EbTableLayout).ColumnCount;
            this.Refresh();

            foreach (EbControl c in serialized_ctrl.Controls)
            {
                var ctrl = designer.ActiveDesignSurface.CreateControl(Type.GetType(c.TargetType)) as System.Windows.Forms.Control;
                ctrl.Parent = this;
                this.SetCellPosition(ctrl, new TableLayoutPanelCellPosition(c.CellPositionRow, c.CellPositionColumn));
                (ctrl as IEbControl).DoDesignerLayout(designer, c);
            }
        }

        public void DoDesignerRefresh() { }
    }
}
