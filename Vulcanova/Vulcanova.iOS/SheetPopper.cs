using UIKit;
using Vulcanova.Core.Layout;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Vulcanova.iOS
{
    public class SheetPopper : ISheetPopper
    {
        public void PopSheet(ContentView content)
        {
            // Maybe let the caller decide about this?
            content.Padding = new Thickness(0, 14, 0, 0);

            var page = new ContentPage
            {
                Content = content,
                Parent = Xamarin.Forms.Application.Current
            };

            var cvc = page.CreateViewController();
            
            var navController = new UINavigationController(cvc);

            navController.SheetPresentationController.Detents =
                new[]
                {
                    UISheetPresentationControllerDetent.CreateMediumDetent(),
                    UISheetPresentationControllerDetent.CreateLargeDetent()
                };

            navController.SheetPresentationController.PrefersGrabberVisible = true;
            navController.SheetPresentationController.PrefersScrollingExpandsWhenScrolledToEdge = true;

            cvc.NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Close,
                (sender, args) => navController.DismissViewController(true, null)), 
                true);

            UIApplication.SharedApplication.KeyWindow?.RootViewController?
                .PresentViewController(navController, true, null);
        }
    }
}