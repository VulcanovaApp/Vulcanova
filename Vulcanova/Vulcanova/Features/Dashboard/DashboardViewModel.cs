using System;
using System.Collections.Generic;
using Prism.Navigation;
using Vulcanova.Core.Mvvm;
using Vulcanova.Features.Exams;
using Vulcanova.Features.Grades;
using Vulcanova.Features.Shared;
using Vulcanova.Features.Timetable;
using Vulcanova.Resources;

namespace Vulcanova.Features.Dashboard
{
    public class DashboardViewModel : ViewModelBase
    {
        public string Title { get; set; }
        public IEnumerable<TimetableListEntry> TimetableList { get; set; }
        public IEnumerable<Grade> GradesList { get; set; }
        public IEnumerable<Exam> ExamsList { get; set; }
        public String LuckyNumber { get; set; }
        public DashboardViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = DateTime.Today.ToString("dddd, dd MMMM");
            TimetableList = GetTimetable();
            GradesList = GetGrades();
            LuckyNumber = GetLuckyNumber();
            ExamsList = GetExams();
        }

        private IEnumerable<Exam> GetExams()
        {
            return null; // TODO: Get exams from server
        }

        private string GetLuckyNumber()
        {
            return Strings.NoLuckyNumberShort; // TODO: Get lucky number from server
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