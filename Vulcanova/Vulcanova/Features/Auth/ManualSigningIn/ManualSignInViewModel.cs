using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Sextant;
using Splat;
using Vulcanova.Core.Mvvm;

namespace Vulcanova.Features.Auth.ManualSigningIn
{
    public class ManualSignInViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> AddDevice { get; }

        [Reactive] public string Token { get; set; }

        [Reactive] public string Symbol { get; set; }

        [Reactive] public string Pin { get; set; }

        private readonly IAuthenticationService _authenticationService;

        public ManualSignInViewModel(IViewStackService viewStackService) : base(viewStackService)
        {
            _authenticationService = Locator.Current.GetService<IAuthenticationService>();

            AddDevice = ReactiveCommand.CreateFromTask(_ => AddDeviceAsync(Token, Symbol, Pin));
        }

        private async Task<Unit> AddDeviceAsync(string token, string symbol, string pin)
        {
            await _authenticationService.AuthenticateAsync(token, symbol, pin);

            return Unit.Default;
        }

        public override string Id => "Dodaj konto";
    }
}