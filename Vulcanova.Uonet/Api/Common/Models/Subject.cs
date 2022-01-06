using System;

namespace Vulcanova.Uonet.Api.Common.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public Guid Key { get; set; }
        public string Name { get; set; }
        public string Kod { get; set; }
        public int Position { get; set; }
    }
}