using System;

namespace Vulcanova.Uonet.Api.Homeworks
{
    public record GetHomeworksByPupilQuery(
        int PupilId,
        int PeriodId,
        DateTime LastSyncDate,
        int LastId = int.MinValue,
        int PageSize = 500) : IApiQuery<HomeworkPayload[]>
    {
        public const string ApiEndpoint = "mobile/homework/byPupil";
    }
}