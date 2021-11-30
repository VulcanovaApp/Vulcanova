using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Xaml;
using System;
using System.Reactive.Linq;

namespace Vulcanova.Features.Grades.Summary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradesSummaryView
    {
        public GradesSummaryView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, vm => vm.Grades, v => v.SubjectGrades.ItemsSource)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.ForceSyncGrades, v => v.RefreshView)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.IsSyncing, v => v.RefreshView.IsRefreshing)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.CurrentSubject, v => v.DetailsView.Subject)
                    .DisposeWith(disposable);

                ViewModel.WhenAnyValue(vm => vm.CurrentSubject)
                    .Skip(1)
                    .Subscribe(sub => Panel.Open = sub != null)
                    .DisposeWith(disposable);
            });
        }
    }
}