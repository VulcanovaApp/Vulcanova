using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Layout;
using Vulcanova.Core.Mvvm;
using Vulcanova.Core.Rx;
using Vulcanova.Features.Attendance.LessonDetails;
using Vulcanova.Features.Shared;
using Xamarin.Forms;

namespace Vulcanova.Features.Attendance;

public class AttendanceViewModel : ViewModelBase
{
    public ReactiveCommand<bool, IReadOnlyDictionary<DateTime, List<Lesson>>> GetAttendanceEntries { get; }

    public ReactiveCommand<int, Unit> ShowLessonDetails { get; }

    [ObservableAsProperty] public IReadOnlyDictionary<DateTime, List<Lesson>> Entries { get; }

    [Reactive] public List<Lesson> CurrentDayEntries { get; private set; }
    [Reactive] public DateTime SelectedDay { get; set; } = DateTime.Today;
    [Reactive] public Lesson SelectedLesson { get; set; }

    private readonly ILessonsService _lessonsService;

    public AttendanceViewModel(
        ILessonsService lessonsService,
        AccountContext accountContext,
        INavigationService navigationService,
        ISheetPopper popper = null) : base(navigationService)
    {
        _lessonsService = lessonsService;

        GetAttendanceEntries = ReactiveCommand.CreateFromObservable((bool forceSync) =>
            GetEntries(accountContext.AccountId, SelectedDay, forceSync));

        GetAttendanceEntries.ToPropertyEx(this, vm => vm.Entries);
            
        ShowLessonDetails = ReactiveCommand.Create((int lessonId) =>
        {
            SelectedLesson = CurrentDayEntries?.First(g => g.Id == lessonId);

            if (Device.RuntimePlatform == Device.iOS)
            {
                var view = new LessonDetailsView
                {
                    Lesson = SelectedLesson
                };

                popper!.PopSheet(view);
            }

            return Unit.Default;
        });

        this.WhenAnyValue(vm => vm.SelectedDay)
            .Subscribe((d) =>
            {
                if (Entries == null || !Entries.TryGetValue(SelectedDay.Date, out _))
                {
                    GetAttendanceEntries.Execute(false).SubscribeAndIgnoreErrors();
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