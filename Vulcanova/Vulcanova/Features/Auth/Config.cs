using Prism.Ioc;
using Vulcanova.Features.Auth.Intro;
using Vulcanova.Features.Auth.ManualSigningIn;
using Vulcanova.Features.Auth.ScanningQrCode;

namespace Vulcanova.Features.Auth
{
    public static class Config
    {
        public static void RegisterAuth(this IContainerRegistry container)
        {
            container.RegisterScoped<AccountsManager>();
            container.RegisterScoped<IAccountRepository, AccountRepository>();

            container.RegisterForNavigation<IntroView>();
            container.RegisterForNavigation<ManualSignInView>();
            container.RegisterForNavigation<QrScannerView>();
            container.RegisterForNavigation<EnterPinCodeView>();
            
            container.RegisterScoped<IAuthenticationService, AuthenticationService>();
        }
    }
}