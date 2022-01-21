using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Vulcanova.Core.Uonet;
using Vulcanova.Features.Auth;
using Vulcanova.Features.Auth.Accounts;
using Vulcanova.Uonet.Api.Schedule;

namespace Vulcanova.Features.Timetable.Changes
{
    public class TimetableChangesService : UonetResourceProvider, ITimetableChangesService
    {
        private readonly ITimetableChangesRepository _changesRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IMapper _mapper;

        public TimetableChangesService(ITimetableChangesRepository changesRepository, IAccountRepository accountRepository,
            IApiClientFactory apiClientFactory, IMapper mapper)
        {
            _changesRepository = changesRepository;
            _accountRepository = accountRepository;
            _apiClientFactory = apiClientFactory;
            _mapper = mapper;
        }

        public IObservable<IEnumerable<TimetableChangeEntry>> GetChangesEntriesByMonth(int accountId, DateTime monthAndYear,
            bool forceSync = false)
        {
            return Observable.Create<IEnumerable<TimetableChangeEntry>>(observer =>
            {
                return Task.Run(async () =>
                {
                    var account = await _accountRepository.GetByIdAsync(accountId);

                    var resourceKey = GetTimetableResourceKey(account, monthAndYear);

                    var items = await _changesRepository.GetEntriesForPupilAsync(account.Id, account.Pupil.Id,
                        monthAndYear);

                    observer.OnNext(items);

                    if (ShouldSync(resourceKey) || forceSync)
                    {
                        var onlineEntries = await FetchEntriesForMonthAndYear(account, monthAndYear);

                        await _changesRepository.UpsertEntriesAsync(onlineEntries);

                        SetJustSynced(resourceKey);

                        items = await _changesRepository.GetEntriesForPupilAsync(account.Id, account.Pupil.Id,
                            monthAndYear);

                        observer.OnNext(items);
                    }

                    observer.OnCompleted();
                });
            });
        }

        private async Task<TimetableChangeEntry[]> FetchEntriesForMonthAndYear(Account account, DateTime monthAndYear)
        {
            var from = new DateTime(monthAndYear.Year, monthAndYear.Month, 1);
            var to = new DateTime(monthAndYear.Year, monthAndYear.Month, DateTime.DaysInMonth(from.Year, from.Month));

            var query = new GetScheduleChangesEntriesByPupilQuery(account.Pupil.Id, from, to, DateTime.MinValue);

            var client = _apiClientFactory.GetForApiInstanceUrl(account.Unit.RestUrl);

            var response = await client.GetAsync(GetScheduleChangesEntriesByPupilQuery.ApiEndpoint, query);

            var entries = response.Envelope.Select(_mapper.Map<TimetableChangeEntry>).ToArray();

            foreach (var entry in entries)
            {
                entry.AccountId = account.Id;
                entry.PupilId = account.Pupil.Id;
            }

            return entries;
        }

        private static string GetTimetableResourceKey(Account account, DateTime monthAndYear)
            => $"TimetableChanges_{account.Id}_{account.Pupil.Id}_{monthAndYear.Month}_{monthAndYear.Year}";

        protected override TimeSpan OfflineDataLifespan => TimeSpan.FromHours(1);
    }
}