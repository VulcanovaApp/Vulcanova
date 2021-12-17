using System.Linq;
using Vulcanova.Features.Auth.Accounts;

namespace Vulcanova.Features.Shared
{
    public static class AccountExtensions
    {
        public static Period GetCurrentPeriod(this Account account)
            => account.Periods.First(p => p.Current);
    }
}