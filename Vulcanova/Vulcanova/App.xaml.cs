using GoogleVisionBarCodeScanner;
using Prism.Ioc;
using Prism.Navigation;
using Vulcanova.Core.Data;
using Vulcanova.Core.Layout;
using Vulcanova.Core.Mapping;
using Vulcanova.Core.Uonet;
using Vulcanova.Features.Auth;
using Vulcanova.Features.Auth.Intro;
using Vulcanova.Features.Auth.ManualSigningIn;
using Vulcanova.Features.Auth.ScanningQrCode;
using Vulcanova.Features.LuckyNumber;
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
            
            SQLitePCL.Batteries_V2.Init();

            Methods.SetSupportBarcodeFormat(BarcodeFormats.QRCode);

            var accRepo = Container.Resolve<IAccountRepository>();
            var activeAccount = await accRepo.GetActiveAccount();

            if (activeAccount != null)
            {
                await NavigationService.NavigateAsync("NavigationPage/HomeTabbedPage?selectedTab=LuckyNumberView", new NavigationParameters
                {
                    {"accountId", activeAccount.Id}
                });
            }
            else
            {
                await NavigationService.NavigateAsync("NavigationPage/IntroView");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDbContext();

            containerRegistry.RegisterScoped<IAccountRepository, AccountRepository>();

            containerRegistry.RegisterAutoMapper();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomeTabbedPage>();
            containerRegistry.RegisterForNavigation<IntroView, IntroViewModel>();
            containerRegistry.RegisterForNavigation<ManualSignInView, ManualSignInViewModel>();
            containerRegistry.RegisterForNavigation<QrScannerView, QrScannerViewModel>();
            containerRegistry.RegisterForNavigation<EnterPinCodeView, EnterPinCodeViewModel>();
            containerRegistry.RegisterForNavigation<LuckyNumberView, LuckyNumberViewModel>();

            containerRegistry.RegisterSingleton<IApiClientFactory, ApiClientFactory>();
            containerRegistry.RegisterSingleton<IRequestSigner, RequestSignerAdapter>();
            containerRegistry.RegisterSingleton<IInstanceUrlProvider, InstanceUrlProvider>();

            containerRegistry.RegisterScoped<IAuthenticationService, AuthenticationService>();
            containerRegistry.RegisterScoped<ILuckyNumberService, LuckyNumberService>();
        }
    }
}