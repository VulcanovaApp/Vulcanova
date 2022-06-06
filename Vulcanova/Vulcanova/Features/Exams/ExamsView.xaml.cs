using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
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
                        ex => ex?.GroupBy(x => x.Deadline)
                            .OrderBy(g => g.Key)
                            .Select(g => new ExamsGroup(g.Key, g.ToList())))
                    .DisposeWith(disposable);
                
                this.WhenAnyObservable(v => v.ViewModel.GetExams.IsExecuting)
                    .Select(v => !v)
                    .BindTo(NoElementsView, x => x.IsVisible)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.ForceRefreshExams, v => v.RefreshView)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.SelectedExam, v => v.DetailsView.Exam)
                    .DisposeWith(disposable);

                this.WhenAnyValue(v => v.ViewModel.SelectedExam)
                    .Skip(1)
                    .Subscribe(sub => Panel.Open = sub != null)
                    .DisposeWith(disposable);

                this.WhenAnyValue(v => v.Panel.Open)
                    .Where(val => val == false)
                    .Subscribe(_ => ViewModel!.SelectedExam = null)
                    .DisposeWith(disposable);
            });
        }
    }
}