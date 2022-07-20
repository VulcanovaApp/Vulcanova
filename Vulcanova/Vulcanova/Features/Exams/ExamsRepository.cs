using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiteDB.Async;

namespace Vulcanova.Features.Exams;

public class ExamsRepository : IExamsRepository
{
    private readonly LiteDatabaseAsync _db;

    public ExamsRepository(LiteDatabaseAsync db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Exam>> GetExamsForPupilAsync(int accountId, DateTime from, DateTime to)
    {
        return await _db.GetCollection<Exam>()
            .FindAsync(e => e.AccountId == accountId
                            && e.Deadline >= from
                            && e.Deadline <= to);
    }

    public async Task UpdateExamsForPupilAsync(int accountId, IEnumerable<Exam> entries)
    {
        await _db.GetCollection<Exam>().DeleteManyAsync(e => e.AccountId == accountId);

        await _db.GetCollection<Exam>().UpsertAsync(entries);
    }
}