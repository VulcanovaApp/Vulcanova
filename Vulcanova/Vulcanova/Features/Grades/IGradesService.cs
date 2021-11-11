using System.Threading.Tasks;

namespace Vulcanova.Features.Grades
{
    public interface IGradesService
    {
        Task<Grade[]> GetCurrentPeriodGradesAsync(int accountId, bool forceUpdate);
    }
}