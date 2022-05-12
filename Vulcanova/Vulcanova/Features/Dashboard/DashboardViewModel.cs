using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Mvvm;
using Vulcanova.Features.Exams;
using Vulcanova.Features.Grades;
using Vulcanova.Features.LuckyNumber;
using Vulcanova.Features.Shared;
using Vulcanova.Features.Timetable;

namespace Vulcanova.Features.Dashboard
{
    public class DashboardViewModel : ViewModelBase
    {
        
        private readonly ILuckyNumberService _luckyNumberService;
        
        public string Title { get; set; }
        public IEnumerable<TimetableListEntry> TimetableList { get; set; }
        public IEnumerable<Grade> GradesList { get; set; }
        public IEnumerable<Exam> ExamsList { get; set; }
        
        
        public ReactiveCommand<int, LuckyNumber.LuckyNumber> GetLuckyNumber { get; }

        [ObservableAsProperty]
        public LuckyNumber.LuckyNumber LuckyNumber { get; }
        
        
        public DashboardViewModel(
            INavigationService navigationService, 
            AccountContext accountContext, 
            ILuckyNumberService luckyNumberService) : base(navigationService)
        {
            
            Title = DateTime.Today.ToString("dddd, dd MMMM");
            
            // Timetable
            
            TimetableList = GetTimetable();
            
            // Grades
            
            GradesList = GetGrades();
            
            // Lucky number
            
            _luckyNumberService = luckyNumberService;

            GetLuckyNumber = ReactiveCommand.CreateFromTask((int accountId) => GetLuckyNumberAsync(accountId));
            GetLuckyNumber.ToPropertyEx(this, vm => vm.LuckyNumber);

            accountContext.WhenAnyValue(ctx => ctx.AccountId)
                .InvokeCommand(GetLuckyNumber);
            
            // Exams
            
            ExamsList = GetExams();
        }

        private IEnumerable<Exam> GetExams()
        {
            return null; // TODO: Get exams from server
        }

        private async Task<LuckyNumber.LuckyNumber> GetLuckyNumberAsync(int accountId)
        {
            return await _luckyNumberService.GetLuckyNumberAsync(
                accountId,
                DateTime.Now);
        }

        private IEnumerable<Grade> GetGrades()
        {
            return null; // TODO: Get grades from server
        }

        private static IEnumerable<TimetableListEntry> GetTimetable()
        {
            return null; // TODO: Get timetable from server
        }
    }
}