using System.Reactive;
using Prism.Navigation;
using ReactiveUI;
using Vulcanova.Core.Mvvm;
using Vulcanova.Features.Auth.ManualSigningIn;
using Vulcanova.Features.Auth.ScanningQrCode;
using Xamarin.Essentials;

namespace Vulcanova.Features.Auth.Intro;

public class IntroViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, INavigationResult> ScanQrCode { get; }
    public ReactiveCommand<Unit, INavigationResult> SignInManually { get; }
    public ReactiveCommand<Unit, Unit> OpenGitHubLink { get; }

    public IntroViewModel(INavigationService navigationService) : base(navigationService)
    {
        ScanQrCode = ReactiveCommand.CreateFromTask(() => 
            NavigationService.NavigateAsync(nameof(QrScannerView)));

        SignInManually = ReactiveCommand.CreateFromTask(() =>
            NavigationService.NavigateAsync(nameof(ManualSignInView)));

        OpenGitHubLink = ReactiveCommand.CreateFromTask(() =>
            Browser.OpenAsync("https://github.com/VulcanovaApp/Vulcanova",
                BrowserLaunchMode.SystemPreferred));
    }
}