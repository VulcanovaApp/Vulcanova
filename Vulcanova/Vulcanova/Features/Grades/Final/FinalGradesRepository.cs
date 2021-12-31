using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB.Async;

namespace Vulcanova.Features.Grades.Final
{
    public class FinalGradesRepository : IFinalGradesRepository
    {
        private readonly LiteDatabaseAsync _db;

        public FinalGradesRepository(LiteDatabaseAsync db)
        {
            _db = db;
        }

        public async Task<IEnumerable<FinalGradesEntry>> GetFinalGradesForPupilAsync(int accountId, int pupilId, int periodId)
        {
            return (await _db.GetCollection<FinalGradesEntry>()
                    .FindAsync(g => g.PupilId == pupilId && g.AccountId == accountId && g.PeriodId == periodId))
                .OrderBy(g => g.Subject.Name);
        }

        public async Task UpdatePupilFinalGradesAsync(int accountId, int pupilId, IEnumerable<FinalGradesEntry> newGrades)
        {
            await _db.GetCollection<FinalGradesEntry>().UpsertAsync(newGrades);
        }
    }
}