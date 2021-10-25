using System;

namespace Vulcanova.Uonet.Api.Auth
{
    public class AccountPayload
    {
        public string TopLevelPartition { get; set; }
        public string Partition { get; set; }
        public string ClassDisplay { get; set; }
        public string InfoDisplay { get; set; }
        public bool FullSync { get; set; }
        public SenderEntry SenderEntry { get; set; }
        public Login Login { get; set; }
        public Unit Unit { get; set; }
        public ConstituentUnit ConstituentUnit { get; set; }
        public string[] Capabilities { get; set; }
        public Educator[] Educators { get; set; }
        public Pupil Pupil { get; set; }
        public Period[] Periods { get; set; }
        public Journal Journal { get; set; }
        public Constraints Constraints { get; set; }
        public long State { get; set; }
        public Policies Policies { get; set; }
        public string Context { get; set; }
    }

    public class ConstituentUnit
    {
        public long Id { get; set; }
        public string Short { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Patron { get; set; }
        public Guid SchoolTopic { get; set; }
    }

    public class Constraints
    {
        public object AbsenceDaysBefore { get; set; }
        public object AbsenceHoursBefore { get; set; }
    }

    public class Educator
    {
        public string Id { get; set; }
        public long LoginId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Initials { get; set; }
        public Role[] Roles { get; set; }
    }

    public class Role
    {
        public string RoleName { get; set; }
        public long RoleOrder { get; set; }
        public string Address { get; set; }
        public string AddressHash { get; set; }
        public object UnitSymbol { get; set; }
        public string ConstituentUnitSymbol { get; set; }
        public string ClassSymbol { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Initials { get; set; }
    }

    public class Journal
    {
        public long Id { get; set; }
        public YearEnd YearStart { get; set; }
        public YearEnd YearEnd { get; set; }
    }

    public class YearEnd
    {
        public long Timestamp { get; set; }
        // FIXME: Breaks serializer
        // public DateTimeOffset Date { get; set; }
        public string DateDisplay { get; set; }
        // FIXME: Breaks serializer
        // public DateTimeOffset Time { get; set; }
    }

    public class Login
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Surname { get; set; }
        public string DisplayName { get; set; }
        public string LoginRole { get; set; }
    }

    public class Period
    {
        public object[] Capabilities { get; set; }
        public long Id { get; set; }
        public long Level { get; set; }
        public long Number { get; set; }
        public YearEnd Start { get; set; }
        public YearEnd End { get; set; }
        public bool Current { get; set; }
        public bool Last { get; set; }
    }

    public class Policies
    {
        public object Privacy { get; set; }
        public object Cookie { get; set; }
        public object InfoEnclosure { get; set; }
        public object AccessDeclaration { get; set; }
    }

    public class Pupil
    {
        public long Id { get; set; }
        public long LoginId { get; set; }
        public string LoginValue { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Surname { get; set; }
        // Why is this a boolean? 🤣
        public bool Sex { get; set; }
    }

    public class SenderEntry
    {
        public long LoginId { get; set; }
        public string Address { get; set; }
        public string AddressHash { get; set; }
        public string Initials { get; set; }
    }

    public class Unit
    {
        public long Id { get; set; }
        public string Symbol { get; set; }
        public string Short { get; set; }
        public Uri RestUrl { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Patron { get; set; }
        public string DisplayName { get; set; }
        public Guid SchoolTopic { get; set; }
    }

    public class Status
    {
        public long Code { get; set; }
        public string Message { get; set; }
    }
}
