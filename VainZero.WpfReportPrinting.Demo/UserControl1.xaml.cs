using System.Windows.Controls;
using VainZero.WpfReportPrinting.Demo.Reports;

namespace VainZero.WpfReportPrinting.Demo
{
    /// <summary>
    /// UserControl1.xaml の相互作用ロジック
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public ShidouReportPageViewModel Sample1 { get; } = new ShidouReportPageViewModel();
        public UserControl1()
        {
            InitializeComponent();
            this.DataContext = this;
            //Sample1.Init();
        }
    }
}
