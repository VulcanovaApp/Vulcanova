using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
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
        public ReactiveCommand<Unit, IEnumerable<SubjectGrades>> ForceSyncGrades { get; }
        public ReactiveCommand<int, IEnumerable<SubjectGrades>> GetGrades { get; }
        
        [ObservableAsProperty]
        public IEnumerable<SubjectGrades> Grades { get; }

        public GradesViewModel(
            INavigationService navigationService,
            AccountContext accountContext,
            IGradesService gradesService) : base(navigationService)
        {
            GetGrades = ReactiveCommand.CreateFromObservable((int accountId) =>
                gradesService
                    .GetCurrentPeriodGrades(accountId)
                    .Select(ToSubjectGrades));

            GetGrades.ToPropertyEx(this, vm => vm.Grades);
            
            accountContext.WhenAnyValue(ctx => ctx.AccountId)
                .InvokeCommand(GetGrades);
        }

        private static IEnumerable<SubjectGrades> ToSubjectGrades(IEnumerable<Grade> grades)
            => grades.GroupBy(g => new
                {
                    g.Column.Subject.Id,
                    g.Column.Subject.Name
                })
                .Select(g => new SubjectGrades
                {
                    SubjectName = g.Key.Name,
                    Grades = g.ToArray()
                });
    }
}