using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Core.Layout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeTabbedPage : SvgTabbedPage<HomeTabbedPageViewModel>
    {
        public HomeTabbedPage()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.Events()
                    .CurrentPageChanged
                    .Select(_ => CurrentPage.Title)
                    .InvokeCommand(ViewModel, vm => vm.UpdateTitle)
                    .DisposeWith(disposable);
            
                this.OneWayBind(ViewModel, vm => vm.Title, v => v.Title)
                    .DisposeWith(disposable);

                this.WhenAnyValue(v => v.ViewModel)
                    .Select(_ => CurrentPage.Title)
                    .InvokeCommand(ViewModel, vm => vm.UpdateTitle)
                    .DisposeWith(disposable);
            });
        }
    }
}