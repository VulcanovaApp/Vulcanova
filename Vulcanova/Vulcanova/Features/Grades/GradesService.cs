using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Vulcanova.Core.Uonet;
using Vulcanova.Features.Auth;
using Vulcanova.Uonet.Api.Grades;

namespace Vulcanova.Features.Grades
{
    public class GradesService : IGradesService
    {
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GradesService(
            IApiClientFactory apiClientFactory,
            IAccountRepository accountRepository,
            IMapper mapper)
        {
            _apiClientFactory = apiClientFactory;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<Grade[]> GetGradesAsync(int accountId, int periodId, bool forceUpdate = true)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            var query = new GetGradesByPupilQuery(account.Unit.Id, account.Pupil.Id, periodId, DateTime.MinValue, 500);

            var client = _apiClientFactory.GetForApiInstanceUrl(account.Unit.RestUrl);

            var response = await client.GetAsync(GetGradesByPupilQuery.ApiEndpoint, query);

            return response.Envelope.Select(_mapper.Map<Grade>).ToArray();
        }
    }
}