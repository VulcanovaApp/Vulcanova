using System;

namespace Vulcanova.Uonet.Api.Schedule
{
    public record GetScheduleChangesEntriesByPupilQuery(
        int PupilId,
        DateTime DateFrom,
        DateTime DateTo,
        DateTime LastSyncDate,
        int LastId = int.MinValue,
        int PageSize = 500) : IApiQuery<ScheduleChangeEntryPayload[]>
    {
        public const string ApiEndpoint = "mobile/schedule/changes/byPupil";
    }
}