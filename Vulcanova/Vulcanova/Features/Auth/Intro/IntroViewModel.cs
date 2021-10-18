using System.Reactive;
using ReactiveUI;
using Sextant;
using Splat;
using Vulcanova.Core.Mvvm;
using Vulcanova.Features.Auth.ManualSigningIn;
using Vulcanova.Features.Auth.ScanningQrCode;

namespace Vulcanova.Features.Auth.Intro
{
    public class IntroViewModel : ViewModelBase
    {
        public override string Id => "Intro";
        
        public ReactiveCommand<Unit, Unit> ScanQrCode { get; }
        public ReactiveCommand<Unit, Unit> SignInManually { get; }

        public IntroViewModel() : base(Locator.Current.GetService<IViewStackService>())
        {
            ScanQrCode = ReactiveCommand.CreateFromObservable(() => ViewStackService.PushPage(new QrScannerViewModel(ViewStackService)));
            SignInManually = ReactiveCommand.CreateFromObservable(() => ViewStackService.PushPage(new ManualSignInViewModel(ViewStackService)));
        }
    }
}