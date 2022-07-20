using System;

namespace Vulcanova.Features.Auth.Accounts;

public class ConstituentUnit
{
    public int Id { get; set; }
    public string Short { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Patron { get; set; }
    public Guid SchoolTopic { get; set; }
}