using System;
using Vulcanova.Features.Shared;

namespace Vulcanova.Features.Grades.Final;

public class FinalGradesEntry
{
    public string Id { get; set; }
    public int AccountId { get; set; }
    public int PupilId { get; set; }
    public int PeriodId { get; set; }
    public Subject Subject { get; set; }
    public string PredictedGrade { get; set; }
    public string FinalGrade { get; set; }
    public string Entry3 { get; set; }
    public DateTime DateModify { get; set; }
}