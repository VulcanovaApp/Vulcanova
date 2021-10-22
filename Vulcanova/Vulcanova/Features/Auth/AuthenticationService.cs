using System;
using System.Threading.Tasks;
using Vulcanova.Core.Uonet;
using Vulcanova.Uonet;
using Vulcanova.Uonet.Api.Auth;

namespace Vulcanova.Features.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IApiClientFactory _apiClientFactory;

        public AuthenticationService(IApiClientFactory apiClientFactory)
        {
            _apiClientFactory = apiClientFactory;
        }

        public async Task<bool> AuthenticateAsync(string token, string pin, string instanceUrl)
        {
            var (x509Certificate2, _, _) = await ClientIdentityProvider.GetOrCreateClientIdentityAsync();

            var request = new RegisterClientRequest
            {
                OS = Constants.AppOs,
                DeviceModel = Constants.DeviceModel,
                Certificate = Convert.ToBase64String(x509Certificate2.RawData),
                CertificateType = "X509",
                CertificateThumbprint = x509Certificate2.Thumbprint,
                PIN = pin,
                SecurityToken = token,
                SelfIdentifier = Guid.NewGuid().ToString()
            };

            var client = _apiClientFactory.GetForApiInstanceUrl(instanceUrl);

            await client.SendRequestAsync("api/mobile/register/new", request);
            
            return true;
        }
    }
}