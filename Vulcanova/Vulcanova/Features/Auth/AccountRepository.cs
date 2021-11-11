using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vulcanova.Core.Data;
using Vulcanova.Features.Auth.Accounts;

namespace Vulcanova.Features.Auth
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _dbContext;

        public AccountRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAccounts(IEnumerable<Account> accounts)
        {
            await _dbContext.AddRangeAsync(accounts);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Account> GetActiveAccount()
        {
            return await ApplyIncludeQueries(_dbContext.Accounts)
                .FirstOrDefaultAsync(a => a.IsActive);
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            return await ApplyIncludeQueries(_dbContext.Accounts)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        private static IQueryable<Account> ApplyIncludeQueries(IQueryable<Account> dbSet)
            => dbSet
                .Include(a => a.Periods)
                .Include(a => a.Pupil)
                .Include(a => a.Unit)
                .Include(a => a.ConstituentUnit);
    }
}