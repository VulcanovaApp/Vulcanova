using Xamarin.Forms;

namespace Vulcanova.Core.Layout.Controls;

public class OptionCell : ViewCell
{
    public static readonly BindableProperty SelectedProperty =
        BindableProperty.Create(nameof(Selected), typeof(bool), typeof(OptionCell));

    public bool Selected
    {
        get => (bool) GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }
        
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(OptionCell));

    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
        
    public static readonly BindableProperty BackgroundColorProperty =
        BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(OptionCell));

    public Color BackgroundColor
    {
        get => (Color) GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(OptionCell));

    public Color TextColor
    {
        get => (Color) GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }
}