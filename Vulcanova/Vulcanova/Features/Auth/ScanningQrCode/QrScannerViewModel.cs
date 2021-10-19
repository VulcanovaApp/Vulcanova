using System;
using System.Threading.Tasks;
using Prism.Navigation;
using ReactiveUI;
using Vulcanova.Core.Mvvm;

namespace Vulcanova.Features.Auth.ScanningQrCode
{
    public class QrScannerViewModel : ViewModelBase
    {
        public ReactiveCommand<string, string> ProcessQrCodeCommand { get; }

        private readonly IAuthenticationService _authenticationService;

        public QrScannerViewModel(
            INavigationService navigationService,
            IAuthenticationService authenticationService) : base(navigationService)
        {
            _authenticationService = authenticationService;

            ProcessQrCodeCommand = ReactiveCommand.CreateFromTask<string, string>(ProcessQrCode);
        }
        
        private async Task<string> ProcessQrCode(string code)
        {
            var qrCode = AuthQrCode.FromQrString(code);

            throw new NotImplementedException();
        }
    }
}