using System;

namespace Vulcanova.Features.Auth.Accounts;

public class Unit
{
    public int Id { get; set; }
    public string Symbol { get; set; }
    public string Short { get; set; }
    public string RestUrl { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Patron { get; set; }
    public string DisplayName { get; set; }
    public Guid SchoolTopic { get; set; }
}