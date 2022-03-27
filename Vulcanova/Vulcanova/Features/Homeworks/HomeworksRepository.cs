using System.Collections.Generic;
using System.Threading.Tasks;
using LiteDB.Async;

namespace Vulcanova.Features.Homeworks
{
    public class HomeworksRepository : IHomeworksRepository
    {
        private readonly LiteDatabaseAsync _db;
        
        public HomeworksRepository(LiteDatabaseAsync db)
        {
            _db = db;
        }

        public async Task<IEnumerable<HomeworkEntry>> GetHomeworksForPupilAsync(int accountId, int pupilId)
        {
            return await _db.GetCollection<HomeworkEntry>()
                .FindAsync(h => h.IdPupil == pupilId && h.AccountId == accountId);
        }

        public async Task UpdateHomeworksEntriesAsync(IEnumerable<HomeworkEntry> entries, int accountId)
        {
            await _db.GetCollection<HomeworkEntry>()
                .DeleteManyAsync(h => h.AccountId == accountId);
            
            await _db.GetCollection<HomeworkEntry>().UpsertAsync(entries);
        }
    }
}