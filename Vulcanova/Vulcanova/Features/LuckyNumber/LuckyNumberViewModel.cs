using System;
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
        public ReactiveCommand<Unit, LuckyNumber> GetLuckyNumber { get; }

        [ObservableAsProperty]
        public LuckyNumber LuckyNumber { get; }

        private readonly ILuckyNumberService _luckyNumberService;

        private int _accountId;

        public LuckyNumberViewModel(
            INavigationService navigationService,
            ILuckyNumberService luckyNumberService) : base(navigationService)
        {
            _luckyNumberService = luckyNumberService;

            GetLuckyNumber = ReactiveCommand.CreateFromTask(_ => GetLuckyNumberAsync(_accountId, DateTime.Now));
            GetLuckyNumber.ToPropertyEx(this, vm => vm.LuckyNumber);
        }

        private async Task<LuckyNumber> GetLuckyNumberAsync(int accountId, DateTime date)
        {
            return await _luckyNumberService.GetLuckyNumberAsync(accountId, date);
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