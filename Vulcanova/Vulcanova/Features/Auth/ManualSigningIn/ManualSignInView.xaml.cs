using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Auth.ManualSigningIn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManualSignInView
    {
        public ManualSignInView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, vm => vm.Token, v => v.Token.Text).
                    DisposeWith(disposable);
                this.Bind(ViewModel, vm => vm.Symbol, v => v.Symbol.Text)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, vm => vm.Pin, v => v.Pin.Text)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.AddDevice, v => v.AddButton)
                    .DisposeWith(disposable);
            });
        }
    }
}