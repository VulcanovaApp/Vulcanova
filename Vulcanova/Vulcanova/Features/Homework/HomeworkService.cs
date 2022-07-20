using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Vulcanova.Uonet.Api;
using Vulcanova.Core.Uonet;
using Vulcanova.Features.Auth;
using Vulcanova.Features.Auth.Accounts;
using Vulcanova.Uonet.Api.Homework;

namespace Vulcanova.Features.Homework;

public class HomeworkService : UonetResourceProvider, IHomeworkService
{
    private readonly ApiClientFactory _apiClientFactory;
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;
    private readonly IHomeworkRepository _homeworksRepository;
        
    public HomeworkService(ApiClientFactory apiClientFactory, IMapper mapper, IAccountRepository accountRepository, IHomeworkRepository homeworksRepository)
    {
        _apiClientFactory = apiClientFactory;
        _mapper = mapper;
        _accountRepository = accountRepository;
        _homeworksRepository = homeworksRepository;
    }

    public IObservable<IEnumerable<Homework>> GetHomework(int accountId, int periodId,
        bool forceSync = false)
    {
        return Observable.Create<IEnumerable<Homework>>(async observer =>
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            var resourceKey = GetHomeworkResourceKey(account, periodId);

            var items = await _homeworksRepository.GetHomeworkForPupilAsync(account.Id, account.Pupil.Id);
                
            observer.OnNext(items);

            if (ShouldSync(resourceKey) || forceSync)
            {
                var onlineEntities = await FetchHomework(account, periodId, accountId);

                await _homeworksRepository.UpdateHomeworkEntriesAsync(onlineEntities, account.Id);

                SetJustSynced(resourceKey);

                items = await _homeworksRepository.GetHomeworkForPupilAsync(account.Id, account.Pupil.Id);
                    
                observer.OnNext(items);
            }
                
            observer.OnCompleted();
        });
    }

    private async Task<Homework[]> FetchHomework(Account account, int periodId, int accountId)
    {        
        var query = new GetHomeworkByPupilQuery(account.Pupil.Id, periodId, DateTime.MinValue);

        var client = _apiClientFactory.GetForApiInstanceUrl(account.Unit.RestUrl);

        var response = client.GetAllAsync(GetHomeworkByPupilQuery.ApiEndpoint, query);

        var entries = await response.Select(_mapper.Map<Homework>).ToArrayAsync();

        foreach (var entry in entries)
        {
            entry.AccountId = accountId;
        }

        return entries;
    }

    private static string GetHomeworkResourceKey(Account account, int periodId)
        => $"Homeworks_{account.Id}_{periodId}";

    protected override TimeSpan OfflineDataLifespan => TimeSpan.FromHours(1);
}