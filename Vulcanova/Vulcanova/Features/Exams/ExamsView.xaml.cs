using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Vulcanova.Core.Layout;
using Vulcanova.Core.Rx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Exams;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ExamsView
{
    public ExamsView()
    {
        InitializeComponent();

        this.WhenActivated(disposable =>
        {
            this.Bind(ViewModel, vm => vm.SelectedDay, v => v.Calendar.SelectedDate)
                .DisposeWith(disposable);

            this.OneWayBind(ViewModel, vm => vm.CurrentWeekEntries, v => v.EntriesList.ItemsSource,
                    ex => ex?.GroupBy(x => x.Deadline)
                        .OrderBy(g => g.Key)
                        .Select(g => new ExamsGroup(g.Key, g.ToList())))
                .DisposeWith(disposable);
                
            this.WhenAnyObservable(v => v.ViewModel.GetExams.IsExecuting)
                .Select(v => !v)
                .BindTo(NoElementsView, x => x.IsVisible)
                .DisposeWith(disposable);

            this.BindForceRefresh(RefreshView, v => v.ViewModel.GetExams)
                .DisposeWith(disposable);

            this.OneWayBind(ViewModel, vm => vm.SelectedExam, v => v.DetailsView.Exam)
                .DisposeWith(disposable);

            if (Device.RuntimePlatform != Device.iOS)
            {
                UiExtensions.WireUpNonNativeSheet(ViewModel, DetailsView, Panel,
                        vm => vm.SelectedExam,
                        v => v.Exam)
                    .DisposeWith(disposable);
            }
        });
    }
}