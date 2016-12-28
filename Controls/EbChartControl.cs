﻿using ExpressBase.UI;
using System;

namespace ExpressBase.Studio.Controls
{
    public class EbChartControl : System.Windows.Forms.DataVisualization.Charting.Chart, IEbControl
    {
        public EbObject EbObject { get; set; }

        public EbChartControl() { }

        //required
        public void BeforeSerialization()
        {
            this.EbObject.TargetType = this.GetType().FullName;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.EbObject == null)
                this.EbObject = new EbChart();
        }

        public void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbObject serialized_ctrl)
        {
            this.EbObject = serialized_ctrl;
            this.Name = serialized_ctrl.Name;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Text = serialized_ctrl.Label;
        }

        public void DoDesignerRefresh()
        {
            this.Name = this.EbObject.Name;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Text = this.EbObject.Label;
        }
    }
}
