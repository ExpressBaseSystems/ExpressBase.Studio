using System;
using pF.pDesigner;
using ExpressBase.UI;
using System.Windows.Forms;
using System.ComponentModel;

namespace ExpressBase.Studio.Controls
{
    public class EbDataGridViewControl : System.Windows.Forms.DataGridView, IEbControl
    {
        public EbControl EbControl { get; set; }

        [Browsable(false)]
        new public DataGridViewColumnCollection Columns { get; set; }

        public EbDataGridViewControl() { }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbControl == null)
            {
                this.EbControl = new EbDataGridView();
                (this.EbControl as EbDataGridView).ColumnsChanged += EbDataGridViewControl_ColumnsChanged;
            }
            this.EbControl.Name = this.Name;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        private void EbDataGridViewControl_ColumnsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var col = (e.NewItems[0] as EbDataGridViewColumn);
                this.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = col.Name,
                    HeaderText = col.Label,
                    Width = col.Width
                });
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                var col = (e.NewItems[0] as EbDataGridViewColumn);
                this.Columns.Remove(col.Name);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                this.Columns.Clear();
            }
        }

        //required
        public void BeforeSerialization()
        {
            this.EbControl.TargetType = this.GetType().FullName;
            this.EbControl.Name = this.Name;
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbControl serialized_ctrl)
        {
            this.EbControl = serialized_ctrl;
            this.Name = serialized_ctrl.Name;
            this.Dock = DockStyle.Fill;
            this.Text = serialized_ctrl.Label;

            foreach (EbDataGridViewColumn col in (serialized_ctrl as EbDataGridView).Columns)
            {
                this.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = col.Name,
                    HeaderText = col.Label,
                    Width = col.Width
                });
            }
        }

        public void DoDesignerRefresh()
        {
            this.Name = this.EbControl.Name;
            this.Dock = DockStyle.Fill;
            this.Text = this.EbControl.Label;
        }
    }
}
