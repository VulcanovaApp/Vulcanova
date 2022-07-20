using System.Reactive.Disposables;
using ReactiveUI;
using Vulcanova.Core.Layout;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Grades;

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

            this.OneWayBind(ViewModel, vm => vm.FinalGradesViewModel, v => v.FinalGradesView.ViewModel)
                .DisposeWith(disposable);

            this.BindCommand(ViewModel, vm => vm.PreviousSemester, v => v.PreviousSemesterTap)
                .DisposeWith(disposable);

            this.BindCommand(ViewModel, vm => vm.NextSemester, v => v.NextSemesterTap)
                .DisposeWith(disposable);

            this.OneWayBind(ViewModel, vm => vm.PeriodInfo, v => v.PeriodNameLabel.Text,
                    p => p is null
                        ? string.Empty
                        : $"{p.YearStart}/{p.YearEnd} – {p.CurrentPeriod.Number}")
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
                
            this.OneWayBind(ViewModel, vm => vm.PeriodInfo, v => v.FinalGradesView.PeriodId,
                    r => r?.CurrentPeriod?.Id)
                .DisposeWith(disposable);

            if (Device.RuntimePlatform != Device.iOS)
            {
                UiExtensions.WireUpNonNativeSheet(ViewModel,
                    DetailsView,
                    Panel,
                    vm => vm.GradesSummaryViewModel.CurrentSubject,
                    v => v.Subject).DisposeWith(disposable);
            }
        });
    }
}