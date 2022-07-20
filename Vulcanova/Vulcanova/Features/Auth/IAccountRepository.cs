using System.Collections.Generic;
using System.Threading.Tasks;
using Vulcanova.Features.Auth.Accounts;

namespace Vulcanova.Features.Auth
{
    public interface IAccountRepository
    {
        Task AddAccountsAsync(IEnumerable<Account> accounts);
        Task<Account> GetActiveAccountAsync();
        Task<Account> GetByIdAsync(int id);
        Task UpdateAccountAsync(Account account);
        Task DeleteByIdAsync(int id);
    }
}