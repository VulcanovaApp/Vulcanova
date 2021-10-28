using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
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
                this.WhenAnyValue(v => v.ViewModel)
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel!.GetLuckyNumber)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, vm => vm.LuckyNumber, v => v.LuckyNumberLabel.Text);
            });
        }
    }
}