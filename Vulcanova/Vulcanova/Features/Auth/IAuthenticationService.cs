using System.Threading.Tasks;
using Vulcanova.Features.Auth.Accounts;

namespace Vulcanova.Features.Auth
{
    public interface IAuthenticationService
    {
        Task<Account[]> AuthenticateAsync(string token, string pin, string instanceUrl);
    }
}