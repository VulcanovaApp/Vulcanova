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
                this.OneWayBind(ViewModel, vm => vm.FinalGrades, v => v.FinalGradesList.ItemsSource)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.FinalGrades, v => v.NoElementsMessage.IsVisible,
                        grades => grades?.All(g => g.PredictedGrade == null && g.FinalGrade == null))
                    .DisposeWith(disposable);

                this.WhenAnyValue(v => v.NoElementsMessage.IsVisible)
                    .Subscribe(val => FinalGradesList.IsVisible = !val)
                    .DisposeWith(disposable);

                this.WhenAnyValue(v => v.PeriodId)
                    .WhereNotNull()
                    .Subscribe((val) => ViewModel!.PeriodId = val!.Value)
                    .DisposeWith(disposable);
            });
        }
    }
}