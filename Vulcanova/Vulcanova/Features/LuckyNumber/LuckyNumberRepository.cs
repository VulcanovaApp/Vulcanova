using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vulcanova.Core.Data;

namespace Vulcanova.Features.LuckyNumber
{
    public class LuckyNumberRepository : ILuckyNumberRepository
    {
        private readonly AppDbContext _dbContext;

        public LuckyNumberRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LuckyNumber> FindForAccountAndConstituentAsync(int accountId, int constituentId,
            DateTime date)
        {
            return await _dbContext.LuckyNumbers.FirstOrDefaultAsync(l =>
                l.ConstituentId == constituentId && l.AccountId == accountId && date.Date == l.Date.Date);
        }

        public async Task AddAsync(LuckyNumber luckyNumber)
        {
            await _dbContext.LuckyNumbers.AddAsync(luckyNumber);
            await _dbContext.SaveChangesAsync();
        }
    }
}