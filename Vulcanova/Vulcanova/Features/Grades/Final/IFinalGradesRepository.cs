using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vulcanova.Features.Grades.Final
{
    public interface IFinalGradesRepository
    {
        Task<IEnumerable<FinalGradesEntry>> GetFinalGradesForPupilAsync(int accountId, int pupilId, int periodId);
        Task UpdatePupilFinalGradesAsync(IEnumerable<FinalGradesEntry> newGrades);
    }
}