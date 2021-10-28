using System.Reactive;
using System.Threading.Tasks;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Mvvm;
using Vulcanova.Features.Auth;

namespace Vulcanova.Features.LuckyNumber
{
    public class LuckyNumberViewModel : ViewModelBase, INavigatedAware
    {
        public ReactiveCommand<Unit, int> GetLuckyNumber { get; }

        [ObservableAsProperty]
        public int LuckyNumber { get; }

        private readonly ILuckyNumberService _luckyNumberService;
        private readonly IAccountRepository _accountRepository;

        private int _accountId;

        public LuckyNumberViewModel(
            INavigationService navigationService,
            ILuckyNumberService luckyNumberService,
            IAccountRepository accountRepository) : base(navigationService)
        {
            _luckyNumberService = luckyNumberService;
            _accountRepository = accountRepository;

            GetLuckyNumber = ReactiveCommand.CreateFromTask(_ => GetLuckyNumberAsync(_accountId));
            GetLuckyNumber.ToPropertyEx(this, vm => vm.LuckyNumber);
        }

        private async Task<int> GetLuckyNumberAsync(int accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            return await _luckyNumberService.GetLuckyNumberAsync(account.ConstituentUnit.Id, account.Unit.RestUrl);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _accountId = parameters.GetValue<int>("accountId");
        }
    }
}