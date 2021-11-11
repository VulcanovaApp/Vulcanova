using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Mvvm;
using Vulcanova.Features.Grades.Summary;
using Vulcanova.Features.Shared;

namespace Vulcanova.Features.Grades
{
    public class GradesViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, SubjectGrades[]> ForceSyncGrades { get; }
        public ReactiveCommand<int, SubjectGrades[]> GetGrades { get; }
        
        [ObservableAsProperty]
        public SubjectGrades[] Grades { get; }
        
        private readonly IGradesService _gradesService;

        public GradesViewModel(
            INavigationService navigationService,
            AccountContext accountContext,
            IGradesService gradesService) : base(navigationService)
        {
            _gradesService = gradesService;

            GetGrades = ReactiveCommand.CreateFromTask((int accountId) => GetGradesAsync(accountId));

            GetGrades.ToPropertyEx(this, vm => vm.Grades);
            
            accountContext.WhenAnyValue(ctx => ctx.AccountId)
                .InvokeCommand(GetGrades);
        }

        private async Task<SubjectGrades[]> GetGradesAsync(int accountId)
        {
            return (await _gradesService.GetCurrentPeriodGradesAsync(accountId, true))
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
    }
}