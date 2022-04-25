using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms.Xaml;
using System;

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
                
                this.BindCommand(ViewModel, vm => vm.ForceRefreshTimetableEntries, v => v.RefreshView)
                    .DisposeWith(disposable);
                
                this.WhenAnyObservable(v => v.ViewModel.GetTimetableEntries.IsExecuting)
                    .Select(v => !v)
                    .BindTo(NoElementsView, x => x.IsVisible)
                    .DisposeWith(disposable);

                ViewModel?.ForceRefreshTimetableEntries.IsExecuting
                    .Subscribe(value =>
                    {
                        if (!value)
                        {
                            RefreshView.IsRefreshing = false;
                        }
                    })
                    .DisposeWith(disposable);
            });
        }
    }
}