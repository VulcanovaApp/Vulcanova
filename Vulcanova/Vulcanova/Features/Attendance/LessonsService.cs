using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Vulcanova.Core.Uonet;
using Vulcanova.Features.Auth;
using Vulcanova.Features.Auth.Accounts;
using Vulcanova.Uonet.Api.Lessons;

namespace Vulcanova.Features.Attendance;

public class LessonsService : UonetResourceProvider, ILessonsService
{
    private readonly ILessonsRepository _changesRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IApiClientFactory _apiClientFactory;
    private readonly IMapper _mapper;

    public LessonsService(ILessonsRepository changesRepository,
        IAccountRepository accountRepository,
        IApiClientFactory apiClientFactory, IMapper mapper)
    {
        _changesRepository = changesRepository;
        _accountRepository = accountRepository;
        _apiClientFactory = apiClientFactory;
        _mapper = mapper;
    }

    public IObservable<IEnumerable<Lesson>> GetLessonsByMonth(int accountId, DateTime monthAndYear, bool forceSync = false)
    {
        return Observable.Create<IEnumerable<Lesson>>(async observer =>
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            var resourceKey = GetTimetableResourceKey(account, monthAndYear);

            var items = await _changesRepository.GetLessonsForAccountAsync(account.Id, monthAndYear);

            observer.OnNext(items);

            if (ShouldSync(resourceKey) || forceSync)
            {
                var onlineEntries = await FetchEntriesForMonthAndYear(account, monthAndYear);

                await _changesRepository.UpsertLessonsForAccountAsync(onlineEntries, account.Id, monthAndYear);

                SetJustSynced(resourceKey);

                items = await _changesRepository.GetLessonsForAccountAsync(account.Id, monthAndYear);

                observer.OnNext(items);
            }

            observer.OnCompleted();
        });
    }

    private async Task<Lesson[]> FetchEntriesForMonthAndYear(Account account, DateTime monthAndYear)
    {
        var from = new DateTime(monthAndYear.Year, monthAndYear.Month, 1);
        var to = new DateTime(monthAndYear.Year, monthAndYear.Month, DateTime.DaysInMonth(from.Year, from.Month));

        var query = new GetLessonsByPupilQuery(account.Pupil.Id, from, to, DateTime.MinValue);

        var client = _apiClientFactory.GetForApiInstanceUrl(account.Unit.RestUrl);

        var response = await client.GetAsync(GetLessonsByPupilQuery.ApiEndpoint, query);

        var lessons = response.Envelope.Select(_mapper.Map<Lesson>).ToArray();

        foreach (var lesson in lessons)
        {
            lesson.AccountId = account.Id;
        }

        return lessons;
    }

    private static string GetTimetableResourceKey(Account account, DateTime monthAndYear)
        => $"Lessons_{account.Id}_{monthAndYear.Month}_{monthAndYear.Year}";

    protected override TimeSpan OfflineDataLifespan => TimeSpan.FromHours(1);
}