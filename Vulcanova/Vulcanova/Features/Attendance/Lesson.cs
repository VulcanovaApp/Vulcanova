using System;
using Vulcanova.Features.Shared;

namespace Vulcanova.Features.Attendance
{
    public class Lesson
    {
        public int Id { get; set; }
        public int No { get; set; }
        public bool CalculatePresence { get; set; }
        public DateTime Date { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Topic { get; set; }
        public bool Visible { get; set; }
        public string TeacherName { get; set; }
        public Subject Subject { get; set; }
        public PresenceType PresenceType { get; set; }
        public int AccountId { get; set; }
    }
    
    public class PresenceType
    {
        public bool Absence { get; set; }
        public bool AbsenceJustified { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Id { get; set; }
        public bool Late { get; set; }
        public bool LegalAbsence { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public bool Presence { get; set; }
        public bool Removed { get; set; }
        public string Symbol { get; set; }
    }
}