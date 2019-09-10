using OxyPlot;

using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

using OxyPlot.Series;

namespace VainZero.WpfReportPrinting.Demo.Reports
{
    public sealed class HelloWorldPageViewModel : BindableBase
    {
        private int dataNumber = 200;
        private double frequency = 50.0;
        private double amplitude = 1.0;

        private string _title = "OxtPlot Sample";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public HelloWorldPageViewModel()
        {
            MakeLineData();
            MakeChart();
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
            //PModel.Title = "詳細情報(PlotView & Code)";

            var series1 = new LineSeries
            {
                Title = "速度",
                ItemsSource = DataPoints1,
                DataFieldX = "X",
                DataFieldY = "Y",
                Color = OxyColor.Parse("#4CAF50"),
                MarkerSize = 1,
                MarkerFill = OxyColor.Parse("#F55FFFFF"),
                MarkerStroke = OxyColor.Parse("#4CAF50"),
                MarkerStrokeThickness = 1.5,
                MarkerType = MarkerType.Cross,
                StrokeThickness = 1,
            };
            PModel.Series.Add(series1);

            var series2 = new LineSeries
            {
                Title = "回転数",
                ItemsSource = DataPoints2,
                DataFieldX = "X",
                DataFieldY = "Y",
                Color = OxyColor.Parse("#FF0000"),
                MarkerSize = 1,
                MarkerFill = OxyColor.Parse("#00FFFFFF"),
                MarkerStroke = OxyColor.Parse("#FF0000"),
                MarkerStrokeThickness = 0.0,
                MarkerType = MarkerType.Cross,
                StrokeThickness = 1,
            };
            PModel.Series.Add(series2);
        }

    }

    public sealed class HelloWorldReport
        : IReport
    {
        public string ReportName => "指導書";

        IReadOnlyList<object> Pages { get; } =
            new object[]
            {
                new HelloWorldPageViewModel(),
            };

        public IReadOnlyList<object> Paginate(Size size)
        {
            return Pages;
        }

    }

}
