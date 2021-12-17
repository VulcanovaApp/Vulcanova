using System.Collections.Generic;

namespace Vulcanova.Features.Grades
{
    public interface IGradesRepository
    {
        IEnumerable<Grade> GetGradesForPupil(int accountId, int pupilId, int periodId);
        void UpdatePupilGrades(int accountId, int pupilId, IEnumerable<Grade> newGrades);
    }
}