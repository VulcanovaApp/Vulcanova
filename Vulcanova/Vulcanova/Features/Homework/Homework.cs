using System;
using Vulcanova.Uonet.Api.Common.Models;

namespace Vulcanova.Features.Homework;

public class Homework
{
    public int Id { get; set; }
    public Guid Key { get; set; }
    public int PupilId { get; set; }
    public int AccountId { get; set; }
    public int HomeworkId { get; set; }
    public string Content { get; set; }
    public bool IsAnswerRequired { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime? AnswerDate { get; set; }
    public DateTime? AnswerDeadline { get; set; }
    public string CreatorName { get; set; }
    public Subject Subject { get; set; }
}