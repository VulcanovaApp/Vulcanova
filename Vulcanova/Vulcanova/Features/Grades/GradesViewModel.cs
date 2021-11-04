using System.Reactive;
using System.Threading.Tasks;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Mvvm;

namespace Vulcanova.Features.Grades
{
    public class GradesViewModel : ViewModelBase, INavigatedAware
    {
        public ReactiveCommand<Unit, Grade[]> ForceSyncGrades { get; }
        public ReactiveCommand<Unit, Grade[]> GetGrades { get; }
        
        [ObservableAsProperty]
        public Grade[] Grades { get; }

        private readonly IGradesService _gradesService;

        private int _accountId;
        private int _periodId;

        public GradesViewModel(
            INavigationService navigationService,
            IGradesService gradesService) : base(navigationService)
        {
            _gradesService = gradesService;

            GetGrades = ReactiveCommand.CreateFromTask(_ => GetGradesAsync(_accountId, _periodId));

            GetGrades.ToPropertyEx(this, vm => vm.Grades);
        }

        private async Task<Grade[]> GetGradesAsync(int accountId, int periodId)
        {
            return await _gradesService.GetGradesAsync(accountId, periodId, true);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _accountId = parameters.GetValue<int>("accountId");
            _periodId = parameters.GetValue<int>("periodId");
        }
    }
}