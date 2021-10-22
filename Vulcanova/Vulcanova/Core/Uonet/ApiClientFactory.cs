using System;
using Vulcanova.Uonet.Api;
using Vulcanova.Uonet.Signing;

namespace Vulcanova.Core.Uonet
{
    public class ApiClientFactory : IApiClientFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ApiClientFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IApiClient GetForApiInstanceUrl(string apiInstanceUrl)
        {
            var signer = (IRequestSigner)_serviceProvider.GetService(typeof(IRequestSigner));
            return new ApiClient(signer, apiInstanceUrl);
        }
    }
}