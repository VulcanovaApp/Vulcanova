using System;

namespace Vulcanova.Features.Shared;

public class Subject
{
    public const int BehaviourSubjectId = -1;

    public int Id { get; set; }
    public Guid Key { get; set; }
    public string Name { get; set; }
    public string Kod { get; set; }
    public int Position { get; set; }
}