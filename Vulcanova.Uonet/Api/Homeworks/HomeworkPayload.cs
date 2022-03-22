using System;
using System.Collections.Generic;
using Vulcanova.Uonet.Api.Common.Models;

namespace Vulcanova.Uonet.Api.Homeworks
{
    public class HomeworkPayload
    {
        public long Id { get; set; }
        public Guid Key { get; set; }
        public long IdPupil { get; set; }
        public long IdHomework { get; set; }
        public string Content { get; set; }
        public bool IsAnswerRequired { get; set; }
        public AnswerDeadline DateCreated { get; set; }
        public AnswerDeadline Date { get; set; }
        public AnswerDeadline Deadline { get; set; }
        public object AnswerDate { get; set; }
        public AnswerDeadline AnswerDeadline { get; set; }
        public Teacher Creator { get; set; }
        public Subject Subject { get; set; }
        public List<object> Attachments { get; set; }
    }

    public class AnswerDeadline
    {
        public long Timestamp { get; set; }
        public Date Date { get; set; }
        public string DateDisplay { get; set; }
        public Date Time { get; set; }
    }
}