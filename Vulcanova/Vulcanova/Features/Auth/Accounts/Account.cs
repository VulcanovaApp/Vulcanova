namespace Vulcanova.Features.Auth.Accounts
{
    public class Account
    {
        public int Id { get; set; }
        public Pupil Pupil { get; set; }
        public Unit Unit { get; set; }
        public ConstituentUnit ConstituentUnit { get; set; }
        public bool IsActive { get; set; }
    }
}