using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace VainZero.WpfReportPrinting.Demo.Reports
{
    public sealed class ShidouReportPageViewModel : BindableBase
    {
        private int dataNumber = 200;
        private double frequency = 50.0;
        private double amplitude = 1.0;

        private string _title = "OxtPlot Sample";
        #region Properties

        public PlotModel Model { get; } = new PlotModel();
        public PlotController Controller { get; } = new PlotController();

        public OxyPlot.Axes.TimeSpanAxis X { get; } = new OxyPlot.Axes.TimeSpanAxis();
        public OxyPlot.Axes.LinearAxis Y { get; } = new OxyPlot.Axes.LinearAxis();
        public OxyPlot.Series.LineSeries LineSeries { get; private set; }
        public OxyPlot.Series.FunctionSeries FunctionSeries { get; private set; }

        public ObservableCollection<TestData> Samples { get; private set; }

        #endregion

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ShidouReportPageViewModel()
        {
            MakeLineData();
            MakeChart();
            //画像ファイルに保存
            using (var stream = new FileStream("output_1.png", FileMode.Create))
            {
                var gwidth = 400;
                var gheight = 300;

                OxyPlot.Wpf.PngExporter.Export(Model, stream, gwidth, gheight, OxyColor.FromArgb(0xff, 0xff, 0xff, 0xff));
            }
        }

        //表示用のラインデータを２本作成
        public void MakeLineData()
        {
            var points1 = new List<DataPoint>();
            for (int i = 0; i < dataNumber; i++)
            {
                points1.Add(new DataPoint((double)i, amplitude * Math.Sin(2.0 * Math.PI * i / frequency)));
            }
            DataPoints1 = points1;

            var points2 = new List<DataPoint>();
            for (int i = 0; i < dataNumber; i++)
            {
                points2.Add(new DataPoint((double)i, amplitude / 2 * Math.Sin(2.0 * Math.PI * i * 2 / frequency)));
            }
            DataPoints2 = points2;

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
                new TestData { Time = new TimeSpan(0, 0, 11), Value = 20, Tag = "L" },
                new TestData { Time = new TimeSpan(0, 0, 12), Value = 40, Tag = "M" },
                new TestData { Time = new TimeSpan(0, 0, 13), Value = 60, Tag = "N" },
                new TestData { Time = new TimeSpan(0, 0, 14), Value = 0, Tag = "O" },
                new TestData { Time = new TimeSpan(0, 0, 15), Value = 20, Tag = "P" },
                new TestData { Time = new TimeSpan(0, 0, 16), Value = 0, Tag = "Q" },
                new TestData { Time = new TimeSpan(0, 0, 17), Value = 20, Tag = "R" },
                new TestData { Time = new TimeSpan(0, 0, 18), Value = 40, Tag = "S" },
                new TestData { Time = new TimeSpan(0, 0, 19), Value = 60, Tag = "T" },
                new TestData { Time = new TimeSpan(0, 0, 20), Value = 50, Tag = "U" },
                new TestData { Time = new TimeSpan(0, 0, 21), Value = 0, Tag = "V" },
                new TestData { Time = new TimeSpan(0, 0, 22), Value = 40, Tag = "M" },
                new TestData { Time = new TimeSpan(0, 0, 23), Value = 60, Tag = "X" },
                new TestData { Time = new TimeSpan(0, 0, 24), Value = 30, Tag = "Y" },
                new TestData { Time = new TimeSpan(0, 0, 25), Value = 0, Tag = "Z" },

            };
        }



        //1本目のラインデータ
        private List<DataPoint> dataPoints1;
        public List<DataPoint> DataPoints1
        {
            get { return dataPoints1; }
            set { SetProperty(ref dataPoints1, value); }
        }

        //２本目のラインデータ
        private List<DataPoint> dataPoints2;
        public List<DataPoint> DataPoints2
        {
            get { return dataPoints2; }
            set { SetProperty(ref dataPoints2, value); }
        }

        private PlotModel pModel = new PlotModel();
        public PlotModel PModel
        {
            get { return pModel; }
            set { SetProperty(ref pModel, value); }
        }

        public void MakeChart()
        {
            // 軸の初期化
            X.Position = OxyPlot.Axes.AxisPosition.Bottom;
            Y.Position = OxyPlot.Axes.AxisPosition.Left;

            // 線グラフ
            LineSeries = new OxyPlot.Series.LineSeries();
            LineSeries.Title = "速度";
            LineSeries.ItemsSource = Samples;
            LineSeries.DataFieldX = nameof(TestData.Time);
            LineSeries.DataFieldY = nameof(TestData.Value);
            Model.Axes.Add(X);
            Model.Axes.Add(Y);
            Model.Series.Add(LineSeries);
            //Model.Series.Add(FunctionSeries);

            X.Position = AxisPosition.Bottom;
            X.MajorGridlineColor = OxyColor.FromArgb(0xCC, 0xE3, 0xE3, 0xE3);
            X.MajorGridlineStyle = LineStyle.Solid;
            X.MajorGridlineThickness = 1;
            X.MinorGridlineColor = OxyColor.FromArgb(0x99, 0xE3, 0xE3, 0xE3);
            X.MinorGridlineStyle = LineStyle.Dot;
            X.MinorGridlineThickness = 1;

            var min = 0;
            var absMin = -10;
            var max = 100;
            var absMax = 120;

            // 表示領域
            X.Minimum = min;
            X.AbsoluteMinimum = absMin;
            X.Maximum = max;
            X.AbsoluteMaximum = absMax;

            var minRange = 10;
            var maxRange = 200;

            // 表示サイズの最大最小
            X.MinimumRange = minRange;
            X.MaximumRange = maxRange;
            Y.Position = AxisPosition.Left;
            Y.MajorGridlineColor = OxyColor.FromArgb(0xCC, 0xE3, 0xE3, 0xE3);
            Y.MajorGridlineStyle = LineStyle.Solid;
            Y.MajorGridlineThickness = 1;
            Y.MinorGridlineColor = OxyColor.FromArgb(0x99, 0xE3, 0xE3, 0xE3);
            Y.MinorGridlineStyle = LineStyle.Dot;
            Y.MinorGridlineThickness = 1;

            var ymin = 0;
            var yabsMin = -10;
            var ymax = 50;
            var yabsMax = 60;

            // 表示領域
            Y.Minimum = ymin;
            Y.AbsoluteMinimum = yabsMin;
            Y.Maximum = ymax;
            Y.AbsoluteMaximum = yabsMax;

            var yminRange = 10;
            var ymaxRange = 70;

            // 表示サイズの最大最小
            Y.MinimumRange = yminRange;
            Y.MaximumRange = ymaxRange;


            Model.InvalidatePlot(true);


            //PModel.Title = "詳細情報(PlotView & Code)";

            //var series1 = new LineSeries
            //{
            //    Title = "速度",
            //    ItemsSource = DataPoints1,
            //    DataFieldX = "X",
            //    DataFieldY = "Y",
            //    Color = OxyColor.Parse("#4CAF50"),
            //    MarkerSize = 1,
            //    MarkerFill = OxyColor.Parse("#F55FFFFF"),
            //    MarkerStroke = OxyColor.Parse("#4CAF50"),
            //    MarkerStrokeThickness = 1.5,
            //    MarkerType = MarkerType.Cross,
            //    StrokeThickness = 1,
            //};
            //PModel.Series.Add(series1);

            //var series2 = new LineSeries
            //{
            //    Title = "回転数",
            //    ItemsSource = DataPoints2,
            //    DataFieldX = "X",
            //    DataFieldY = "Y",
            //    Color = OxyColor.Parse("#FF0000"),
            //    MarkerSize = 1,
            //    MarkerFill = OxyColor.Parse("#00FFFFFF"),
            //    MarkerStroke = OxyColor.Parse("#FF0000"),
            //    MarkerStrokeThickness = 0.0,
            //    MarkerType = MarkerType.Cross,
            //    StrokeThickness = 1,
            //};
            //PModel.Series.Add(series2);


        }

    }

    public sealed class ShidouReport
        : IReport
    {
        public string ReportName => "指導書";

        IReadOnlyList<object> Pages { get; } =
            new object[]
            {
                new ShidouReportPageViewModel(),
            };

        public IReadOnlyList<object> Paginate(Size size)
        {
            return Pages;
        }


    }
    public class TestData
    {
        public TimeSpan Time { get; set; }
        public double Value { get; set; }
        public string Tag { get; set; }
    }
}
