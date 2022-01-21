using Vulcanova.Uonet.Api.Common.Models;

namespace Vulcanova.Uonet.Api.Schedule
{
    public class ScheduleEntry
    {
        public Change Change { get; set; }
        public ClassUnit Clazz { get; set; }
        public Date Date { get; set; }
        public object Distribution { get; set; }
        public object Event { get; set; }
        public int Id { get; set; }
        public object MergeChangeId { get; set; }
        public object PupilAlias { get; set; }
        public Room Room { get; set; }
        public Subject Subject { get; set; }
        public Teacher TeacherPrimary { get; set; }
        public Teacher TeacherSecondary { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public bool Visible { get; set; }
    }

    public class Change
    {
        public int Id { get; set; }
        public ChangeType Type { get; set; }
        public bool IsMerge { get; set; }
        public bool Separation { get; set; }
    }
    
    public enum ChangeType
    {
        Exemption = 1,
        Substitution,
    }
}