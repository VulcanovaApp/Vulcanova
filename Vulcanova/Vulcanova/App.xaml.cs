using GoogleVisionBarCodeScanner;
using Prism.Ioc;
using ReactiveUI;
using Vulcanova.Core.Data;
using Vulcanova.Core.Layout;
using Vulcanova.Core.Mapping;
using Vulcanova.Core.Rx;
using Vulcanova.Core.Uonet;
using Vulcanova.Features.Auth;
using Vulcanova.Features.Grades;
using Vulcanova.Features.LuckyNumber;
using Vulcanova.Features.Shared;
using Vulcanova.Features.Timetable;
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

            RxApp.DefaultExceptionHandler = new ReactiveExceptionHandler();

            Sharpnado.Tabs.Initializer.Initialize(false, false);
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);

            Methods.SetSupportBarcodeFormat(BarcodeFormats.QRCode);

            var accRepo = Container.Resolve<IAccountRepository>();
            
            // breaks app startup when executed asynchronously
            var activeAccount = accRepo.GetActiveAccountAsync().Result;

            if (activeAccount != null)
            {
                var ctx = Container.Resolve<AccountContext>();
                ctx.AccountId = activeAccount.Id;

                await NavigationService.NavigateAsync("MainNavigationPage/HomeTabbedPage?selectedTab=GradesSummaryView");
            }
            else
            {
                await NavigationService.NavigateAsync("MainNavigationPage/IntroView");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterLiteDb();

            containerRegistry.RegisterAutoMapper();
            
            containerRegistry.RegisterLayout();
            
            containerRegistry.RegisterAuth();
            containerRegistry.RegisterLuckyNumber();
            containerRegistry.RegisterGrades();
            containerRegistry.RegisterTimetable();

            containerRegistry.RegisterUonet();
        }
    }
}