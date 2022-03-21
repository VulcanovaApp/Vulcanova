using System;

namespace Vulcanova.Uonet.Api.Exams
{
    public record GetExamsByPupilQuery(
        int UnitId,
        int PupilId,
        DateTime LastSyncDate,
        int PageSize,
        int LastId = int.MinValue) : IApiQuery<ExamPayload[]>
    {
        public const string ApiEndpoint = "mobile/exam/byPupil";
    }
}