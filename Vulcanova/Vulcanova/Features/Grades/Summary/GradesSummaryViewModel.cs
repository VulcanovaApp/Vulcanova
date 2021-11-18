using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Mvvm;
using Vulcanova.Features.Shared;

namespace Vulcanova.Features.Grades.Summary
{
    public class GradesSummaryViewModel : ViewModelBase
    {
        public ReactiveCommand<bool, IEnumerable<SubjectGrades>> GetGrades { get; }

        public ReactiveCommand<Unit, IEnumerable<SubjectGrades>> ForceSyncGrades { get; }
        
        public ReactiveCommand<int, INavigationResult> ShowSubjectGradesDetails { get; }

        [ObservableAsProperty]
        public IEnumerable<SubjectGrades> Grades { get; }
        
        [ObservableAsProperty]
        public bool IsSyncing { get; }

        public GradesSummaryViewModel(
            INavigationService navigationService,
            AccountContext accountContext,
            IGradesService gradesService) : base(navigationService)
        {
            GetGrades = ReactiveCommand.CreateFromObservable((bool forceSync) =>
                gradesService
                    .GetCurrentPeriodGrades(accountContext.AccountId, forceSync)
                    .Select(ToSubjectGrades));

            ForceSyncGrades = ReactiveCommand.CreateFromObservable(() => GetGrades.Execute(true));

            GetGrades.ToPropertyEx(this, vm => vm.Grades);

            GetGrades.IsExecuting.ToPropertyEx(this, vm => vm.IsSyncing);
            
            accountContext.WhenAnyValue(ctx => ctx.AccountId)
                .Select(_ => false)
                .InvokeCommand(GetGrades);

            ShowSubjectGradesDetails = ReactiveCommand.CreateFromTask((int subjectId) =>
            {
                var subjectGrades = Grades?.First(g => g.SubjectId == subjectId);

                return navigationService.NavigateAsync("GradesSubjectDetailsView", new NavigationParameters
                {
                    {"grades", subjectGrades.Grades},
                    {"subjectName", subjectGrades.SubjectName}
                });
            });
        }

        private static IEnumerable<SubjectGrades> ToSubjectGrades(IEnumerable<Grade> grades)
            => grades.GroupBy(g => new
                {
                    g.Column.Subject.Id,
                    g.Column.Subject.Name
                })
                .Select(g => new SubjectGrades
                {
                    SubjectId = g.Key.Id,
                    SubjectName = g.Key.Name,
                    Average = g.Average(),
                    Grades = g.ToArray()
                });
    }
}