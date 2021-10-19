using GoogleVisionBarCodeScanner;
using Prism.Ioc;
using Vulcanova.Features.Auth;
using Vulcanova.Features.Auth.Intro;
using Vulcanova.Features.Auth.ManualSigningIn;
using Vulcanova.Features.Auth.ScanningQrCode;
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

            await NavigationService.NavigateAsync("NavigationPage/IntroView");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<IntroView, IntroViewModel>();
            containerRegistry.RegisterForNavigation<ManualSignInView, ManualSignInViewModel>();
            containerRegistry.RegisterForNavigation<QrScannerView, QrScannerViewModel>();

            containerRegistry.RegisterScoped<IAuthenticationService, AuthenticationService>();
        }
    }
}