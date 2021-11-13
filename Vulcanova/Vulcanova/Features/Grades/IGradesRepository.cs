using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vulcanova.Features.Grades
{
    public interface IGradesRepository
    {
        Task<IReadOnlyList<Grade>> GetGradesForPupilAsync(int accountId, int pupilId);
        Task UpdatePupilGradesAsync(int accountId, int pupilId, IEnumerable<Grade> newGrades);
    }
}