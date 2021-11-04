using System;
using System.Collections.Generic;

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
        public IEnumerable<KeyValuePair<string, string>> GetPropertyKeyValuePairs()
        {
            return new KeyValuePair<string, string>[]
            {
                new("unitId", UnitId.ToString()),
                new("pupilId", PupilId.ToString()),
                new("periodId", PeriodId.ToString()),
                new("lastSyncDate", LastSyncDate.ToString("yyyy-MM-dd")),
                new("pageSize", PageSize.ToString()),
                new("lastId", LastId.ToString())
            };
        }

        public const string ApiEndpoint = "mobile/grade/byPupil";
    }
}