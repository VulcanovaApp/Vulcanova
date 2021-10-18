using System;
using System.Threading.Tasks;
using ReactiveUI;
using Sextant;
using Splat;
using Vulcanova.Core.Mvvm;

namespace Vulcanova.Features.Auth.ScanningQrCode
{
    public class QrScannerViewModel : ViewModelBase
    {
        public ReactiveCommand<string, string> ProcessQrCodeCommand { get; }

        private readonly IAuthenticationService _authenticationService;

        public QrScannerViewModel(IViewStackService viewStackService) : base(viewStackService)
        {
            _authenticationService = Locator.Current.GetService<IAuthenticationService>();

            ProcessQrCodeCommand = ReactiveCommand.CreateFromTask<string, string>(ProcessQrCode);
        }
        
        private async Task<string> ProcessQrCode(string code)
        {
            var qrCode = AuthQrCode.FromQrString(code);

            throw new NotImplementedException();
        }

        public override string Id => "QrScanner";
    }
}