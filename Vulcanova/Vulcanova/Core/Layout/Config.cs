using Prism.Ioc;
using Prism.Plugin.Popups;

namespace Vulcanova.Core.Layout;

public static class Config
{
    public static void RegisterLayout(this IContainerRegistry container)
    {
        container.RegisterPopupNavigationService();

        container.RegisterForNavigation<MainNavigationPage>();
        container.RegisterForNavigation<HomeTabbedPage>();
    }
}