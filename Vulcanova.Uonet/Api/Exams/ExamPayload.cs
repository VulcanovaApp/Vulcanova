using Vulcanova.Uonet.Api.Common.Models;

namespace Vulcanova.Uonet.Api.Exams
{
    public class ExamPayload
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public Date DateCreated { get; set; }
        public Date DateModify { get; set; }
        public Date Deadline { get; set; }
        public Teacher Creator { get; set; }
        public Subject Subject { get; set; }
        public int PupilId { get; set; }
    }
}