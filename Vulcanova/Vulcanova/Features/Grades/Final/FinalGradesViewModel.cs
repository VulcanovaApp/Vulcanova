using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Mvvm;
using Vulcanova.Core.Rx;
using Vulcanova.Features.Settings;
using Vulcanova.Features.Shared;

namespace Vulcanova.Features.Grades.Final;

public class FinalGradesViewModel : ViewModelBase
{
    public ReactiveCommand<bool, IEnumerable<FinalGradesEntry>> GetFinalGrades { get; }

    [ObservableAsProperty] public IEnumerable<FinalGradesEntry> FinalGrades { get; private set; }

    [Reactive] public int? PeriodId { get; set; }
    [Reactive] public decimal? FinalAverage { get; private set; }

    public FinalGradesViewModel(
        INavigationService navigationService,
        IFinalGradesService finalGradesService,
        AccountContext accountContext,
        AppSettings settings) : base(navigationService)
    {
        GetFinalGrades = ReactiveCommand.CreateFromObservable((bool forceSync) =>
            finalGradesService
                .GetPeriodGrades(accountContext.AccountId, PeriodId!.Value, forceSync)
                .SubscribeOn(RxApp.TaskpoolScheduler));

        GetFinalGrades.ToPropertyEx(this, vm => vm.FinalGrades);

        this.WhenAnyValue(vm => vm.PeriodId)
            .WhereNotNull()
            .Subscribe(v => GetFinalGrades.Execute().SubscribeAndIgnoreErrors());

        var modifiersObservable = settings
            .WhenAnyValue(s => s.Modifiers.PlusSettings.SelectedValue, s => s.Modifiers.MinusSettings.SelectedValue)
            .WhereNotNull();

        this.WhenAnyValue(vm => vm.FinalGrades)
            .WhereNotNull()
            .CombineLatest(modifiersObservable)
            .Subscribe(values =>
            {
                FinalAverage = values.First.Average(settings.Modifiers);
            });
    }
}