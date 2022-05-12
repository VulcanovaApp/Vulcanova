using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Mvvm;
using Vulcanova.Core.Rx;
using Vulcanova.Extensions;
using Vulcanova.Features.Exams;
using Vulcanova.Features.Grades;
using Vulcanova.Features.LuckyNumber;
using Vulcanova.Features.Shared;
using Vulcanova.Features.Timetable;
using Vulcanova.Features.Timetable.Changes;

namespace Vulcanova.Features.Dashboard
{
    public class DashboardViewModel : ViewModelBase
    {
        [Reactive] public IReadOnlyCollection<Exam> CurrentWeekEntries { get; private set; }
        [Reactive] public Exam SelectedExam { get; set; }
        [Reactive] public IEnumerable<TimetableListEntry> TimetableEntries { get; private set; }
        [Reactive] public DateTime SelectedDay { get; set; } = DateTime.Today.AddDays(4);
        
        
        [ObservableAsProperty] public ImmutableArray<Exam> Entries { get; }
        [ObservableAsProperty] public IReadOnlyDictionary<DateTime, IEnumerable<TimetableListEntry>> TimetableTempEntries { get; }
        [ObservableAsProperty] public LuckyNumber.LuckyNumber LuckyNumber { get; }
        
        
        public ReactiveCommand<bool, IReadOnlyDictionary<DateTime, IEnumerable<TimetableListEntry>>> GetTimetableEntries { get; }
        public ReactiveCommand<Unit, IReadOnlyDictionary<DateTime, IEnumerable<TimetableListEntry>>> ForceRefreshTimetableEntries { get; }
        public ReactiveCommand<int, LuckyNumber.LuckyNumber> GetLuckyNumber { get; }
        public ReactiveCommand<bool, ImmutableArray<Exam>> GetExams { get; }
        public ReactiveCommand<Unit, ImmutableArray<Exam>> ForceRefreshExams { get; }
        public ReactiveCommand<int, Unit> ShowExamDetails { get; }
        
        
        private readonly ILuckyNumberService _luckyNumberService;
        private readonly ITimetableService _timetableService;
        private readonly ITimetableChangesService _timetableChangesService;
        private readonly IExamsService _examsService;

        
        
        public string Title { get; set; }


        public DashboardViewModel(
            INavigationService navigationService, 
            ITimetableService timetableService,
            AccountContext accountContext,
            IExamsService examsService,
            ITimetableChangesService timetableChangesService, 
            ILuckyNumberService luckyNumberService) : base(navigationService)
        {
            
            Title = DateTime.Today.ToString("dddd, dd MMMM");
            
            // Timetable
            
            _timetableService = timetableService;
            _timetableChangesService = timetableChangesService;

            GetTimetableEntries = ReactiveCommand.CreateFromObservable((bool forceSync) =>
                GetTimetableEntriesS(accountContext.AccountId, SelectedDay, forceSync)
                    .SubscribeOn(RxApp.TaskpoolScheduler));

            ForceRefreshTimetableEntries = ReactiveCommand.CreateFromObservable(() =>
                GetTimetableEntries.Execute(true));

            GetTimetableEntries.ToPropertyEx(this, vm => vm.TimetableTempEntries);

            this.WhenAnyValue(vm => vm.SelectedDay)
                .Subscribe((d) =>
                {
                    if (TimetableTempEntries == null || !TimetableTempEntries.TryGetValue(SelectedDay.Date, out _))
                    {
                        GetTimetableEntries.Execute(false).SubscribeAndIgnoreErrors();
                    }
                });

            this.WhenAnyValue(vm => vm.TimetableTempEntries)
                .CombineLatest(this.WhenAnyValue(vm => vm.SelectedDay))
                .Subscribe(tuple =>
                {
                    var (TimetableTempEntries, selectedDay) = tuple;

                    if (TimetableTempEntries != null && TimetableTempEntries.TryGetValue(selectedDay, out var values))
                    {
                        TimetableEntries = values;
                        return;
                    }

                    TimetableEntries = null;
                });
            
            // Grades
            
            // TODO: Get grades from server
            
            // Lucky number
            
            _luckyNumberService = luckyNumberService;

            GetLuckyNumber = ReactiveCommand.CreateFromTask((int accountId) => GetLuckyNumberAsync(accountId));
            GetLuckyNumber.ToPropertyEx(this, vm => vm.LuckyNumber);

            accountContext.WhenAnyValue(ctx => ctx.AccountId)
                .InvokeCommand(GetLuckyNumber);
            
            // Exams
            
            _examsService = examsService;
            
            GetExams = ReactiveCommand.CreateFromObservable((bool forceSync) =>
                GetExamEntries(accountContext.AccountId, SelectedDay, forceSync));
            
            ForceRefreshExams = ReactiveCommand.CreateFromObservable(() =>
                GetExams.Execute(true));
            
            GetExams.ToPropertyEx(this, vm => vm.Entries);
            
            ShowExamDetails = ReactiveCommand.Create((int lessonId) =>
            {
                SelectedExam = CurrentWeekEntries?.First(g => g.Id == lessonId);
            
                return Unit.Default;
            });
            
            var lastDate = SelectedDay;
            
            this.WhenAnyValue(vm => vm.SelectedDay)
                .Subscribe((d) =>
                {
                    if (Entries == null || lastDate.Month != d.Month)
                    {
                        GetExams.Execute(false).SubscribeAndIgnoreErrors();
                    }
            
                    lastDate = d;
                });
            
            this.WhenAnyValue(vm => vm.Entries)
                .Where(e => !e.IsDefaultOrEmpty)
                .CombineLatest(this.WhenAnyValue(vm => vm.SelectedDay))
                .Subscribe(tuple =>
                {
                    var (entries, selectedDay) = tuple;
            
                    var monday = selectedDay.LastMonday();
                    var sunday = selectedDay.NextSunday();
            
                    CurrentWeekEntries = entries.Where(e => e.Deadline >= monday && e.Deadline < sunday)
                        .ToImmutableList();
                });
        }

        // Lucky number

        private async Task<LuckyNumber.LuckyNumber> GetLuckyNumberAsync(int accountId)
        {
            return await _luckyNumberService.GetLuckyNumberAsync(
                accountId,
                DateTime.Now);
        }
        
        // Exams

        private IObservable<IReadOnlyDictionary<DateTime, IEnumerable<TimetableListEntry>>> GetTimetableEntriesS(int accountId,
            DateTime monthAndYear, bool forceSync = false)
        {
            var changes = _timetableChangesService.GetChangesEntriesByMonth(accountId, monthAndYear, forceSync);

            return _timetableService.GetPeriodEntriesByMonth(accountId, monthAndYear, forceSync)
                .CombineLatest(changes)
                .Select(items => ToDictionary(items.First, items.Second));
        }

        private static IReadOnlyDictionary<DateTime, IEnumerable<TimetableListEntry>> ToDictionary(
            IEnumerable<TimetableEntry> lessons, IEnumerable<TimetableChangeEntry> changes)
        {
            var result = new Dictionary<DateTime, IEnumerable<TimetableListEntry>>();

            var groups = lessons.Where(l => l.Visible).GroupBy(e => e.Date.Date);

            // avoid multiple enumerations
            var timetableChangeEntries = changes as TimetableChangeEntry[] ?? changes.ToArray();

            foreach (var group in groups)
            {
                var entries = new List<TimetableListEntry>();
                foreach (var lesson in group.OrderBy(e => e.Start))
                {
                    var change = timetableChangeEntries
                        .FirstOrDefault(c => c.TimetableEntryId == lesson.Id);

                    var entry = new TimetableListEntry
                    {
                        No = lesson.No,
                        Start = lesson.Start,
                        End = lesson.End,
                        SubjectName = change?.Subject?.Name ?? lesson.Subject.Name,
                        RoomName = change?.RoomName ?? lesson.RoomName,
                        TeacherName = change?.TeacherName ?? lesson.TeacherName,
                        Change = change?.Change.Type
                    };

                    entries.Add(entry);
                }

                result[group.Key] = entries.AsReadOnly();
            }

            return result;
        }
        
        // Exams
        
        private IObservable<ImmutableArray<Exam>> GetExamEntries(int accountId, DateTime date, bool forceSync = false)
        {
            var (firstDay, lastDay) = date.GetMondayOfFirstWeekAndSundayOfLastWeekOfMonth();

            return _examsService.GetExamsByDateRange(accountId, firstDay, lastDay, forceSync)
                .Select(e => e.ToImmutableArray());
        }
    }
}