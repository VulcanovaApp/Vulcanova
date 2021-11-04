using System;

namespace Vulcanova.Uonet.Api.Grades
{
    public record GetGradesByPupilQuery(
        int UnitId,
        int PupilId,
        int PeriodId,
        DateTime LastSyncDate,
        int PageSize,
        int LastId = int.MinValue) : IApiQuery<GradePayload[]>
    {
        public const string ApiEndpoint = "mobile/grade/byPupil";
    }
}