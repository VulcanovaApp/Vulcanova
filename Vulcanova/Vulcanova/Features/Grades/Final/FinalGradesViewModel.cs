using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Mvvm;
using Vulcanova.Features.Shared;

namespace Vulcanova.Features.Grades.Final
{
    public class FinalGradesViewModel : ViewModelBase
    {
        public ReactiveCommand<int, IEnumerable<FinalGradesEntry>> GetFinalGrades { get; }
        public ReactiveCommand<Unit, IEnumerable<FinalGradesEntry>> ForceRefreshGrades { get; }

        [ObservableAsProperty] public IEnumerable<FinalGradesEntry> FinalGrades { get; }

        [Reactive] public int? PeriodId { get; set; }

        public FinalGradesViewModel(
            INavigationService navigationService,
            IFinalGradesService finalGradesService,
            AccountContext accountContext) : base(navigationService)
        {
            GetFinalGrades = ReactiveCommand.CreateFromObservable((int periodId) =>
                finalGradesService
                    .GetPeriodGrades(accountContext.AccountId, periodId, false));

            ForceRefreshGrades = ReactiveCommand.CreateFromObservable(() =>
                    finalGradesService
                        .GetPeriodGrades(accountContext.AccountId, PeriodId!.Value, true),
                this.WhenAnyValue(vm => vm.PeriodId).Select(value => value != null));

            this.WhenAnyValue(vm => vm.PeriodId)
                .WhereNotNull()
                .Subscribe(v => { GetFinalGrades.Execute(v!.Value).Subscribe(); });

            GetFinalGrades.ToPropertyEx(this, vm => vm.FinalGrades);
        }
    }
}