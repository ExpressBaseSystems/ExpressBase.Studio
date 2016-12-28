using ExpressBase.UI;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace ExpressBase.Studio.Controls
{
    public interface IEbControl
    {
        EbObject EbObject { get; set; }

        void BeforeSerialization();

        void DoDesignerLayout(pF.pDesigner.IpDesigner designer, EbObject serialized_ctrl);

        void DoDesignerRefresh();
    }

//    [ProtoBuf.ProtoContract]
//    [ProtoBuf.ProtoInclude(1000, typeof(EbButton))]
//    [ProtoBuf.ProtoInclude(1001, typeof(EbTableLayout))]
//    [ProtoBuf.ProtoInclude(1002, typeof(EbChart))]
//    [ProtoBuf.ProtoInclude(1003, typeof(EbDataGridView))]
//    public class EbObject
//    {
//        [ProtoBuf.ProtoMember(1)]
//        [Browsable(false)]
//        public virtual List<EbObject> Controls { get; set; }

//        [ProtoBuf.ProtoMember(2)]
//        [Browsable(false)]
//        public string TargetType { get; set; }

//        [ProtoBuf.ProtoMember(3)]
//        public string Name { get; set; }

//        [ProtoBuf.ProtoMember(4)]
//        public string Label { get; set; }

//        [ProtoBuf.ProtoMember(5)]
//        public string HelpText { get; set; }

//        [ProtoBuf.ProtoMember(6)]
//        public string ToolTipText { get; set; }

//        //[ProtoBuf.ProtoMember(5)]
//        [Browsable(false)]
//        public DockStyle Dock { get; set; }

//        [ProtoBuf.ProtoMember(7)]
//        public int CellPositionRow { get; set; }

//        [ProtoBuf.ProtoMember(8)]
//        public int CellPositionColumn { get; set; }

//        [Browsable(false)]
//        public Size Size { get; set; }

//        //[ProtoBuf.ProtoMember(8)]
//        //[Browsable(false)]
//        //public string SizeSerialized
//        //{
//        //    get { return Size.ToString(); }
//        //    set
//        //    {
//        //        string[] coords = value.Replace("{Width=", string.Empty).Replace("Height=", string.Empty).Replace("}", string.Empty).Split(',');
//        //        Size = new Size(int.Parse(coords[0]), int.Parse(coords[1]));
//        //    }
//        //}

//        [Browsable(false)]
//        public Point Location { get; set; }

//        //[ProtoBuf.ProtoMember(9)]
//        //[Browsable(false)]
//        //public string LocationSerialized
//        //{
//        //    get { return Location.ToString(); }
//        //    set
//        //    {
//        //        string[] coords = value.Replace("{X=", string.Empty).Replace("Y=", string.Empty).Replace("}", string.Empty).Split(',');
//        //        Location = new Point(int.Parse(coords[0]), int.Parse(coords[1]));
//        //    }
//        //}

//        //[Browsable(false)]
//        //public IEbControl IEbControl { get; set; }

//        public EbObject()
//        {
//            //this.Dock = DockStyle.Fill;
//        }

//        //public EbObject(IEbControl parent)
//        //{
//        //    this.IEbControl = parent;
//        //}
//    }

//    [ProtoBuf.ProtoContract]
//    public class EbButton : EbObject
//    {
//        public EbButton() { }
//    }

//    [ProtoBuf.ProtoContract]
//    public class EbTableLayout : EbObject
//    {
//        [ProtoBuf.ProtoMember(1)]
//        public int RowCount { get; set; }

//        [ProtoBuf.ProtoMember(2)]
//        public int ColumnCount { get; set; }

//        public EbTableLayout() { }
//    }

//    [ProtoBuf.ProtoContract]
//    public class EbChart : EbObject
//    {
//        [ProtoBuf.ProtoMember(1)]
//        public int Id { get; set; }

//        [ProtoBuf.ProtoMember(2)]
//        public string ChartType { get; set; }

//        [ProtoBuf.ProtoMember(3)]
//        public int DataSourceId { get; set; }

//        public EbChart() { }

//        public string GetHtml()
//        {
//            return string.Format(@"
//<div style='height: auto; width: 50%; display: inline-block; '>
//    <div>
//        <select id='ctype'>
//            <option value='line'>Line</option>
//            <option value='line'>Pie</option >
//            <option value='line'>Doughnut</option >
//        </select>
//    </div>
//    <canvas id='chartContainer'></canvas>
//</div>
//<script>
//        $.get('/ds/data/{0}?format=json', function(data) {
//                var Ydatapoints = [];
//                var Xdatapoints = [];
//            $.each(data.data, function(i, value) {
//                    Xdatapoints.push(value[1]);
//                    Ydatapoints.push(value[2]);
//                });
//                var ctx = document.getElementById('chartContainer');
//                Chart.defaults.global.hover.mode = 'nearest';
//                var myChart = new Chart(ctx, {
//            type: '{1}',
//            data:
//                {
//                labels: Xdatapoints,
//                datasets: [{
//                    label: '# Exchange Rates',
//                    xLabels:['one', 'two'],
//                    data: Ydatapoints,
//                    backgroundColor: [
//                        'rgba(255, 99, 132, 0.8)',
//                        'rgba(54, 162, 235, 0.8)',
//                        'rgba(255, 206, 86, 0.8)',
//                        'rgba(75, 192, 192, 0.8)',
//                        'rgba(153, 102, 255, 0.8)',
//                        'rgba(255, 159, 64, 0.8)'
//                    ],
//                    borderColor: [
//                        'rgba(255,99,132,1)',
//                        'rgba(54, 162, 235, 1)',
//                        'rgba(255, 206, 86, 1)',
//                        'rgba(75, 192, 192, 1)',
//                        'rgba(153, 102, 255, 1)',
//                        'rgba(255, 159, 64, 1)'
//                    ],
//                    borderWidth: 4
//                }]
//            },
//            zoom: { enabled: true },
//            options: {
//                    hover: { mode: 'index' },
//                    scales: { yAxes: [{ responsive: true, ticks: { beginAtZero: true } }] }
//            }
//            });
//        });
//    </script>
//", this.DataSourceId, this.ChartType);
//        }
//    }

//    [ProtoBuf.ProtoContract]
//    public class EbDataGridView : EbObject
//    {
//        [ProtoBuf.ProtoMember(1)]
//        public int DataSourceId { get; set; }
//    }
}