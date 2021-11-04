using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Grades
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradesSummaryView
    {
        public GradesSummaryView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.WhenAnyValue(v => v.ViewModel)
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel!.GetGrades)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.Grades, v => v.Grades.ItemsSource)
                    .DisposeWith(disposable);
            });
        }
    }
}