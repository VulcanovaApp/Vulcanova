using System.Collections.Generic;
using System.Threading.Tasks;
using LiteDB.Async;

namespace Vulcanova.Features.Homework
{
    public class HomeworkRepository : IHomeworkRepository
    {
        private readonly LiteDatabaseAsync _db;
        
        public HomeworkRepository(LiteDatabaseAsync db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Homework>> GetHomeworksForPupilAsync(int accountId, int pupilId)
        {
            return await _db.GetCollection<Homework>()
                .FindAsync(h => h.IdPupil == pupilId && h.AccountId == accountId);
        }

        public async Task UpdateHomeworksEntriesAsync(IEnumerable<Homework> entries, int accountId)
        {
            await _db.GetCollection<Homework>()
                .DeleteManyAsync(h => h.AccountId == accountId);
            
            await _db.GetCollection<Homework>().UpsertAsync(entries);
        }
    }
}