using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using System;

namespace Vulcanova.Features.Grades.Summary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradesSummaryView
    {
        public static readonly BindableProperty PeriodIdProperty = BindableProperty.Create(
            nameof(PeriodId), typeof(int?), typeof(GradesSummaryView));

        public int? PeriodId
        {
            get => (int?) GetValue(PeriodIdProperty);
            set => SetValue(PeriodIdProperty, value);
        }

        public GradesSummaryView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, vm => vm.Grades, v => v.SubjectGrades.ItemsSource)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.ForceRefreshGrades, v => v.RefreshView)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.IsSyncing, v => v.RefreshView.IsRefreshing)
                    .DisposeWith(disposable);

                this.WhenAnyValue(v => v.PeriodId)
                    .WhereNotNull()
                    .Subscribe((val) => ViewModel!.PeriodId = val!.Value)
                    .DisposeWith(disposable);
            });
        }
    }
}