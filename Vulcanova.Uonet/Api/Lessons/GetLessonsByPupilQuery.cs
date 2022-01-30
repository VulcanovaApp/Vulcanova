using System;

namespace Vulcanova.Uonet.Api.Lessons
{
    public record GetLessonsByPupilQuery(
        int PupilId,
        DateTime DateFrom,
        DateTime DateTo,
        DateTime LastSyncDate,
        int LastId = int.MinValue,
        int PageSize = 500) : IApiQuery<LessonPayload[]>
    {
        public const string ApiEndpoint = "mobile/lesson/byPupil";
    }
}