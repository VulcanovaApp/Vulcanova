using System;
using Vulcanova.Uonet.Api.Common.Models;

namespace Vulcanova.Features.Homework
{
    public class Homework
    {
        public long Id { get; set; }
        public Guid Key { get; set; }
        public long PupilId { get; set; }
        public long AccountId { get; set; }
        public long HomeworkId { get; set; }
        public string Content { get; set; }
        public bool IsAnswerRequired { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public DateTimeOffset AnswerDate { get; set; }
        public DateTimeOffset AnswerDeadline { get; set; }
        public string CreatorName { get; set; }
        public Subject Subject { get; set; }
    }
}