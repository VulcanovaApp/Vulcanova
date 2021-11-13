using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Xaml;

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
            });
        }
    }
}