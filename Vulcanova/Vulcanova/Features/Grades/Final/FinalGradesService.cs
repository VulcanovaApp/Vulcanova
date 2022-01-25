using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Vulcanova.Core.Uonet;
using Vulcanova.Features.Auth;
using Vulcanova.Features.Auth.Accounts;
using Vulcanova.Uonet.Api.Grades;

namespace Vulcanova.Features.Grades.Final
{
    public class FinalGradesService : UonetResourceProvider, IFinalGradesService
    {
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IAccountRepository _accountRepository;
        private readonly IFinalGradesRepository _gradesRepository;
        private readonly IMapper _mapper;

        public FinalGradesService(
            IApiClientFactory apiClientFactory,
            IAccountRepository accountRepository,
            IMapper mapper,
            IFinalGradesRepository gradesRepository)
        {
            _apiClientFactory = apiClientFactory;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _gradesRepository = gradesRepository;
        }

        public IObservable<IEnumerable<FinalGradesEntry>> GetPeriodGrades(int accountId, int periodId,
            bool forceSync = false)
        {
            return Observable.Create<IEnumerable<FinalGradesEntry>>(async observer =>
            {
                var account = await _accountRepository.GetByIdAsync(accountId);

                var resourceKey = GetGradesSummaryResourceKey(account, periodId);

                var items = await _gradesRepository.GetFinalGradesForPupilAsync(account.Id, account.Pupil.Id,
                    periodId);

                observer.OnNext(items);

                if (ShouldSync(resourceKey) || forceSync)
                {
                    var onlineGrades = await FetchPeriodGradesAsync(account, periodId);

                    await _gradesRepository.UpdatePupilFinalGradesAsync(onlineGrades);

                    SetJustSynced(resourceKey);

                    items = await _gradesRepository.GetFinalGradesForPupilAsync(account.Id, account.Pupil.Id,
                        periodId);

                    observer.OnNext(items);
                }

                observer.OnCompleted();
            });
        }

        private async Task<FinalGradesEntry[]> FetchPeriodGradesAsync(Account account, int periodId)
        {
            var query = new GetGradesSummaryByPupilQuery(account.Unit.Id, account.Pupil.Id, periodId, 500);

            var client = _apiClientFactory.GetForApiInstanceUrl(account.Unit.RestUrl);

            var response = await client.GetAsync(GetGradesSummaryByPupilQuery.ApiEndpoint, query);

            var domainGrades = response.Envelope.Select(_mapper.Map<FinalGradesEntry>).ToArray();

            foreach (var grade in domainGrades)
            {
                grade.AccountId = account.Id;
            }

            return domainGrades;
        }

        private static string GetGradesSummaryResourceKey(Account account, int periodId)
            => $"GradesSummary_{account.Id}_{account.Pupil.Id}_{periodId}";

        protected override TimeSpan OfflineDataLifespan => TimeSpan.FromHours(1);
    }
}