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

namespace Vulcanova.Features.Grades
{
    public class GradesService : UonetResourceProvider, IGradesService
    {
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IAccountRepository _accountRepository;
        private readonly IGradesRepository _gradesRepository;
        private readonly IMapper _mapper;

        public GradesService(
            IApiClientFactory apiClientFactory,
            IAccountRepository accountRepository,
            IMapper mapper,
            IGradesRepository gradesRepository)
        {
            _apiClientFactory = apiClientFactory;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _gradesRepository = gradesRepository;
        }

        public IObservable<IEnumerable<Grade>> GetPeriodGrades(int accountId, int periodId, bool forceSync = false)
        {
            return Observable.Create<IEnumerable<Grade>>(observer =>
            {
                return Task.Run(async () =>
                {
                    var account = await _accountRepository.GetByIdAsync(accountId);

                    var resourceKey = GetGradesResourceKey(account, periodId);
                    
                    var items = await _gradesRepository.GetGradesForPupilAsync(account.Id, account.Pupil.Id,
                        periodId);
                    
                    observer.OnNext(items);

                    if (ShouldSync(resourceKey) || forceSync)
                    {
                        var onlineGrades = await FetchPeriodGradesAsync(account, periodId);

                        await _gradesRepository.UpdatePupilGradesAsync(onlineGrades);
                        
                        SetJustSynced(resourceKey);
                        
                        items = await _gradesRepository.GetGradesForPupilAsync(account.Id, account.Pupil.Id,
                            periodId);
                        
                        observer.OnNext(items);
                    }

                    observer.OnCompleted();
                });
            });
        }

        private async Task<Grade[]> FetchPeriodGradesAsync(Account account, int periodId)
        {
            var lastSync = GetLastSync(GetGradesResourceKey(account, periodId));

            var query = new GetGradesByPupilQuery(account.Unit.Id, account.Pupil.Id, periodId, lastSync, 500);

            var client = _apiClientFactory.GetForApiInstanceUrl(account.Unit.RestUrl);

            var response = await client.GetAsync(GetGradesByPupilQuery.ApiEndpoint, query);

            var domainGrades = response.Envelope.Select(_mapper.Map<Grade>).ToArray();

            foreach (var grade in domainGrades)
            {
                grade.AccountId = account.Id;
            }

            return domainGrades;
        }

        private static string GetGradesResourceKey(Account account, int periodId)
            => $"Grades_{account.Id}_{account.Pupil.Id}_{periodId}";

        protected override TimeSpan OfflineDataLifespan => TimeSpan.FromMinutes(15);
    }
}