using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Auth.Intro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntroView
    {
        public IntroView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.BindCommand(ViewModel, x => x.ScanQrCode, x => x.ScanQrButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.SignInManually, x => x.ManualSignInButton)
                    .DisposeWith(disposable);
            });
        }
    }
}