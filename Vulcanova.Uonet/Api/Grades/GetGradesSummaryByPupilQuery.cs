namespace Vulcanova.Uonet.Api.Grades
{
    public record GetGradesSummaryByPupilQuery(
        int UnitId,
        int PupilId,
        int PeriodId,
        int PageSize,
        int LastId = int.MinValue) : IApiQuery<GradesSummaryEntryPayload[]>
    {
        public const string ApiEndpoint = "mobile/grade/summary/byPupil";
    }
}