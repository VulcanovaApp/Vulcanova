using System;
using System.Collections.Generic;
using Vulcanova.Uonet.Api.Common.Models;

namespace Vulcanova.Features.Homeworks
{
    public class HomeworkEntry
    {
        public long Id { get; set; }
        public Guid Key { get; set; }
        public long IdPupil { get; set; }
        public int AccountId { get; set; }
        public long IdHomework { get; set; }
        public string Content { get; set; }
        public bool IsAnswerRequired { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime Date { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime AnswerDate { get; set; }
        public DateTime AnswerDeadline { get; set; }
        public Teacher Creator { get; set; }
        public Subject Subject { get; set; }
        public bool Visible { get; set; }
        public int PeriodId { get; set; }
    }
}