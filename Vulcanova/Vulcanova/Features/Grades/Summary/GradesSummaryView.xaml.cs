using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

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

                this.OneWayBind(ViewModel, vm => vm.CurrentSubject, v => v.DetailsView.Subject)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.CurrentSubject, v => v.Panel.Open, s => s != null)
                    .DisposeWith(disposable);

                this.WhenAnyValue(v => v.PeriodId)
                    .WhereNotNull()
                    .InvokeCommand(this, v => v.ViewModel.GetGrades)
                    .DisposeWith(disposable);
            });
        }
    }
}