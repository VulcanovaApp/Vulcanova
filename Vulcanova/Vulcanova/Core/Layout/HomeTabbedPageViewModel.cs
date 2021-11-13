using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Mvvm;

namespace Vulcanova.Core.Layout
{
    public class HomeTabbedPageViewModel : ViewModelBase
    {
        public ReactiveCommand<string, string> UpdateTitle { get; }

        [ObservableAsProperty]
        public string Title { get; }

        public HomeTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            UpdateTitle = ReactiveCommand.Create<string, string>(t => t);

            UpdateTitle.ToPropertyEx(this, vm => vm.Title);
        }
    }
}