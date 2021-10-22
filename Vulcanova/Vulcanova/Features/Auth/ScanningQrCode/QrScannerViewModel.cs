using System;
using System.Threading.Tasks;
using Prism.Navigation;
using ReactiveUI;
using Vulcanova.Core.Mvvm;
using Vulcanova.Uonet.Api.Common;

namespace Vulcanova.Features.Auth.ScanningQrCode
{
    public class QrScannerViewModel : ViewModelBase
    {
        public ReactiveCommand<string, string> ProcessQrCodeCommand { get; }

        private readonly IAuthenticationService _authenticationService;
        private readonly IInstanceUrlProvider _instanceUrlProvider;

        public QrScannerViewModel(
            INavigationService navigationService,
            IAuthenticationService authenticationService,
            IInstanceUrlProvider instanceUrlProvider) : base(navigationService)
        {
            _authenticationService = authenticationService;
            _instanceUrlProvider = instanceUrlProvider;

            ProcessQrCodeCommand = ReactiveCommand.CreateFromTask<string, string>(ProcessQrCode);
        }
        
        private async Task<string> ProcessQrCode(string code)
        {
            var qrCode = AuthQrCode.FromQrString(code);
            var instanceUrl = _instanceUrlProvider.ExtractInstanceUrlFromRequestUrl(qrCode.ApiAddress);

            throw new NotImplementedException();
        }
    }
}