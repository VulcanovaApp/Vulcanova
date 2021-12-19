using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Grades
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradesView
    {
        public GradesView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, vm => vm.SelectedViewModelIndex, v => v.TabHost.SelectedIndex)
                    .DisposeWith(disposable);

                this.Bind(ViewModel, vm => vm.SelectedViewModelIndex, v => v.Switcher.SelectedIndex)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.GradesSummaryViewModel, v => v.GradesSummaryView.ViewModel)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.PreviousSemester, v => v.PreviousSemesterTap)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.NextSemester, v => v.NextSemesterTap)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.PeriodInfo, v => v.PeriodNameLabel.Text,
                        p => p is null
                            ? string.Empty
                            : $"{p.CurrentPeriod.Start.Year}/{p.CurrentPeriod.End.Year} – {p.CurrentPeriod.Number}")
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.PreviousSemester, v => v.PreviousSemesterTap)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.NextSemester, v => v.NextSemesterTap)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.PeriodInfo, v => v.NextPeriodImg.IsVisible, r => r?.HasNext)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.PeriodInfo, v => v.PreviousPeriodImg.IsVisible, r => r?.HasPrevious)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.PeriodInfo, v => v.GradesSummaryView.PeriodId,
                        r => r?.CurrentPeriod?.Id)
                    .DisposeWith(disposable);
            });
        }
    }
}