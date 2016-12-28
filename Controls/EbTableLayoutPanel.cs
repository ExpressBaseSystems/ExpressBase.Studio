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

        [ProtoBuf.ProtoBeforeSerialization]
        private void BeforeSerialization()
        {
            this.EbControl.TargetType = this.GetType().FullName;
            this.EbControl.Controls = new List<EbControl>();
            foreach (IEbControl e in this.Controls)
            {
                e.BeforeSerialization();
                this.EbControl.Controls.Add(e.EbControl);
            }

            (this.EbControl as EbTableLayout).ColumnCount = this.ColumnCount;
            (this.EbControl as EbTableLayout).RowCount = this.RowCount;

            foreach (Control c in this.Controls)
            {
                var position = this.GetCellPosition(c);
                (this.EbControl as EbTableLayout).CellPositionRow = position.Row;
                (this.EbControl as EbTableLayout).CellPositionColumn = position.Column;
            }
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControl serialized_ctrl)
        {
            this.RowCount = (serialized_ctrl as EbTableLayout).RowCount;
            this.ColumnCount = (serialized_ctrl as EbTableLayout).ColumnCount;
            this.Refresh();

            foreach (IEbControl c in serialized_ctrl.Controls)
            {
                var ctrl = designer.ActiveDesignSurface.CreateControl(c.GetType()) as System.Windows.Forms.Control;
                ctrl.Parent = this;
                this.SetCellPosition(ctrl, new TableLayoutPanelCellPosition(c.EbControl.CellPositionRow, c.EbControl.CellPositionColumn));
                (ctrl as IEbControl).DoDesignerLayout(designer, c.EbControl);
            }
        }

        public void DoDesignerRefresh() { }

        void IEbControl.BeforeSerialization()
        {
            throw new NotImplementedException();
        }
    }
}
