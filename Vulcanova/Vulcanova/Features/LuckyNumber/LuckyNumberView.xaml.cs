using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.LuckyNumber
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LuckyNumberView
    {
        public LuckyNumberView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, 
                        vm => vm.LuckyNumber, 
                        v => v.LuckyNumberLabel.Text,
                        l => l?.Number.ToString())
                    .DisposeWith(disposable);
            });
        }
    }
}