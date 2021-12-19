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

                this.OneWayBind(ViewModel, vm => vm.CurrentSubject, v => v.DetailsView.Subject)
                    .DisposeWith(disposable);

                this.WhenAnyValue(v => v.ViewModel.CurrentSubject)
                    .Skip(1)
                    .Subscribe(sub => Panel.Open = sub != null)
                    .DisposeWith(disposable);

                this.WhenAnyValue(v => v.PeriodId)
                    .Skip(1)
                    .WhereNotNull()
                    .InvokeCommand(this, v => v.ViewModel.GetGrades)
                    .DisposeWith(disposable);
            });
        }
    }
}