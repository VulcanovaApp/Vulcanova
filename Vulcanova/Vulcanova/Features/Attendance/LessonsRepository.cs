using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiteDB.Async;

namespace Vulcanova.Features.Attendance
{
    public class LessonsRepository : ILessonsRepository
    {
        private readonly LiteDatabaseAsync _db;

        public LessonsRepository(LiteDatabaseAsync db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Lesson>> GetLessonsForAccountAsync(int accountId, DateTime monthAndYear)
        {
            return await _db.GetCollection<Lesson>()
                .FindAsync(g =>
                    g.AccountId == accountId && g.Date.Year == monthAndYear.Year && g.Date.Month == monthAndYear.Month);
        }

        public async Task UpsertLessonsForAccountAsync(IEnumerable<Lesson> entries, int accountId, DateTime monthAndYear)
        {
            await _db.GetCollection<Lesson>()
                .DeleteManyAsync(g =>
                    g.Date.Year == monthAndYear.Year && g.Date.Month == monthAndYear.Month && g.AccountId == accountId);

            await _db.GetCollection<Lesson>().UpsertAsync(entries);
        }
    }
}