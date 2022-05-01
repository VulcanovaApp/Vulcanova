using System.Linq;
using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Homework
{
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
                        ex => ex.GroupBy(x => x.AnswerDeadline)
                            .OrderBy(g => g.Key)
                            .Select(g => new HomeworkGroup(g.Key, g.ToList())))
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.ForceRefreshHomeworks, v => v.RefreshView)
                    .DisposeWith(disposable);
            });
        }
    }
}