using System.Threading.Tasks;

namespace Vulcanova.Features.Auth
{
    public interface IAuthenticationService
    {
        Task<Account[]> AuthenticateAsync(string token, string pin, string instanceUrl);
    }
}