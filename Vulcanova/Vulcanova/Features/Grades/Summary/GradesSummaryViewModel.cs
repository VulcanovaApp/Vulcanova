using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Mvvm;
using Vulcanova.Features.Shared;
using System;

namespace Vulcanova.Features.Grades.Summary
{
    public class GradesSummaryViewModel : ViewModelBase
    {
        public ReactiveCommand<int, IEnumerable<SubjectGrades>> GetGrades { get; }

        public ReactiveCommand<Unit, IEnumerable<SubjectGrades>> ForceRefreshGrades { get; }

        public ReactiveCommand<int, Unit> ShowSubjectGradesDetails { get; }

        [ObservableAsProperty] public IEnumerable<SubjectGrades> Grades { get; }

        [ObservableAsProperty] public bool IsSyncing { get; }
        
        [Reactive] public int? PeriodId { get; set; }

        [Reactive] public SubjectGrades CurrentSubject { get; private set; }

        public GradesSummaryViewModel(
            INavigationService navigationService,
            AccountContext accountContext,
            IGradesService gradesService) : base(navigationService)
        {
            GetGrades = ReactiveCommand.CreateFromObservable((int periodId) =>
                gradesService
                    .GetPeriodGrades(accountContext.AccountId, periodId, false)
                    .Select(ToSubjectGrades));
            
            ForceRefreshGrades = ReactiveCommand.CreateFromObservable(() =>
                gradesService
                    .GetPeriodGrades(accountContext.AccountId, PeriodId.Value, true)
                    .Select(ToSubjectGrades));

            GetGrades.ToPropertyEx(this, vm => vm.Grades);

            ForceRefreshGrades.ToPropertyEx(this, vm => vm.Grades);

            GetGrades.IsExecuting.ToPropertyEx(this, vm => vm.IsSyncing);

            ShowSubjectGradesDetails = ReactiveCommand.Create((int subjectId) =>
            {
                CurrentSubject = Grades?.First(g => g.SubjectId == subjectId);

                return Unit.Default;
            });

            this.WhenAnyValue(vm => vm.PeriodId)
                .WhereNotNull()
                .Subscribe(v =>
                {
                    GetGrades.Execute(v!.Value).Subscribe();
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