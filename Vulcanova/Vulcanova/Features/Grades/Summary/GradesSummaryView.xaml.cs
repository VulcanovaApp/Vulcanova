using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Xaml;
using System;
using System.Reactive.Linq;

namespace Vulcanova.Features.Grades.Summary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradesSummaryView
    {
        public GradesSummaryView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, vm => vm.Grades, v => v.SubjectGrades.ItemsSource)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.ForceSyncGrades, v => v.RefreshView)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.IsSyncing, v => v.RefreshView.IsRefreshing)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.CurrentSubject, v => v.DetailsView.Subject)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.PreviousSemester, v => v.PreviousSemesterTap)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.NextSemester, v => v.NextSemesterTap)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.PeriodInfo, v => v.PeriodNameLabel.Text,
                    p => p is null ? string.Empty : $"{p.CurrentPeriod.Start.Year}/{p.CurrentPeriod.End.Year} – {p.CurrentPeriod.Number}");

                this.OneWayBind(ViewModel, vm => vm.PeriodInfo, v => v.NextPeriodImg.IsVisible, r => r.HasNext);

                this.OneWayBind(ViewModel, vm => vm.PeriodInfo, v => v.PreviousPeriodImg.IsVisible, r => r.HasPrevious);

                ViewModel.WhenAnyValue(vm => vm.CurrentSubject)
                    .Skip(1)
                    .Subscribe(sub => Panel.Open = sub != null)
                    .DisposeWith(disposable);
            });
        }
    }
}