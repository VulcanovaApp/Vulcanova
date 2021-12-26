using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;
using Vulcanova.Features.Auth.Accounts;
using Vulcanova.Features.Shared;

namespace Vulcanova.Features.Auth
{
    public class AccountsManager
    {
        private readonly IAccountRepository _accountRepository;
        private readonly AccountContext _accountContext;
        private readonly INavigationService _navigationService;

        public AccountsManager(IAccountRepository accountRepository, INavigationService navigationService, AccountContext accountContext)
        {
            _accountRepository = accountRepository;
            _navigationService = navigationService;
            _accountContext = accountContext;
        }

        public async Task OpenAccountAndMarkAsCurrentAsync(int accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            account.IsActive = true;

            await _accountRepository.UpdateAccountAsync(account);
            
            _accountContext.AccountId = account.Id;
            
            await _navigationService.NavigateAsync("/NavigationPage/HomeTabbedPage?selectedTab=GradesSummaryView");
        }

        public async Task AddAccountsAsync(IEnumerable<Account> accounts)
        {
            await _accountRepository.AddAccountsAsync(accounts);
        }
    }
}