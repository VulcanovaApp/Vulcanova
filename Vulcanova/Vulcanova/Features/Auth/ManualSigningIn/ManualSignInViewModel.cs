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

        private readonly IInstanceUrlProvider _instanceUrlProvider;

        public ManualSignInViewModel(
            INavigationService navigationService,
            IInstanceUrlProvider instanceUrlProvider,
            IAuthenticationService authenticationService) : base(navigationService)
        {
            _authenticationService = authenticationService;
            _instanceUrlProvider = instanceUrlProvider;

            AddDevice = ReactiveCommand.CreateFromTask(_ => AddDeviceAsync(Token, Symbol, Pin));
        }

        private async Task<Unit> AddDeviceAsync(string token, string symbol, string pin)
        {
            var instanceUrl = await _instanceUrlProvider.GetInstanceUrlAsync(token, symbol);
            await _authenticationService.AuthenticateAsync(token, pin, instanceUrl);

            return Unit.Default;
        }
    }
}