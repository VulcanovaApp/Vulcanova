using Vulcanova.Core.Mvvm;
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