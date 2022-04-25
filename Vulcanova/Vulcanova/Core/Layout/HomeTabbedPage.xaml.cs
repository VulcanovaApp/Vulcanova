using Vulcanova.Features.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Core.Layout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeTabbedPage
    {
        public HomeTabbedPage()
        {
            InitializeComponent();

            Page page = new SettingsView();

            // Settings page is expected to be placed in "More" tab.
            // On iOS this breaks Prism's NavigationService if wrapped in a NavigationPage
            if (Device.RuntimePlatform != Device.iOS)
            {
                var navigationPage = new NavigationPage(page)
                {
                    Title = page.Title,
                    IconImageSource = page.IconImageSource
                };

                page = navigationPage;
            }

            Children.Add(page);
        }
    }
}