using System;

namespace Vulcanova.Features.Shared;

public class Period
{
    public int Id { get; set; }
    public int Level { get; set; }
    public int Number { get; set; }
    public bool Current { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}