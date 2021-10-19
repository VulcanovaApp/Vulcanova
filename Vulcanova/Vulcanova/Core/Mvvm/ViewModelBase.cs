using Prism.Navigation;
using ReactiveUI;

namespace Vulcanova.Core.Mvvm
{
    public abstract class ViewModelBase : ReactiveObject
    {
        protected readonly INavigationService NavigationService;

        protected ViewModelBase(INavigationService navigationService) => NavigationService = navigationService;
    }
}