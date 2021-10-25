using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Vulcanova.Core.Uonet;
using Vulcanova.Features.Auth.Accounts;
using Vulcanova.Uonet;
using Vulcanova.Uonet.Api.Auth;

namespace Vulcanova.Features.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IMapper _mapper;

        public AuthenticationService(
            IApiClientFactory apiClientFactory,
            IMapper mapper)
        {
            _apiClientFactory = apiClientFactory;
            _mapper = mapper;
        }

        public async Task<Account[]> AuthenticateAsync(string token, string pin, string instanceUrl)
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

            await client.PostAsync(RegisterClientRequest.ApiEndpoint, request);

            var registerHebeResponse = await client.GetAsync(RegisterHebeClientQuery.ApiEndpoint, new RegisterHebeClientQuery());

            return registerHebeResponse.Envelope.Select(_mapper.Map<Account>).ToArray();
        }
    }
}