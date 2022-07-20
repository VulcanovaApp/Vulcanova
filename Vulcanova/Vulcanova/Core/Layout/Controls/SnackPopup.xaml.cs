using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Core.Layout.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SnackPopup
{
    public string Message { get; }
    public ImageSource IconResource { get; }
    public string ActionText { get; }
    public ICommand ActionCommand { get; }
    public object CommandParameter { get; }

    public SnackPopup(string title, string message, ImageSource iconResource, string actionText, ICommand actionCommand, object commandParameter)
    {
        Title = title;
        Message = message;
        IconResource = iconResource;
        ActionText = actionText;
        ActionCommand = actionCommand;
        CommandParameter = commandParameter;

        InitializeComponent();
    }
}