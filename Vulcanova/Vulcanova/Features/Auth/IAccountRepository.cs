using System.Collections.Generic;
using Vulcanova.Features.Auth.Accounts;

namespace Vulcanova.Features.Auth
{
    public interface IAccountRepository
    {
        void AddAccounts(IEnumerable<Account> accounts);
        Account GetActiveAccount();
        Account GetById(int id);
    }
}