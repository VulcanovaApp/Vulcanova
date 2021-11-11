using System;
using System.Threading.Tasks;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Mvvm;
using Vulcanova.Features.Shared;

namespace Vulcanova.Features.LuckyNumber
{
    public class LuckyNumberViewModel : ViewModelBase
    {
        public ReactiveCommand<int, LuckyNumber> GetLuckyNumber { get; }

        [ObservableAsProperty]
        public LuckyNumber LuckyNumber { get; }

        private readonly ILuckyNumberService _luckyNumberService;

        public LuckyNumberViewModel(
            INavigationService navigationService,
            AccountContext accountContext,
            ILuckyNumberService luckyNumberService) : base(navigationService)
        {
            _luckyNumberService = luckyNumberService;

            GetLuckyNumber = ReactiveCommand.CreateFromTask((int accountId) => GetLuckyNumberAsync(accountId));
            GetLuckyNumber.ToPropertyEx(this, vm => vm.LuckyNumber);

            accountContext.WhenAnyValue(ctx => ctx.AccountId)
                .InvokeCommand(GetLuckyNumber);
        }

        private async Task<LuckyNumber> GetLuckyNumberAsync(int accountId)
        {
            return await _luckyNumberService.GetLuckyNumberAsync(
                accountId,
                DateTime.Now);
        }
    }
}