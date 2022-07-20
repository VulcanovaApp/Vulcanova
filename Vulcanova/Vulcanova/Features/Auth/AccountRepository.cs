using System.Collections.Generic;
using System.Threading.Tasks;
using LiteDB.Async;
using Vulcanova.Features.Auth.Accounts;

namespace Vulcanova.Features.Auth;

public class AccountRepository : IAccountRepository
{
    private readonly LiteDatabaseAsync _db;

    public AccountRepository(LiteDatabaseAsync db)
    {
        _db = db;
    }

    public async Task AddAccountsAsync(IEnumerable<Account> accounts)
    {
        await _db.GetCollection<Account>().InsertBulkAsync(accounts);
    }

    public async Task<Account> GetActiveAccountAsync()
    {
        return await _db.GetCollection<Account>()
            .FindOneAsync(a => a.IsActive).ConfigureAwait(false);
    }

    public async Task<Account> GetByIdAsync(int id)
    {
        return await _db.GetCollection<Account>()
            .FindByIdAsync(id);
    }

    public async Task UpdateAccountAsync(Account account)
    {
        await _db.GetCollection<Account>().UpdateAsync(account);
    }

    public async Task DeleteByIdAsync(int id)
    {
        await _db.GetCollection<Account>().DeleteAsync(id);
    }
}