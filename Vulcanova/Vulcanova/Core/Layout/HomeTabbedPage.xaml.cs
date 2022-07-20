using Vulcanova.Features.Homework;
using Vulcanova.Features.LuckyNumber;
using Vulcanova.Features.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Core.Layout;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class HomeTabbedPage
{
    public HomeTabbedPage()
    {
        InitializeComponent();

        // These pages are expected to be placed in "More" tab.
        // On iOS this breaks Prism's NavigationService if a page is wrapped in a NavigationPage
        Page[] pages = {new HomeworkView(), new LuckyNumberView(), new SettingsView()};

        foreach (var page in pages)
        {
            var toBeAdded = page;
                
            if (Device.RuntimePlatform != Device.iOS)
            {
                var navigationPage = new NavigationPage(toBeAdded)
                {
                    Title = toBeAdded.Title,
                    IconImageSource = toBeAdded.IconImageSource
                };

                toBeAdded = navigationPage;
            }

            Children.Add(toBeAdded);
        }
    }
}