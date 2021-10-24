using System.Collections.Generic;
using System.Linq;
using Akavache;

namespace Vulcanova.Features.Auth
{
    public static class AuthBlobCacheExtensions
    {
        public static void InsertAccounts(this IBlobCache blobCache, IEnumerable<Account> accounts)
            => blobCache.InsertAllObjects(accounts.ToDictionary(a => a.Pupil.Id.ToString(), a => a));

    }
}