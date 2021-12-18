using System.Threading.Tasks;

namespace Vulcanova.Features.Shared
{
    public interface IPeriodService
    {
        Task<PeriodResult> GetCurrentPeriodAsync(int accountId);
        Task<PeriodResult> ChangePeriodAsync(int accountId, PeriodChangeDirection direction);
    }
}