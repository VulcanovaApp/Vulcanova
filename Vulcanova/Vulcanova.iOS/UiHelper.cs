using System.Linq;
using UIKit;

namespace Vulcanova.iOS
{
    // https://github.com/xamarin/Essentials/issues/1007#issuecomment-1038965876
    public static class UiHelper
    {
        public static UIViewController GetTopViewController()
        {
            var window = UIApplication.SharedApplication.GetKeyWindow();
            var vc = window?.RootViewController;
            while (vc is { PresentedViewController: { } })
                vc = vc.PresentedViewController;

            if (vc is UINavigationController { ViewControllers: { } } navController) 
                vc = navController.ViewControllers.Last();

            return vc;
        }

        public static UIWindow GetKeyWindow(this UIApplication application)
        {
            if (!UIDevice.CurrentDevice.CheckSystemVersion(13, 0)) 
                return application.KeyWindow; // deprecated in iOS 13
    
            var window = application
                .ConnectedScenes
                .ToArray()
                .OfType<UIWindowScene>()
                .SelectMany(scene => scene.Windows)
                .FirstOrDefault(window => window.IsKeyWindow);

            return window;
        }
    }
}