using Vulcanova.Core.Rx;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Core.Layout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainNavigationPage
    {
        public MainNavigationPage()
        {
            InitializeComponent();

            Interactions.Errors.RegisterHandler(async ctx =>
            {
                await DisplayAlert(
                    "Error",
                    ctx.Input.ToString(), "OK");
            });
        }
    }
}