using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Mvvm;
using Vulcanova.Features.Shared;

namespace Vulcanova.Features.Timetable
{
    public class TimetableViewModel : ViewModelBase
    {
        public ReactiveCommand<DateTime, IReadOnlyDictionary<DateTime, IEnumerable<TimetableListEntry>>> GetTimetableEntries { get; }

        [ObservableAsProperty] public IReadOnlyDictionary<DateTime, IEnumerable<TimetableListEntry>> Entries { get; }

        [Reactive] public IEnumerable<TimetableListEntry> CurrentDayEntries { get; private set; }
        [Reactive] public DateTime SelectedDay { get; set; } = DateTime.Today;

        private readonly ITimetableService _timetableService;

        public TimetableViewModel(
            INavigationService navigationService,
            ITimetableService timetableService,
            AccountContext accountContext) : base(navigationService)
        {
            _timetableService = timetableService;

            GetTimetableEntries = ReactiveCommand.CreateFromObservable((DateTime date) =>
                GetEntries(accountContext.AccountId, date, false));

            GetTimetableEntries.ToPropertyEx(this, vm => vm.Entries);

            this.WhenAnyValue(vm => vm.SelectedDay)
                .Subscribe((d) =>
                {
                    if (Entries == null || !Entries.TryGetValue(SelectedDay.Date, out _))
                    {
                        GetTimetableEntries.Execute(d).Subscribe();
                    }
                });

            this.WhenAnyValue(vm => vm.Entries)
                .CombineLatest(this.WhenAnyValue(vm => vm.SelectedDay))
                .Subscribe(tuple =>
                {
                    var (entries, selectedDay) = tuple;

                    if (entries != null && entries.TryGetValue(selectedDay, out var values))
                    {
                        CurrentDayEntries = values;
                        return;
                    }

                    CurrentDayEntries = null;
                });
        }

        private IObservable<IReadOnlyDictionary<DateTime, IEnumerable<TimetableListEntry>>> GetEntries(int accountId,
            DateTime monthAndYear, bool forceSync = false)
        {
            return _timetableService.GetPeriodEntriesByMonth(accountId, monthAndYear, forceSync)
                .Select(ToDictionary);
        }

        private static IReadOnlyDictionary<DateTime, IEnumerable<TimetableListEntry>> ToDictionary(
            IEnumerable<TimetableEntry> entries)
        {
            return entries.GroupBy(e => e.Date.Date)
                .ToDictionary(g => g.Key,
                    g => g.Where(e => e.Visible)
                        .OrderBy(e => e.Start)
                        .Select((e, i) => new TimetableListEntry(i + 1, e)));
        }
    }
}