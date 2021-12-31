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
    
    public class Date
    {
        public long Timestamp { get; set; }
        // public DateTime Date { get; set; }
        public string DateDisplay { get; set; }
        // public DateTime Time { get; set; }
    }
}