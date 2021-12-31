using System.Text.Json.Serialization;
using Vulcanova.Uonet.Api.Common.Models;

namespace Vulcanova.Uonet.Api.Grades
{
    public class GradesSummaryEntryPayload
    {
        public int Id { get; set; }
        public int PupilId { get; set; }
        public int PeriodId { get; set; }
        public Subject Subject { get; set; }
        [JsonPropertyName("Entry_1")]
        public string Entry1 { get; set; }
        [JsonPropertyName("Entry_2")]
        public string Entry2 { get; set; }
        [JsonPropertyName("Entry_3")]
        public string Entry3 { get; set; }
        public Date DateModify { get; set; }
    }
}