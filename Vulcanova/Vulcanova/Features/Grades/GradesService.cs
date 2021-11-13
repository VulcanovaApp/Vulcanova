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

        public IObservable<IEnumerable<Grade>> GetCurrentPeriodGrades(int accountId)
        {
            return Observable.Create<IEnumerable<Grade>>(observer =>
            {
                return Task.Run(async () =>
                {
                    var account = _accountRepository.GetById(accountId);

                    var resourceKey = $"Grades_{accountId}_{account.Pupil.Id}";

                    observer.OnNext(_gradesRepository.GetGradesForPupil(account.Id, account.Pupil.Id));

                    if (ShouldSync(resourceKey))
                    {
                        var onlineGrades = await FetchCurrentPeriodGradesAsync(account);
                        observer.OnNext(onlineGrades);
                    
                        _gradesRepository.UpdatePupilGrades(account.Id, account.Pupil.Id, onlineGrades);
                        
                        SetJustSynced(resourceKey);
                    }

                    observer.OnCompleted();
                });
            });
        }

        private async Task<Grade[]> FetchCurrentPeriodGradesAsync(Account account)
        {
            var periodId = account.Periods.Single(p => p.Current).Id;

            var query = new GetGradesByPupilQuery(account.Unit.Id, account.Pupil.Id, periodId, DateTime.MinValue, 500);

            var client = _apiClientFactory.GetForApiInstanceUrl(account.Unit.RestUrl);

            var response = await client.GetAsync(GetGradesByPupilQuery.ApiEndpoint, query);

            var domainGrades = response.Envelope.Select(_mapper.Map<Grade>).ToArray();

            foreach (var grade in domainGrades)
            {
                grade.AccountId = account.Id;
            }

            return domainGrades;
        }

        protected override TimeSpan OfflineDataLifespan => TimeSpan.FromMinutes(15);
    }
}