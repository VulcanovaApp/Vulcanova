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
        public DateTime DateCreated { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? AnswerDate { get; set; }
        public DateTime AnswerDeadline { get; set; }
        public string CreatorName { get; set; }
        public Subject Subject { get; set; }
    }
}