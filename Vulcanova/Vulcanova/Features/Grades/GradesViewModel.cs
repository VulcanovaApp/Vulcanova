using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Mvvm;
using Vulcanova.Features.Grades.Summary;

namespace Vulcanova.Features.Grades
{
    public class GradesViewModel : ViewModelBase, INavigatedAware
    {
        public ReactiveCommand<Unit, SubjectGrades[]> ForceSyncGrades { get; }
        public ReactiveCommand<Unit, SubjectGrades[]> GetGrades { get; }
        
        [ObservableAsProperty]
        public SubjectGrades[] Grades { get; }

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

        private async Task<SubjectGrades[]> GetGradesAsync(int accountId, int periodId)
        {
            return (await _gradesService.GetGradesAsync(accountId, periodId, true))
                .GroupBy(g => new
                {
                    g.Column.Subject.Id,
                    g.Column.Subject.Name
                })
                .Select(g => new SubjectGrades
                {
                    SubjectName = g.Key.Name,
                    Grades = g.ToArray()
                })
                .ToArray();
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