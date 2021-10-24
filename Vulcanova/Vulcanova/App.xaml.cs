using Akavache;
using GoogleVisionBarCodeScanner;
using Prism.Ioc;
using Vulcanova.Core.Mapping;
using Vulcanova.Core.Uonet;
using Vulcanova.Features.Auth;
using Vulcanova.Features.Auth.Intro;
using Vulcanova.Features.Auth.ManualSigningIn;
using Vulcanova.Features.Auth.ScanningQrCode;
using Vulcanova.Uonet.Api.Common;
using Vulcanova.Uonet.Signing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Vulcanova
{
    public partial class App
    {
        public App()
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            
            Methods.SetSupportBarcodeFormat(BarcodeFormats.QRCode);

            Registrations.Start("Vulcanova");

            await NavigationService.NavigateAsync("NavigationPage/IntroView");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterAutoMapper();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<IntroView, IntroViewModel>();
            containerRegistry.RegisterForNavigation<ManualSignInView, ManualSignInViewModel>();
            containerRegistry.RegisterForNavigation<QrScannerView, QrScannerViewModel>();
            containerRegistry.RegisterForNavigation<EnterPinCodeView, EnterPinCodeViewModel>();

            containerRegistry.RegisterSingleton<IApiClientFactory, ApiClientFactory>();
            containerRegistry.RegisterSingleton<IRequestSigner, RequestSignerAdapter>();
            containerRegistry.RegisterSingleton<IInstanceUrlProvider, InstanceUrlProvider>();

            containerRegistry.RegisterScoped<IAuthenticationService, AuthenticationService>();
        }
    }
}