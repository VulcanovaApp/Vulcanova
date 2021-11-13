using System.Collections.Generic;

namespace Vulcanova.Features.Grades
{
    public interface IGradesRepository
    {
        IEnumerable<Grade> GetGradesForPupil(int accountId, int pupilId);
        void UpdatePupilGrades(int accountId, int pupilId, IEnumerable<Grade> newGrades);
    }
}