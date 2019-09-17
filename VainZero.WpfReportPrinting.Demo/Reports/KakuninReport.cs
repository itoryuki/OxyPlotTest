using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace VainZero.WpfReportPrinting.Demo.Reports
{
    //public sealed class OrderItem
    //{
    //    static Random Random { get; } =
    //        new Random();

    //    public string Name { get; }
    //    public int Count { get; }
    //    public int UnitPrice { get; }
    //    public int TotalPrice { get; }
    //    public string Note { get; }

    //    public OrderItem(string name, int unitPrice)
    //    {
    //        Name = name;
    //        Count = 1;
    //        UnitPrice = unitPrice;
    //        TotalPrice = unitPrice;

    //        // ページネーションの難度を上げるために、ランダムな行数の備考を生成する。
    //        Note =
    //            string.Join(
    //                Environment.NewLine,
    //                Enumerable.Range(0, Random.Next(0, 3)).Select(i => $"{i + 1}行目")
    //            );
    //    }
    //}

    public sealed class KakuninReportPageViewModel
    {
        //public string TargetName { get; }
        //public DateTime OrderDate { get; }
        //public int TotalPrice { get; }

        private int dataNumber = 10;

        //public PlotModel Model { get; } = new PlotModel();
        //public PlotController Controller { get; } = new PlotController();

        public OxyPlot.Axes.TimeSpanAxis X { get; } = new OxyPlot.Axes.TimeSpanAxis();
        public OxyPlot.Axes.LinearAxis Y { get; } = new OxyPlot.Axes.LinearAxis();
        public OxyPlot.Series.LineSeries LineSeries { get; private set; }
        public OxyPlot.Series.FunctionSeries FunctionSeries { get; private set; }

        public ObservableCollection<TestData> Samples { get; private set; }

        public
            KakuninReportPageViewModel()
        {
            //TargetName = targetName;
            //OrderDate = orderDate;
            //TotalPrice = totalPrice;
            MakeLineData();
            MakeChart();
            //画像ファイルに保存
            using (var stream = new FileStream("output_2.png", FileMode.Create))
            {
                var gwidth = 400;
                var gheight = 300;

                OxyPlot.Wpf.PngExporter.Export(Model1, stream, gwidth, gheight, OxyColor.FromArgb(0xff, 0xff, 0xff, 0xff));
            }
        }

        //表示用のラインデータを２本作成
        public void MakeLineData()
        {



            var points1 = new List<DataPoint>();
            for (int i = 0; i < dataNumber; i++)
            {
                //points1.Add(new DataPoint((double)i, amplitude * Math.Sin(2.0 * Math.PI * i / frequency)));
            }
            //DataPoints1 = points1;



            Samples = new ObservableCollection<TestData>
            {
                new TestData { Time = new TimeSpan(0, 0, 0), Value = 20, Tag = "A" },
                new TestData { Time = new TimeSpan(0, 0, 1), Value = 20, Tag = "B" },
                new TestData { Time = new TimeSpan(0, 0, 2), Value = 40, Tag = "C" },
                new TestData { Time = new TimeSpan(0, 0, 3), Value = 60, Tag = "D" },
                new TestData { Time = new TimeSpan(0, 0, 4), Value = 0, Tag = "E" },
                new TestData { Time = new TimeSpan(0, 0, 5), Value = 20, Tag = "F" },
                new TestData { Time = new TimeSpan(0, 0, 6), Value = 0, Tag = "G" },
                new TestData { Time = new TimeSpan(0, 0, 7), Value = 20, Tag = "H" },
                new TestData { Time = new TimeSpan(0, 0, 8), Value = 40, Tag = "I" },
                new TestData { Time = new TimeSpan(0, 0, 9), Value = 60, Tag = "J" },
                new TestData { Time = new TimeSpan(0, 0, 10), Value = 0, Tag = "K" },


            };
        }



        //1本目のラインデータ
       // private List<DataPoint> dataPoints1;
        //public List<DataPoint> DataPoints1
        //{
        //    get { return dataPoints1; }
            //set { SetProperty(ref dataPoints1, value); }
        //}




        public void MakeChart()
        {
            // 発進停止診断
            // Create some data
            this.Items = new Collection<Item>
                            {
                                new Item {Label = "ゆっくり発進", Value1 = 37, Value2 = 0, Value3 = 0},
                                new Item {Label = "安全停止", Value1 = 7, Value2 = 0, Value3 = 0},
                                //new Item {Label = "Bananas", Value1 = 23, Value2 = 2, Value3 = 29}
                            };

            // Create the plot model
            var tmp = new PlotModel { Title = "発進停止診断", LegendPlacement = LegendPlacement.Outside, LegendPosition = LegendPosition.RightTop, LegendOrientation = LegendOrientation.Vertical };

            // Add the axes, note that MinimumPadding and AbsoluteMinimum should be set on the value axis.
            tmp.Axes.Add(new CategoryAxis { Position = AxisPosition.Left, ItemsSource = this.Items, LabelField = "Label" });
            tmp.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, AbsoluteMinimum = 0 });

            // Add the series, note that the BarSeries are using the same ItemsSource as the CategoryAxis.
            tmp.Series.Add(new BarSeries { Title = "2009", ItemsSource = Items, ValueField = "Value1" });
            //tmp.Series.Add(new BarSeries { Title = "2010", ItemsSource = this.Items, ValueField = "Value2" });
            //tmp.Series.Add(new BarSeries { Title = "2011", ItemsSource = this.Items, ValueField = "Value3" });

            this.Model1 = tmp;

            Model1.InvalidatePlot(true);

            //this.DataContext = this;
        }


        public Collection<Item> Items { get; set; }

        public PlotModel Model1 { get; set; }
        //public PlotModel Model { get; } = new PlotModel();
        //public sealed class OrderFormPage
        //: ISingleHeaderedGridPage<OrderItem>
        //{
        //    public KakuninReportPageViewModel Header { get; }
        //    public IReadOnlyList<OrderItem> Items { get; }

        //    public int PageIndex { get; set; } = -1;
        //    public int PageCount { get; set; } = -1;

        //    public
        //        OrderFormPage(
        //            KakuninReportPageViewModel header,
        //            IReadOnlyList<OrderItem> items
        //        )
        //    {
        //        Header = header;
        //        //Items = items;
        //    }
        //}
    }
    public sealed class KakuninReport
        : IReport
    {
        public string ReportName => "確認書";

        //public KakuninReportPageViewModel Header { get; }
        IReadOnlyList<object> Pages { get; } =
            new object[]
            {
                 new KakuninReportPageViewModel (),
            };

        public IReadOnlyList<object> Paginate(Size size)
        {
            return Pages;
        }
        //public IReadOnlyList<OrderItem> Items { get; } =
        //    Enumerable.Range(1, 50)
        //    .Select(i => new OrderItem($"Item {i}", i * 100))
        //    .ToArray();

        // {
        //    new OrderFormPage(Header, Items)
        //    .Paginate(size)
        //    .Select(items => new OrderFormPage(Header, items))
        //    .ToArray();

        //// 各ページのページ番号・ページ数を設定する。
        //var pageIndex = 1;
        //foreach (var page in pages)
        //{
        //    page.PageIndex = pageIndex;
        //    page.PageCount = pages.Length;
        //    pageIndex++;
        //}

        //return pages;
        //}

        //public KakuninReport()
        //    {
        //        Header =
        //            new KakuninReportPageViewModel(
        //            );
        //    }
    }

    public class Item
    {
        public string Label { get; set; }
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double Value3 { get; set; }
    }
}
