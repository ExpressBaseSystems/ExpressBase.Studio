﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    [ProtoBuf.ProtoContract]
    public class EbTableLayoutPanel: TableLayoutPanel, IEbControl
    {
        [ProtoBuf.ProtoMember(1)]
        public EbObject EbObject { get; set; }

        [ProtoBuf.ProtoMember(2)]
        [Browsable(false)]
        public IEbControl[] Controls2 { get; set; }

        [Obsolete("For protobuf-net serialization purposes only")]
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
            this.Controls2 = new IEbControl[this.Controls.Count];
            this.Controls.CopyTo(this.Controls2, 0);

            this.EbObject.Size = this.Size;
            this.EbObject.Location = this.Location;
            this.EbObject.Dock = this.Dock;

            (this.EbObject as EbTableLayout).ColumnCount = this.ColumnCount;
            (this.EbObject as EbTableLayout).RowCount = this.RowCount;

            foreach (Control c in this.Controls)
            {
                var position = this.GetCellPosition(c);
                (this.EbObject as EbTableLayout).CellPositionRow = position.Row;
                (this.EbObject as EbTableLayout).CellPositionColumn = position.Column;
            }
        }

        [ProtoBuf.ProtoAfterDeserialization]
        private void AfterDeserialization()
        {
            if (this.Controls2 == null)
                this.Controls2 = new IEbControl[0];

            if (this.EbObject == null)
                this.EbObject = new EbTableLayout();

            this.Size = this.EbObject.Size;
            this.Location = this.EbObject.Location;
            this.Dock = this.EbObject.Dock;
            this.Visible = true;
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, IEbControl serialized_ctrl)
        {
            this.EbObject.IEbControl = this;
            this.RowCount = (serialized_ctrl.EbObject as EbTableLayout).RowCount;
            this.ColumnCount = (serialized_ctrl.EbObject as EbTableLayout).ColumnCount;
            this.Refresh();

            foreach (IEbControl c in serialized_ctrl.Controls2)
            {
                var ctrl = designer.ActiveDesignSurface.CreateControl(c.GetType(), c.EbObject.Size, c.EbObject.Location) as System.Windows.Forms.Control;
                ctrl.Parent = this;
                this.SetCellPosition(ctrl, new TableLayoutPanelCellPosition(c.EbObject.CellPositionRow, c.EbObject.CellPositionColumn));
                (ctrl as IEbControl).DoDesignerLayout(designer, c);
            }
        }

        public void DoDesignerRefresh() { }
    }
}
