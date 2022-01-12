using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiteDB.Async;

namespace Vulcanova.Features.Timetable
{
    public class TimetableRepository : ITimetableRepository
    {
        private readonly LiteDatabaseAsync _db;

        public TimetableRepository(LiteDatabaseAsync db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TimetableEntry>> GetEntriesForPupilAsync(int accountId, int pupilId,
            DateTime monthAndYear)
        {
            return await _db.GetCollection<TimetableEntry>()
                .FindAsync(g =>
                    g.PupilId == pupilId && g.AccountId == accountId && g.Date.Year == monthAndYear.Year &&
                    g.Date.Month == monthAndYear.Month);
        }

        public async Task UpdatePupilEntriesAsync(IEnumerable<TimetableEntry> entries)
        {
            await _db.GetCollection<TimetableEntry>().UpsertAsync(entries);
        }
    }
}