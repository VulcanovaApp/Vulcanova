using System.Reactive.Disposables;
using ReactiveUI;
using Vulcanova.Features.Grades.Summary;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Linq;
using Vulcanova.Core.Rx;

namespace Vulcanova.Features.Grades.Final
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinalGradesView
    {
        public static readonly BindableProperty PeriodIdProperty = BindableProperty.Create(
            nameof(PeriodId), typeof(int?), typeof(GradesSummaryView));

        public int? PeriodId
        {
            get => (int?) GetValue(PeriodIdProperty);
            set => SetValue(PeriodIdProperty, value);
        }

        public FinalGradesView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, vm => vm.FinalGrades, v => v.FinalGradesList.ItemsSource, grades =>
                    {
                        if (grades == null)
                        {
                            return null;
                        }

                        var finalGradesEntries = grades as FinalGradesEntry[] ?? grades.ToArray();
                        return finalGradesEntries.All(g => g.PredictedGrade == null && g.FinalGrade == null)
                            ? null
                            : finalGradesEntries;
                    })
                    .DisposeWith(disposable);

                this.BindForceRefresh(RefreshView, v => v.ViewModel.GetFinalGrades, true)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.FinalAverage, v => v.FinalAverage.Text,
                        value => value?.ToString("F2"))
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.FinalAverage, v => v.FinalAverageContainer.IsVisible,
                    value => value != null);

                this.WhenAnyValue(v => v.PeriodId)
                    .WhereNotNull()
                    .Subscribe((val) => ViewModel!.PeriodId = val!.Value)
                    .DisposeWith(disposable);
            });
        }
    }
}