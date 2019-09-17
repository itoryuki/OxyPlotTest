using Reactive.Bindings;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace VainZero.WpfReportPrinting.Demo.Reports
{
    public sealed class ReportSelector
    {
        public IReadOnlyList<IReport> Reports { get; } =
            new IReport[]
            {
                new KakuninReport(),
                new ShidouReport(),
            };

        public ReactiveProperty<IReport> SelectedReport { get; }

        public ReportSelector()
        {
            SelectedReport =
                new ReactiveProperty<IReport>(Reports.Last());
        }
    }
}
