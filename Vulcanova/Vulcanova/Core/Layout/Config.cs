using Prism.Ioc;
using Xamarin.Forms;

namespace Vulcanova.Core.Layout
{
    public static class Config
    {
        public static void RegisterLayout(this IContainerRegistry container)
        {
            container.RegisterForNavigation<NavigationPage>();
            container.RegisterForNavigation<HomeTabbedPage>();
        }
    }
}