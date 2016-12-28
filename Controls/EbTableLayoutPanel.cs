using ExpressBase.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    public class EbTableLayoutPanel: TableLayoutPanel, IEbControl
    {
        public EbObject EbObject { get; set; }

        public EbTableLayoutPanel() { }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbObject == null)
                this.EbObject = new EbTableLayout();
        }

        [ProtoBuf.ProtoBeforeSerialization]
        private void BeforeSerialization()
        {
            this.EbObject.TargetType = this.GetType().FullName;
            this.EbObject.Controls = new List<EbObject>();
            foreach (IEbControl e in this.Controls)
            {
                e.BeforeSerialization();
                this.EbObject.Controls.Add(e.EbObject);
            }

            (this.EbObject as EbTableLayout).ColumnCount = this.ColumnCount;
            (this.EbObject as EbTableLayout).RowCount = this.RowCount;

            foreach (Control c in this.Controls)
            {
                var position = this.GetCellPosition(c);
                (this.EbObject as EbTableLayout).CellPositionRow = position.Row;
                (this.EbObject as EbTableLayout).CellPositionColumn = position.Column;
            }
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbObject serialized_ctrl)
        {
            this.RowCount = (serialized_ctrl as EbTableLayout).RowCount;
            this.ColumnCount = (serialized_ctrl as EbTableLayout).ColumnCount;
            this.Refresh();

            foreach (IEbControl c in serialized_ctrl.Controls)
            {
                var ctrl = designer.ActiveDesignSurface.CreateControl(c.GetType()) as System.Windows.Forms.Control;
                ctrl.Parent = this;
                this.SetCellPosition(ctrl, new TableLayoutPanelCellPosition(c.EbObject.CellPositionRow, c.EbObject.CellPositionColumn));
                (ctrl as IEbControl).DoDesignerLayout(designer, c.EbObject);
            }
        }

        public void DoDesignerRefresh() { }

        void IEbControl.BeforeSerialization()
        {
            throw new NotImplementedException();
        }
    }
}
