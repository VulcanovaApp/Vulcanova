using System;

namespace Vulcanova.Uonet.Api.Schedule
{
    public record GetScheduleEntriesByPupilQuery(
        int PupilId,
        DateTime DateFrom,
        DateTime DateTo,
        DateTime LastSyncDate,
        int PageSize = 500,
        int LastId = int.MinValue) : IApiQuery<ScheduleEntry[]>
    {
        public const string ApiEndpoint = "mobile/schedule/byPupil";
    }
}