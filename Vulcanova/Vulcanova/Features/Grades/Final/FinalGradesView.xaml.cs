using System.Reactive.Disposables;
using ReactiveUI;
using Vulcanova.Features.Grades.Summary;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Linq;

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

                this.BindCommand(ViewModel, vm => vm.ForceRefreshGrades, v => v.RefreshView)
                    .DisposeWith(disposable);

                this.WhenAnyValue(v => v.PeriodId)
                    .WhereNotNull()
                    .Subscribe((val) => ViewModel!.PeriodId = val!.Value)
                    .DisposeWith(disposable);

                ViewModel?.GetFinalGrades.IsExecuting
                    .Subscribe((value) =>
                    {
                        if (!value)
                        {
                            RefreshView.IsRefreshing = false;
                        }
                    })
                    .DisposeWith(disposable);
            });
        }
    }
}