using System.Collections.Generic;
using System.Threading.Tasks;
using Vulcanova.Features.Auth.Accounts;

namespace Vulcanova.Features.Auth
{
    public interface IAccountRepository
    {
        Task AddAccounts(IEnumerable<Account> accounts);
        Task<Account> GetActiveAccount();
        Task<Account> GetByIdAsync(int id);
    }
}