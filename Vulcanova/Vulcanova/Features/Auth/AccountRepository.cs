using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.EntityFrameworkCore;
using Vulcanova.Core.Data;
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
    }
}