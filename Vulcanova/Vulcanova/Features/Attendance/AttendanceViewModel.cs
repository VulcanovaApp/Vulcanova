using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Mvvm;
using Vulcanova.Core.Rx;
using Vulcanova.Features.Shared;

namespace Vulcanova.Features.Attendance
{
    public class AttendanceViewModel : ViewModelBase
    {
        public ReactiveCommand<DateTime, IReadOnlyDictionary<DateTime, List<Lesson>>> GetTimetableEntries { get; }

        [ObservableAsProperty] public IReadOnlyDictionary<DateTime, List<Lesson>> Entries { get; }

        [Reactive] public List<Lesson> CurrentDayEntries { get; private set; }
        [Reactive] public DateTime SelectedDay { get; set; } = DateTime.Today;

        private readonly ILessonsService _lessonsService;

        public AttendanceViewModel(
            ILessonsService lessonsService,
            AccountContext accountContext,
            INavigationService navigationService) : base(navigationService)
        {
            _lessonsService = lessonsService;

            GetTimetableEntries = ReactiveCommand.CreateFromObservable((DateTime date) =>
                GetEntries(accountContext.AccountId, date, false));

            GetTimetableEntries.ToPropertyEx(this, vm => vm.Entries);

            this.WhenAnyValue(vm => vm.SelectedDay)
                .Subscribe((d) =>
                {
                    if (Entries == null || !Entries.TryGetValue(SelectedDay.Date, out _))
                    {
                        GetTimetableEntries.Execute(d).SubscribeAndIgnoreErrors();
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

        private IObservable<IReadOnlyDictionary<DateTime, List<Lesson>>> GetEntries(int accountId,
            DateTime monthAndYear, bool forceSync = false)
        {
            return _lessonsService.GetLessonsByMonth(accountId, monthAndYear, forceSync)
                .Select(ToDictionary);
        }

        private static IReadOnlyDictionary<DateTime, List<Lesson>> ToDictionary(IEnumerable<Lesson> lessons)
        {
            return lessons
                .GroupBy(l => l.Date)
                .ToDictionary(g => g.Key, g => g.OrderBy(l => l.No).ToList());
        }
    }
}