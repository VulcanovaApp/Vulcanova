using Vulcanova.Uonet.Api.Common.Models;

namespace Vulcanova.Uonet.Api.Schedule
{
    public class ScheduleChangeEntryPayload
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public int ScheduleId { get; set; }
        public Date LessonDate { get; set; }
        public object ChangeDate { get; set; }
        public string Note { get; set; }
        public object Reason { get; set; }
        public object Event { get; set; }
        public Room Room { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public Subject Subject { get; set; }
        public Teacher TeacherPrimary { get; set; }
        public Teacher TeacherSecondary { get; set; }
        public Change Change { get; set; }
        public ClassUnit Clazz { get; set; }
        public object Distribution { get; set; }
    }
}