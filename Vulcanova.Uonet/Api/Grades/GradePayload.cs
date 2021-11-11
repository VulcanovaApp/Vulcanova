using System;

namespace Vulcanova.Uonet.Api.Grades
{
    public class GradePayload
    {
        public int Id { get; set; }
        public Guid Key { get; set; }
        public int PupilId { get; set; }
        public string ContentRaw { get; set; }
        public string Content { get; set; }
        public string Comment { get; set; }
        public decimal? Value { get; set; }
        public object Numerator { get; set; }
        public object Denominator { get; set; }
        public Date DateCreated { get; set; }
        public Date DateModify { get; set; }
        public Creator Creator { get; set; }
        public Creator Modifier { get; set; }
        public Column Column { get; set; }
    }

    public class Column
    {
        public int Id { get; set; }
        public Guid Key { get; set; }
        public int PeriodId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Group { get; set; }
        public int Number { get; set; }
        public uint Color { get; set; }
        public decimal Weight { get; set; }
        public Subject Subject { get; set; }
        public Category Category { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class Subject
    {
        public int Id { get; set; }
        public Guid Key { get; set; }
        public string Name { get; set; }
        public string Kod { get; set; }
        public int Position { get; set; }
    }

    public class Creator
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class Date
    {
        public long Timestamp { get; set; }
        // public DateTime DateDate { get; set; }
        public string DateDisplay { get; set; }
        // public DateTime Time { get; set; }
    }
}
