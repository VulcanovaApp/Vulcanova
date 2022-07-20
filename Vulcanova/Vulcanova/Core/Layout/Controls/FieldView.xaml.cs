using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Core.Layout.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FieldView
{
    public static readonly BindableProperty NameProperty =
        BindableProperty.Create(nameof(Name), typeof(string), typeof(FieldView));

    public string Name
    {
        get => (string) GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }
        
    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(string), typeof(FieldView));

    public string Value
    {
        get => (string) GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public FieldView()
    {
        InitializeComponent();
    }
}