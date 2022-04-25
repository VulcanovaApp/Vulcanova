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
using Vulcanova.Core.Rx;
using Vulcanova.Features.Settings;

namespace Vulcanova.Features.Grades.Summary
{
    public class GradesSummaryViewModel : ViewModelBase
    {
        public ReactiveCommand<int, IEnumerable<Grade>> GetGrades { get; }

        public ReactiveCommand<Unit, IEnumerable<Grade>> ForceRefreshGrades { get; }

        public ReactiveCommand<int, Unit> ShowSubjectGradesDetails { get; }

        [Reactive] public IEnumerable<SubjectGrades> Grades { get; private set; }

        [ObservableAsProperty] public bool IsSyncing { get; }
        
        [Reactive] public int? PeriodId { get; set; }

        [Reactive] public SubjectGrades CurrentSubject { get; private set; }

        [ObservableAsProperty] private IEnumerable<Grade> RawGrades { get; }

        public GradesSummaryViewModel(
            INavigationService navigationService,
            AccountContext accountContext,
            IGradesService gradesService,
            AppSettings settings) : base(navigationService)
        {
            GetGrades = ReactiveCommand.CreateFromObservable((int periodId) =>
                gradesService
                    .GetPeriodGrades(accountContext.AccountId, periodId, false));
            
            ForceRefreshGrades = ReactiveCommand.CreateFromObservable(() =>
                gradesService
                    .GetPeriodGrades(accountContext.AccountId, PeriodId.Value, true));

            GetGrades.ToPropertyEx(this, vm => vm.RawGrades);

            ForceRefreshGrades.ToPropertyEx(this, vm => vm.RawGrades);

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
                    GetGrades.Execute(v!.Value).SubscribeAndIgnoreErrors();
                });

            var modifiersObservable = settings
                .WhenAnyValue(s => s.Modifiers.PlusSettings.SelectedValue, s => s.Modifiers.MinusSettings.SelectedValue)
                .WhereNotNull();

            this.WhenAnyValue(vm => vm.RawGrades)
                .WhereNotNull()
                .CombineLatest(modifiersObservable)
                .Subscribe(values =>
                {
                    var (grades, _) = values;
                    Grades = ToSubjectGrades(grades, settings.Modifiers);
                });
        }

        private static IEnumerable<SubjectGrades> ToSubjectGrades(IEnumerable<Grade> grades, ModifiersSettings modifiers)
            => grades.GroupBy(g => new
                {
                    g.Column.Subject.Id,
                    g.Column.Subject.Name
                })
                .Select(g => new SubjectGrades
                {
                    SubjectId = g.Key.Id,
                    SubjectName = g.Key.Name,
                    Average = g.Average(modifiers),
                    Grades = g.ToArray()
                });
    }
}