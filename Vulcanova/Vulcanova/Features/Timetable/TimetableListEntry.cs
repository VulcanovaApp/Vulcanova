using System;
using Vulcanova.Uonet.Api.Schedule;

namespace Vulcanova.Features.Timetable;

public class TimetableListEntry
{
    public int No { get; set; }
    public string SubjectName { get; set; }
    public string TeacherName { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string RoomName { get; set; }
    public ChangeType? Change { get; set; }
    public string ChangeNote { get; set; }
}