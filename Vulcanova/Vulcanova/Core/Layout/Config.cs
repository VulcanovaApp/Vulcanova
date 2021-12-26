using Prism.Ioc;

namespace Vulcanova.Core.Layout
{
    public static class Config
    {
        public static void RegisterLayout(this IContainerRegistry container)
        {
            container.RegisterForNavigation<MainNavigationPage>();
            container.RegisterForNavigation<HomeTabbedPage>();
        }
    }
}