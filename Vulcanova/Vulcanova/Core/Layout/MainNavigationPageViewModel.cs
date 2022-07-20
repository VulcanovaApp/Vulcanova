using System;
using System.Reactive;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using Vulcanova.Features.Auth;
using Vulcanova.Features.Shared;
using Vulcanova.Resources;
using Xamarin.Essentials;

namespace Vulcanova.Core.Layout;

public class MainNavigationPageViewModel : ReactiveObject
{
    public ReactiveCommand<Exception, Unit> ShowErrorDetails { get; }
    public ReactiveCommand<Unit, Unit> ShowSignInAlert { get; }


    public MainNavigationPageViewModel(
        INavigationService navigationService,
        IPageDialogService dialogService,
        IAccountRepository accountRepository,
        AccountContext accountContext)
    {
        ShowErrorDetails = ReactiveCommand.CreateFromTask(async (Exception ex) =>
        {
            var result = await dialogService.DisplayAlertAsync(Strings.ErrorAlertTitle, ex.ToString(),
                Strings.ReportErrorButton, Strings.IgnoreErrorButton);

            if (result)
            {
                await Browser.OpenAsync("https://github.com/VulcanovaApp/Vulcanova/issues",
                    BrowserLaunchMode.SystemPreferred);
            }

            return Unit.Default;
        });

        ShowSignInAlert = ReactiveCommand.CreateFromTask(async () =>
        {
            await accountRepository.DeleteByIdAsync(accountContext.AccountId);

            await navigationService.NavigateAsync("/MainNavigationPage/IntroView");

            return Unit.Default;
        });
    }
}