using System.Collections.Generic;
using Vulcanova.Uonet.Api.Common.Models;

namespace Vulcanova.Uonet.Api.Lessons
{
    public class LessonPayload
    {
        public bool CalculatePresence { get; set; }
        public ClassUnit Clazz { get; set; }
        public List<object> Collection { get; set; }
        public Date DateModify { get; set; }
        public Date Day { get; set; }
        public Distribution Distribution { get; set; }
        public string GlobalKey { get; set; }
        public string GroupDefinition { get; set; }
        public int Id { get; set; }
        public JustificationStatus? JustificationStatus { get; set; }
        public string LessonClassGlobalKey { get; set; }
        public int LessonClassId { get; set; }
        public int LessonId { get; set; }
        public int LessonNumber { get; set; }
        public object Note { get; set; }
        public PresenceType PresenceType { get; set; }
        public object PublicResources { get; set; }
        public object RemoteResources { get; set; }
        public bool Replacement { get; set; }
        public Subject Subject { get; set; }
        public Teacher TeacherMod { get; set; }
        public Teacher TeacherPrimary { get; set; }
        public Teacher TeacherSecondary { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public string Topic { get; set; }
        public bool Visible { get; set; }
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

    public enum JustificationStatus
    {
        Requested,
        Accepted,
        Rejected // not sure (yet)
    }
}