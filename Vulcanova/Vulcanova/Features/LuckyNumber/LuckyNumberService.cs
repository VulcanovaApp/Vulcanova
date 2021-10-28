using System;
using System.Threading.Tasks;
using Vulcanova.Core.Uonet;
using Vulcanova.Uonet.Api.LuckyNumber;

namespace Vulcanova.Features.LuckyNumber
{
    public class LuckyNumberService : ILuckyNumberService
    {
        private readonly IApiClientFactory _apiClientFactory;

        public LuckyNumberService(IApiClientFactory apiClientFactory)
        {
            _apiClientFactory = apiClientFactory;
        }

        public async Task<int> GetLuckyNumberAsync(long constituentId, string restUri)
        {
            var apiClient = _apiClientFactory.GetForApiInstanceUrl(restUri);

            var query = new GetLuckyNumberQuery
            {
                ConstituentId = constituentId,
                Day = DateTime.Now
            };

            var result = await apiClient.GetAsync(GetLuckyNumberQuery.ApiEndpoint, query);

            return result.Envelope.Number;
        }
    }
}