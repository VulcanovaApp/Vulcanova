using System;
using Vulcanova.Uonet.Api.Schedule;

namespace Vulcanova.Features.Timetable.Changes
{
    public class TimetableChangeEntry
    {
        public int Id { get; set; }
        public int TimetableEntryId { get; set; }
        public int UnitId { get; set; }
        public int PupilId { get; set; }
        public int AccountId { get; set; }
        public Shared.Subject Subject { get; set; }
        public DateTime LessonDate { get; set; }
        public string Note { get; set; }
        public string TeacherName { get; set; }
        public string RoomName { get; set; }
        public Change Change { get; set; }
    }
}