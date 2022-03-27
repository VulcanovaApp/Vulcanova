using System.Linq;
using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Homeworks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeworksView
    {
        public HomeworksView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, vm => vm.SelectedDay, v => v.Calendar.SelectedDate)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.CurrentWeekEntries, v => v.EntriesList.ItemsSource,
                        ex => ex.GroupBy(x => x.AnswerDeadline)
                            .OrderBy(g => g.Key)
                            .Select(g => new HomeworksGroup(g.Key, g.ToList())))
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.ForceRefreshHomeworks, v => v.RefreshView)
                    .DisposeWith(disposable);
            });
        }
    }
}