using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.EntityFrameworkCore;
using Vulcanova.Core.Data;

namespace Vulcanova.Features.Grades
{
    public class GradesRepository : IGradesRepository
    {
        private readonly LiteDatabase _db;

        public GradesRepository(LiteDatabase db)
        {
            _db = db;
        }

        public IEnumerable<Grade> GetGradesForPupil(int accountId, int pupilId)
        {
            return _db.GetCollection<Grade>()
                .Find(g => g.PupilId == pupilId && g.AccountId == accountId);
        }

        public void UpdatePupilGrades(int accountId, int pupilId, IEnumerable<Grade> newGrades)
        {
            _db.GetCollection<Grade>().DeleteMany(g => g.PupilId == pupilId && g.AccountId == accountId);
            _db.GetCollection<Grade>().InsertBulk(newGrades);
        }
    }
}