using System;
using Vulcanova.Features.Shared;

namespace Vulcanova.Features.Timetable;

public class TimetableEntry
{
    public int Id { get; set; }
    public int No { get; set; }
    public int PupilId { get; set; }
    public int AccountId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string RoomName { get; set; }
    public string TeacherName { get; set; }
    public DateTime Date { get; set; }
    public Subject Subject { get; set; }
    public bool Visible { get; set; }
    public int PeriodId { get; set; }
}