using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Vulcanova.Core.Uonet;
using Vulcanova.Features.Auth;
using Vulcanova.Features.Auth.Accounts;
using Vulcanova.Uonet.Api.Exams;
using Vulcanova.Uonet.Api.Homeworks;

namespace Vulcanova.Features.Homeworks
{
    public class HomeworksService : UonetResourceProvider, IHomeworksService
    {
        private readonly ApiClientFactory _apiClientFactory;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IHomeworksRepository _homeworksRepository;
        
        public HomeworksService(ApiClientFactory apiClientFactory, IMapper mapper, IAccountRepository accountRepository, IHomeworksRepository homeworksRepository)
        {
            _apiClientFactory = apiClientFactory;
            _mapper = mapper;
            _accountRepository = accountRepository;
            _homeworksRepository = homeworksRepository;
        }

        public IObservable<IEnumerable<HomeworkEntry>> GetHomeworksByDateRange(int accountId, int periodId, DateTime from, DateTime to,
            bool forceSync = false)
        {
            return Observable.Create<IEnumerable<HomeworkEntry>>(async observer =>
            {
                var account = await _accountRepository.GetByIdAsync(accountId);

                var resourceKey = GetHomeworksResourceKey(account, from, to, periodId);

                var items = await _homeworksRepository.GetHomeworksForPupilAsync(account.Id, account.Pupil.Id);
                
                observer.OnNext(items);

                if (ShouldSync(resourceKey) || forceSync)
                {
                    var onlineEntities = await FetchHomeworks(account, periodId);

                    await _homeworksRepository.UpdateHomeworksEntriesAsync(onlineEntities, account.Id);

                    SetJustSynced(resourceKey);

                    items = await _homeworksRepository.GetHomeworksForPupilAsync(account.Id, account.Pupil.Id);
                    
                    observer.OnNext(items);
                }
                
                observer.OnCompleted();
            });
        }

        private async Task<HomeworkEntry[]> FetchHomeworks(Account account, int periodId)
        {
            var query = new GetHomeworksByPupilQuery(account.Unit.Id, periodId, DateTime.MinValue);

            var client = _apiClientFactory.GetForApiInstanceUrl(account.Unit.RestUrl);

            var response = await client.GetAsync(GetExamsByPupilQuery.ApiEndpoint, query);

            var entries = response.Envelope.Select(_mapper.Map<HomeworkEntry>).ToArray();

            return entries;
        }

        private static string GetHomeworksResourceKey(Account account, DateTime from, DateTime to, int periodId)
            => $"Homeworks_{account.Id}_{from.ToShortDateString()}-{to.ToLongDateString()}_{periodId}";

        protected override TimeSpan OfflineDataLifespan => TimeSpan.FromHours(1);
    }
}