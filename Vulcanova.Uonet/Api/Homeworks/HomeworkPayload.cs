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
        public Date DateCreated { get; set; }
        public Date Date { get; set; }
        public Date Deadline { get; set; }
        public Date AnswerDate { get; set; }
        public Date AnswerDeadline { get; set; }
        public Teacher Creator { get; set; }
        public Subject Subject { get; set; }
        public List<object> Attachments { get; set; }
    }
}