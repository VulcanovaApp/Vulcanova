using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Vulcanova.Core.Layout;
using Vulcanova.Core.Rx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Homework;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class HomeworkView
{
    public HomeworkView()
    {
        InitializeComponent();

        this.WhenActivated(disposable =>
        {
            this.Bind(ViewModel, vm => vm.SelectedDay, v => v.Calendar.SelectedDate)
                .DisposeWith(disposable);

            this.OneWayBind(ViewModel, vm => vm.CurrentWeekEntries, v => v.EntriesList.ItemsSource,
                    ex => ex.GroupBy(x => x.Deadline)
                        .OrderBy(g => g.Key)
                        .Select(g => new HomeworkGroup(g.Key, g.ToList())))
                .DisposeWith(disposable);
                
            this.OneWayBind(ViewModel, vm => vm.SelectedHomework, v => v.DetailsView.Homework)
                .DisposeWith(disposable);

            if (Device.RuntimePlatform != Device.iOS)
            {
                UiExtensions.WireUpNonNativeSheet(ViewModel, DetailsView, Panel,
                        vm => vm.SelectedHomework,
                        v => v.Homework)
                    .DisposeWith(disposable);
            }

            this.WhenAnyObservable(v => v.ViewModel.GetHomeworkEntries.IsExecuting)
                .Select(v => !v)
                .BindTo(NoElementsView, x => x.IsVisible)
                .DisposeWith(disposable);

            this.BindForceRefresh(RefreshView, v => v.ViewModel.GetHomeworkEntries)
                .DisposeWith(disposable);
        });
    }
}