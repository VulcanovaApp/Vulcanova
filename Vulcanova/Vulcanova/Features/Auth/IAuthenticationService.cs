using System.Threading.Tasks;

namespace Vulcanova.Features.Auth
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(string token, string symbol, string pin);
    }
}