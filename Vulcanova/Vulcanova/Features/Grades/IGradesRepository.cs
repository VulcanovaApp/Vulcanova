using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vulcanova.Features.Grades;

public interface IGradesRepository
{
    Task<IEnumerable<Grade>> GetGradesForPupilAsync(int accountId, int pupilId, int periodId);
    Task UpdatePupilGradesAsync(IEnumerable<Grade> newGrades);
}