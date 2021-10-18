using System;
using System.Reflection;
using GoogleVisionBarCodeScanner;
using ReactiveUI;
using Sextant;
using Sextant.XamForms;
using Splat;
using Vulcanova.Features.Auth;
using Vulcanova.Features.Auth.Intro;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Sextant.Sextant;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Vulcanova
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            Methods.SetSupportBarcodeFormat(BarcodeFormats.QRCode);
            
            Instance.InitializeForms();

            Locator
                .CurrentMutable
                .RegisterViewsForViewModels(Assembly.GetExecutingAssembly());
            
            Locator
                .CurrentMutable
                .Register<IAuthenticationService>(() => new AuthenticationService());

            Locator
                .Current
                .GetService<IViewStackService>()
                .PushPage(new IntroViewModel(), null, true, false)
                .Subscribe();

            MainPage = Locator.Current.GetNavigationView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}