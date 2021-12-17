namespace Vulcanova.Features.Shared
{
    public interface IPeriodService
    {
        PeriodResult GetCurrentPeriod(int accountId);
        PeriodResult ChangePeriod(int accountId, PeriodChangeDirection direction);
    }
}