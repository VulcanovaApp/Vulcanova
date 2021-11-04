using System.Threading.Tasks;

namespace Vulcanova.Features.Grades
{
    public interface IGradesService
    {
        Task<Grade[]> GetGradesAsync(int accountId, int periodId, bool forceUpdate);
    }
}