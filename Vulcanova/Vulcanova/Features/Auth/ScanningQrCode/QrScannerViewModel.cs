using System.Reactive;
using System.Threading.Tasks;
using Prism.Navigation;
using ReactiveUI;
using Vulcanova.Core.Mvvm;
using Vulcanova.Uonet.Api.Common;

namespace Vulcanova.Features.Auth.ScanningQrCode;

public class QrScannerViewModel : ViewModelBase
{
    public ReactiveCommand<string, Unit> ProcessQrCodeCommand { get; }
        
    private readonly IInstanceUrlProvider _instanceUrlProvider;

    public QrScannerViewModel(
        INavigationService navigationService,
        IInstanceUrlProvider instanceUrlProvider) : base(navigationService)
    {
        _instanceUrlProvider = instanceUrlProvider;

        ProcessQrCodeCommand = ReactiveCommand.CreateFromTask<string, Unit>(ProcessQrCode);
    }
        
    private async Task<Unit> ProcessQrCode(string code)
    {
        var (apiAddress, token) = AuthQrCode.FromQrString(code);
        var instanceUrl = _instanceUrlProvider.ExtractInstanceUrlFromRequestUrl(apiAddress);

        var navigationParams = new NavigationParameters
        {
            {"instanceUrl", instanceUrl},
            {"token", token}
        };

        await NavigationService.NavigateAsync(nameof(EnterPinCodeView), navigationParams);
            
        return Unit.Default;
    }
}