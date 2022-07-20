using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.Runtime;
using Android.OS;
using FFImageLoading.Forms.Platform;
using OliveTree.Transitions;
using OliveTree.Transitions.Droid;
using Prism;
using Prism.Ioc;

namespace Vulcanova.Android
{
    [Activity(Label = "Vulcanova", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        Icon = "@mipmap/ic_launcher", RoundIcon = "@mipmap/ic_launcher_round")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity,
        IPlatformInitializer
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);

            CachedImageRenderer.Init(true);

            XamEffects.Droid.Effects.Init();

            TransitionsLibrary.Register<Provider>();

            GoogleVisionBarCodeScanner.Droid.RendererInitializer.Init();
            
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(this));
            
            Window?.SetStatusBarColor(Color.Argb(255, 0, 0, 0));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}