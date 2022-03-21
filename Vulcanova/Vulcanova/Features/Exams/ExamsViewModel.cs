using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Mvvm;
using Vulcanova.Core.Rx;
using Vulcanova.Extensions;
using Vulcanova.Features.Shared;

namespace Vulcanova.Features.Exams
{
    public class ExamsViewModel : ViewModelBase
    {
        public ReactiveCommand<bool, ImmutableArray<Exam>> GetExams { get; }
        public ReactiveCommand<Unit, ImmutableArray<Exam>> ForceRefreshExams { get; }
        public ReactiveCommand<int, Unit> ShowExamDetails { get; }

        [ObservableAsProperty] public ImmutableArray<Exam> Entries { get; }

        [Reactive] public IReadOnlyCollection<Exam> CurrentWeekEntries { get; private set; }
        [Reactive] public DateTime SelectedDay { get; set; } = DateTime.Today;
        [Reactive] public Exam SelectedExam { get; set; }

        private readonly IExamsService _examsService;

        public ExamsViewModel(
            IExamsService examsService,
            AccountContext accountContext,
            INavigationService navigationService) : base(navigationService)
        {
            _examsService = examsService;

            GetExams = ReactiveCommand.CreateFromObservable((bool forceSync) =>
                GetEntries(accountContext.AccountId, SelectedDay, forceSync));
            
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

        private IObservable<ImmutableArray<Exam>> GetEntries(int accountId, DateTime date, bool forceSync = false)
        {
            var (firstDay, lastDay) = date.GetMondayOfFirstWeekAndSundayOfLastWeekOfMonth();

            return _examsService.GetExamsByDateRange(accountId, firstDay, lastDay, forceSync)
                .Select(e => e.ToImmutableArray());
        }
    }
}