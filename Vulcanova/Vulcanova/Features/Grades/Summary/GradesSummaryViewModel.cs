using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Layout;
using Vulcanova.Core.Mvvm;
using Vulcanova.Features.Shared;
using Vulcanova.Core.Rx;
using Vulcanova.Features.Grades.SubjectDetails;
using Vulcanova.Features.Settings;
using Xamarin.Forms;

namespace Vulcanova.Features.Grades.Summary;

public class GradesSummaryViewModel : ViewModelBase
{
    public ReactiveCommand<bool, IEnumerable<Grade>> GetGrades { get; }

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
        AppSettings settings,
        ISheetPopper sheetPopper = null) : base(navigationService)
    {
        GetGrades = ReactiveCommand.CreateFromObservable((bool forceSync) =>
            gradesService
                .GetPeriodGrades(accountContext.AccountId, PeriodId!.Value, forceSync));

        GetGrades.ToPropertyEx(this, vm => vm.RawGrades);

        GetGrades.IsExecuting.ToPropertyEx(this, vm => vm.IsSyncing);

        ShowSubjectGradesDetails = ReactiveCommand.Create((int subjectId) =>
        {
            CurrentSubject = Grades?.First(g => g.SubjectId == subjectId);

            if (Device.RuntimePlatform == Device.iOS)
            {
                var view = new GradesSubjectDetailsView
                {
                    Subject = CurrentSubject
                };

                sheetPopper!.PopSheet(view);
            }

            return Unit.Default;
        });

        this.WhenAnyValue(vm => vm.PeriodId)
            .WhereNotNull()
            .Subscribe(v =>
            {
                GetGrades.Execute(false).SubscribeAndIgnoreErrors();
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
                Grades = new ObservableCollection<Grade>(g.ToArray())
            });
}