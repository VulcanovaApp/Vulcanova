using System;
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
        
        public ReactiveCommand<int, Unit> ShowSubjectGradesDetails { get; }
        
        public ReactiveCommand<Unit, Unit> NextSemester { get; }
        public ReactiveCommand<Unit, Unit> PreviousSemester { get; }

        [ObservableAsProperty]
        public IEnumerable<SubjectGrades> Grades { get; }

        [ObservableAsProperty]
        public bool IsSyncing { get; }

        [Reactive] public PeriodResult PeriodInfo { get; private set; }

        [Reactive] public SubjectGrades CurrentSubject { get; private set; }

        public GradesSummaryViewModel(
            INavigationService navigationService,
            AccountContext accountContext,
            IGradesService gradesService,
            IPeriodService periodService) : base(navigationService)
        {
            GetGrades = ReactiveCommand.CreateFromObservable((bool forceSync) =>
                gradesService
                    .GetPeriodGrades(accountContext.AccountId, PeriodInfo!.CurrentPeriod.Id, forceSync)
                    .Select(ToSubjectGrades));

            ForceSyncGrades = ReactiveCommand.CreateFromObservable(() => GetGrades.Execute(true));

            GetGrades.ToPropertyEx(this, vm => vm.Grades);

            GetGrades.IsExecuting.ToPropertyEx(this, vm => vm.IsSyncing);

            accountContext.WhenAnyValue(ctx => ctx.AccountId)
                .Subscribe(id => PeriodInfo = periodService.GetCurrentPeriod(id));

            this.WhenAnyValue(vm => vm.PeriodInfo)
                .WhereNotNull()
                .Select(_ => false)
                .InvokeCommand(GetGrades);

            ShowSubjectGradesDetails = ReactiveCommand.Create((int subjectId) =>
            {
                CurrentSubject = Grades?.First(g => g.SubjectId == subjectId);

                return Unit.Default;
            });

            NextSemester = ReactiveCommand.Create(() =>
            {
                PeriodInfo = periodService.ChangePeriod(accountContext.AccountId, PeriodChangeDirection.Next);
            });
            
            PreviousSemester = ReactiveCommand.Create(() =>
            {
                PeriodInfo = periodService.ChangePeriod(accountContext.AccountId, PeriodChangeDirection.Previous);
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