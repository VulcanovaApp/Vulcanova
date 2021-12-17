using System.Collections.Generic;
using LiteDB;
using Vulcanova.Features.Auth.Accounts;

namespace Vulcanova.Features.Auth
{
    public class AccountRepository : IAccountRepository
    {
        private readonly LiteDatabase _db;

        public AccountRepository(LiteDatabase db)
        {
            _db = db;
        }

        public void AddAccounts(IEnumerable<Account> accounts)
        {
            _db.GetCollection<Account>().InsertBulk(accounts);
        }

        public Account GetActiveAccount()
        {
            return _db.GetCollection<Account>()
                .FindOne(a => a.IsActive);
        }

        public Account GetById(int id)
        {
            return _db.GetCollection<Account>()
                .FindById(id);
        }

        public void UpdateAccount(Account account)
        {
            _db.GetCollection<Account>().Update(account);
        }
    }
}