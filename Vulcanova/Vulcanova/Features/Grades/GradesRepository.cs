using System.Collections.Generic;
using System.Linq;
using LiteDB;

namespace Vulcanova.Features.Grades
{
    public class GradesRepository : IGradesRepository
    {
        private readonly LiteDatabase _db;

        public GradesRepository(LiteDatabase db)
        {
            _db = db;
        }

        public IEnumerable<Grade> GetGradesForPupil(int accountId, int pupilId, int periodId)
        {
            return _db.GetCollection<Grade>()
                .Find(g => g.PupilId == pupilId && g.AccountId == accountId && g.Column.PeriodId == periodId)
                .OrderByDescending(g => g.DateCreated);
        }

        public void UpdatePupilGrades(int accountId, int pupilId, IEnumerable<Grade> newGrades)
        {
            _db.GetCollection<Grade>().Upsert(newGrades);
        }
    }
}