using Vulcanova.Uonet.Api.Auth;

namespace Vulcanova.Features.Auth
{
    public class Account : AccountPayload
    {
        public bool IsActive { get; set; }
    }
}