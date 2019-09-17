using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows.Input;
using VainZero.WpfReportPrinting.Demo.Printing;
using VainZero.WpfReportPrinting.Demo.Reports;
using Xware.ComponentModel;

namespace VainZero.WpfReportPrinting.Demo.Previewing
{
    public sealed class Previewer : BindableBase
    {
        public IReadOnlyReactiveProperty<IReport> Report { get; }

        public IReadOnlyReactiveProperty<IReadOnlyList<object>> Pages { get; }

        readonly ReactiveCommand printCommand =
            new ReactiveCommand();

        public ICommand PrintCommand => printCommand;

        public MediaSizeSelector MediaSizeSelector { get; } =
            new MediaSizeSelector();

        public void Print()
        {
            var report = Report.Value;
            var pageSize = MediaSizeSelector.SelectedItem.Value.Size;

            var printer = new Printer();
            printer.Print(report, pageSize);
        }

        public Previewer(IReadOnlyReactiveProperty<IReport> report)
        {
            Report = report;

            Pages =
                Report.CombineLatest(
                    MediaSizeSelector.SelectedSize,
                    (r, pageSize) => r.Paginate(pageSize)
                )
                .ToReadOnlyReactiveProperty();

            // 印刷ボタンが押されたら印刷する。
            printCommand.Subscribe(_ => Print());
        }

        #region 表示プロパティ制御
        /// <summary>
        /// プレビューエリア
        /// </summary>
        private bool _previewVisibility;
        /// <summary>
        ///
        /// </summary>
        public bool PreviewVisibility
        {
            get
            {
                return _previewVisibility;
            }

            set
            {
                if (_previewVisibility == value)
                {
                    return;
                }

                _previewVisibility = value;
                RaisePropertyChanged(nameof(PreviewVisibility));
            }
        }
        #endregion

    }
}
