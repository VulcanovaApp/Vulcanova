using Prism.Navigation;
using Vulcanova.Core.Mvvm;

namespace Vulcanova.Features.Dashboard
{
    public class DashboardViewModel : ViewModelBase
    {
        public DashboardViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}