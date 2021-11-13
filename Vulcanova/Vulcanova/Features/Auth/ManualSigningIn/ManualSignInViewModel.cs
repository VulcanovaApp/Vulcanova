using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vulcanova.Core.Mvvm;
using Vulcanova.Uonet.Api.Common;

namespace Vulcanova.Features.Auth.ManualSigningIn
{
    public class ManualSignInViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> AddDevice { get; }

        [Reactive] public string Token { get; set; }

        [Reactive] public string Symbol { get; set; }

        [Reactive] public string Pin { get; set; }

        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountRepository _accountRepository;

        private readonly IInstanceUrlProvider _instanceUrlProvider;

        public ManualSignInViewModel(
            INavigationService navigationService,
            IInstanceUrlProvider instanceUrlProvider,
            IAuthenticationService authenticationService,
            IAccountRepository accountRepository) : base(navigationService)
        {
            _authenticationService = authenticationService;
            _accountRepository = accountRepository;
            _instanceUrlProvider = instanceUrlProvider;

            AddDevice = ReactiveCommand.CreateFromTask(_ => AddDeviceAsync(Token, Symbol, Pin));
        }

        private async Task<Unit> AddDeviceAsync(string token, string symbol, string pin)
        {
            var instanceUrl = await _instanceUrlProvider.GetInstanceUrlAsync(token, symbol);
            var accounts = await _authenticationService.AuthenticateAsync(token, pin, instanceUrl);

            // TODO: Ask the user which acc to make active
            accounts.First().IsActive = true;

            _accountRepository.AddAccounts(accounts);

            return Unit.Default;
        }
    }
}