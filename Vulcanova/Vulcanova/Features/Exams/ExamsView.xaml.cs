using System.Linq;
using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Exams
{
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
                        ex => ex.GroupBy(x => x.Deadline)
                            .OrderBy(g => g.Key)
                            .Select(g => new ExamsGroup(g.Key, g.ToList())))
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.ForceRefreshExams, v => v.RefreshView)
                    .DisposeWith(disposable);
            });
        }
    }
}