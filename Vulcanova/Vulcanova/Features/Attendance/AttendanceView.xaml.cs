using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Attendance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendanceView
    {
        public AttendanceView()
        {
            InitializeComponent();
            
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, vm => vm.SelectedDay, v => v.Calendar.SelectedDate)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.CurrentDayEntries, v => v.EntriesList.ItemsSource)
                    .DisposeWith(disposable);
                
                this.OneWayBind(ViewModel, vm => vm.SelectedLesson, v => v.DetailsView.Lesson)
                    .DisposeWith(disposable);

                this.WhenAnyValue(v => v.ViewModel.SelectedLesson)
                    .Skip(1)
                    .Subscribe(sub => Panel.Open = sub != null)
                    .DisposeWith(disposable);

                this.WhenAnyValue(v => v.Panel.Open)
                    .Where(val => val == false)
                    .Subscribe(_ => ViewModel!.SelectedLesson = null)
                    .DisposeWith(disposable);
                
                this.WhenAnyObservable(v => v.ViewModel.GetTimetableEntries.IsExecuting)
                    .Select(v => !v)
                    .BindTo(NoElementsView, x => x.IsVisible)
                    .DisposeWith(disposable);
            });
        }
    }
}