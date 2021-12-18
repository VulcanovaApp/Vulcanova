using System;
using System.Threading.Tasks;
using Vulcanova.Core.Uonet;
using Vulcanova.Features.Auth;
using Vulcanova.Uonet.Api.LuckyNumber;

namespace Vulcanova.Features.LuckyNumber
{
    public class LuckyNumberService : ILuckyNumberService
    {
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IAccountRepository _accountRepository;
        private readonly ILuckyNumberRepository _luckyNumberRepository;

        public LuckyNumberService(
            IApiClientFactory apiClientFactory,
            IAccountRepository accountRepository,
            ILuckyNumberRepository luckyNumberRepository)
        {
            _apiClientFactory = apiClientFactory;
            _accountRepository = accountRepository;
            _luckyNumberRepository = luckyNumberRepository;
        }

        public async Task<LuckyNumber> GetLuckyNumberAsync(int accountId, DateTime dateTime)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            var luckyNumber = await _luckyNumberRepository.FindForAccountAndConstituentAsync(
                accountId,
                account.ConstituentUnit.Id, 
                dateTime);

            if (luckyNumber != null)
            {
                return luckyNumber;
            }
            
            var result = await FetchLuckyNumberAsync(account.ConstituentUnit.Id, dateTime, account.Unit.RestUrl);
            
            luckyNumber = new LuckyNumber
            {
                AccountId = accountId,
                ConstituentId = account.ConstituentUnit.Id,
                Date = result.Day,
                Number = result.Number
            };

            await _luckyNumberRepository.AddAsync(luckyNumber);

            return luckyNumber;
        }

        private async Task<LuckyNumberPayload> FetchLuckyNumberAsync(int constituentId, DateTime dateTime, string restUrl)
        {
            var apiClient = _apiClientFactory.GetForApiInstanceUrl(restUrl);

            var query = new GetLuckyNumberQuery(constituentId, dateTime);

            var result = await apiClient.GetAsync(GetLuckyNumberQuery.ApiEndpoint, query);

            return result.Envelope;
        }
    }
}