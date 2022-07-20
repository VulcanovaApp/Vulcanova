using System;
using System.Threading.Tasks;
using LiteDB.Async;

namespace Vulcanova.Features.LuckyNumber;

public class LuckyNumberRepository : ILuckyNumberRepository
{
    private readonly LiteDatabaseAsync _db;

    public LuckyNumberRepository(LiteDatabaseAsync db)
    {
        _db = db;
    }

    public async Task<LuckyNumber> FindForAccountAndConstituentAsync(int accountId, int constituentId,
        DateTime date)
    {
        return await _db.GetCollection<LuckyNumber>().FindOneAsync(l =>
            l.ConstituentId == constituentId 
            && l.AccountId == accountId 
            && date.Date == l.Date.Date);
    }

    public async Task AddAsync(LuckyNumber luckyNumber)
    {
        await _db.GetCollection<LuckyNumber>().InsertAsync(luckyNumber);
    }
}