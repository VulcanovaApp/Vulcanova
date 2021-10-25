using System;
using System.Collections.Generic;

namespace Vulcanova.Uonet.Api.LuckyNumber
{
    public class GetLuckyNumberQuery : IApiQuery<LuckyNumberPayload>
    {
        public long ConstituentId { get; set; }
        public DateTime Day { get; set; }

        public IEnumerable<KeyValuePair<string, string>> GetPropertyKeyValuePairs() =>
            new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("constituentId", ConstituentId.ToString()),
                new KeyValuePair<string, string>("day", Day.ToString("yyyy-MM-dd"))
            };

        public const string ApiEndpoint = "mobile/school/lucky";
    }
}