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

namespace Vulcanova.Features.Exams
{
    public class ExamsService : UonetResourceProvider, IExamsService
    {
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IExamsRepository _examsRepository;

        public ExamsService(IApiClientFactory apiClientFactory, IMapper mapper,
            IAccountRepository accountRepository, IExamsRepository examsRepository)
        {
            _apiClientFactory = apiClientFactory;
            _mapper = mapper;
            _accountRepository = accountRepository;
            _examsRepository = examsRepository;
        }

        public IObservable<IEnumerable<Exam>> GetExamsByDateRange(int accountId, DateTime from, DateTime to,
            bool forceSync = false)
        {
            return Observable.Create<IEnumerable<Exam>>(async observer =>
            {
                var account = await _accountRepository.GetByIdAsync(accountId);

                var resourceKey = GetExamsResourceKey(account, from, to);

                var items = await _examsRepository.GetExamsForPupilAsync(account.Id, from, to);

                observer.OnNext(items);

                if (ShouldSync(resourceKey) || forceSync)
                {
                    var onlineEntries = await FetchExamsAsync(account);

                    await _examsRepository.UpdateExamsForPupilAsync(accountId, onlineEntries);

                    SetJustSynced(resourceKey);

                    items = await _examsRepository.GetExamsForPupilAsync(account.Id, from, to);

                    observer.OnNext(items);
                }

                observer.OnCompleted();
            });
        }

        private async Task<Exam[]> FetchExamsAsync(Account account)
        {
            var query = new GetExamsByPupilQuery(account.Unit.Id, account.Pupil.Id, DateTime.MinValue, 500);

            var client = _apiClientFactory.GetForApiInstanceUrl(account.Unit.RestUrl);

            var response = await client.GetAsync(GetExamsByPupilQuery.ApiEndpoint, query);

            var entries = response.Envelope.Select(_mapper.Map<Exam>).ToArray();

            foreach (var entry in entries)
            {
                entry.AccountId = account.Id;
            }

            return entries;
        }

        private static string GetExamsResourceKey(Account account, DateTime from, DateTime to)
            => $"Timetable_{account.Id}_{from.ToShortDateString()}_{to.ToLongDateString()}";

        protected override TimeSpan OfflineDataLifespan => TimeSpan.FromHours(1);
    }
}