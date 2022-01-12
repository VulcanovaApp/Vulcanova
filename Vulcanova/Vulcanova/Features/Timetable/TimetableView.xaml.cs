using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Timetable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimetableView
    {
        public TimetableView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, vm => vm.SelectedDay, v => v.Calendar.SelectedDate)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.CurrentDayEntries, v => v.EntriesList.ItemsSource)
                    .DisposeWith(disposable);
            });
        }
    }
}