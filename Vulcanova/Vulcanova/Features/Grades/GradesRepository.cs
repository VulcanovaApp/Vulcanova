using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB.Async;

namespace Vulcanova.Features.Grades;

public class GradesRepository : IGradesRepository
{
    private readonly LiteDatabaseAsync _db;

    public GradesRepository(LiteDatabaseAsync db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Grade>> GetGradesForPupilAsync(int accountId, int pupilId, int periodId)
    {
        return (await _db.GetCollection<Grade>()
                .FindAsync(g => g.PupilId == pupilId && g.AccountId == accountId && g.Column.PeriodId == periodId))
            .OrderBy(g => g.Column.Subject.Name)
            .ThenBy(g => g.DateCreated);
    }

    public async Task UpdatePupilGradesAsync(IEnumerable<Grade> newGrades)
    {
        await _db.GetCollection<Grade>().UpsertAsync(newGrades);
    }
}