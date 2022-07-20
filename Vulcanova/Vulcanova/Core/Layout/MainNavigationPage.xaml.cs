using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using FFImageLoading.Svg.Forms;
using Rg.Plugins.Popup.Contracts;
using Vulcanova.Core.Layout.Controls;
using Vulcanova.Core.Rx;
using Vulcanova.Resources;
using Vulcanova.Uonet.Api;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Core.Layout;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MainNavigationPage
{
    public MainNavigationPage(IPopupNavigation navigationService)
    {
        InitializeComponent();

        Interactions.Errors.RegisterHandler(async ctx =>
        {
            var (title, message, icon, action, command, parameter) = ctx.Input switch
            {
                VulcanException {StatusCode: 108} => (
                    Strings.SessionErrorPopupTitle,
                    Strings.SessionErrorAlertMessage,
                    SvgImageSource.FromResource("Vulcanova.Resources.Icons.alert-circle-outline.svg"),
                    Strings.SessionErrorAlertAction,
                    ViewModel.ShowSignInAlert,
                    Unit.Default),

                _ => (
                    Strings.ErrorAlertTitle,
                    ctx.Input.Message,
                    SvgImageSource.FromResource("Vulcanova.Resources.Icons.close-circle.svg"),
                    Strings.ShowErrorDetails,
                    (ICommand) ViewModel.ShowErrorDetails,
                    (object) ctx.Input)
            };

            var popup = new SnackPopup(title, message, icon, action, command, parameter);

            await navigationService.PushAsync(popup);

            await Task.Delay(5000);

            await navigationService.RemovePageAsync(popup);
        });
    }
}