using System.Reactive.Disposables;
using ReactiveUI;
using Vulcanova.Resources;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardView
    {
        public DashboardView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, vm => vm.Title, v => v.Title);
                this.OneWayBind(ViewModel, vm => vm, v => v.GradesListEntry.BindingContext);
                this.OneWayBind(ViewModel, vm => vm, v => v.ExamsListEntry.BindingContext);
                this.OneWayBind(ViewModel, vm => vm, v => v.TimetableListEntry.BindingContext)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, 
                        vm => vm.LuckyNumber, 
                        v => v.LuckyNumberEntry.Text,
                        l => l?.Number != 0 ? l?.Number.ToString() : Strings.NoLuckyNumberShort)
                    .DisposeWith(disposable);
            });
        }
    }
}