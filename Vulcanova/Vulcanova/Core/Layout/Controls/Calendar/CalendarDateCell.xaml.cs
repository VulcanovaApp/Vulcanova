using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Core.Layout.Controls.Calendar;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CalendarDateCell
{
    public static readonly BindableProperty SelectedProperty =
        BindableProperty.Create(nameof(Selected), typeof(bool), typeof(CalendarDateCell), false,
            propertyChanged: SelectedPropertyChanged);

    public bool Selected
    {
        get => (bool) GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }

    public static readonly BindableProperty DayProperty =
        BindableProperty.Create(nameof(Day), typeof(int), typeof(CalendarDateCell), 1);

    public int Day
    {
        get => (int) GetValue(DayProperty);
        set => SetValue(DayProperty, value);
    }

    public static readonly BindableProperty TapCommandProperty =
        BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(CalendarDateCell));

    public ICommand TapCommand
    {
        get => (ICommand) GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }

    public static readonly BindableProperty SelectedColorProperty =
        BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(CalendarDateCell), Color.Red,
            propertyChanged: SelectedColorPropertyChanged);

    public Color SelectedColor
    {
        get => (Color) GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }
        
    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CalendarDateCell), Color.Default,
            propertyChanged: TextColorChanged);

    public Color TextColor
    {
        get => (Color) GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public static readonly BindableProperty SelectedTextColorProperty =
        BindableProperty.Create(nameof(SelectedTextColor), typeof(Color), typeof(CalendarDateCell), Color.White,
            propertyChanged: SelectedTextColorChanged);

    public Color SelectedTextColor
    {
        get => (Color) GetValue(SelectedTextColorProperty);
        set => SetValue(SelectedTextColorProperty, value);
    }

    public static readonly BindableProperty SecondaryProperty =
        BindableProperty.Create(nameof(Secondary), typeof(bool), typeof(CalendarDateCell), false,
            propertyChanged: SecondaryPropertyChanged);

    public bool Secondary
    {
        get => (bool) GetValue(SecondaryProperty);
        set => SetValue(SecondaryProperty, value);
    }

    public static readonly BindableProperty SecondaryTextColorProperty =
        BindableProperty.Create(nameof(SecondaryTextColor), typeof(Color), typeof(CalendarDateCell), Color.DarkGray, propertyChanged: SecondaryTextColorChanged);

    public Color SecondaryTextColor
    {
        get => (Color) GetValue(SecondaryTextColorProperty);
        set => SetValue(SecondaryTextColorProperty, value);
    }

    public CalendarDateCell()
    {
        InitializeComponent();
    }

    private static void SelectedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var cell = (CalendarDateCell) bindable;
        UpdateCellAppearance(cell);
    }

    private static void SecondaryPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var cell = (CalendarDateCell) bindable;
        UpdateCellAppearance(cell);
    }

    private static void SelectedColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var cell = (CalendarDateCell) bindable;
        UpdateCellAppearance(cell);
    }
        
    private static void TextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        var cell = (CalendarDateCell) bindable;
        UpdateCellAppearance(cell);
    }

    private static void SelectedTextColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var cell = (CalendarDateCell) bindable;
        UpdateCellAppearance(cell);
    }

    private static void SecondaryTextColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var cell = (CalendarDateCell) bindable;
        UpdateCellAppearance(cell);
    }

    private static void UpdateCellAppearance(CalendarDateCell cell)
    {
        cell.Container.BackgroundColor = cell.Selected ? cell.SelectedColor : Color.Transparent;

        if (cell.Selected)
        {
            cell.Label.TextColor = cell.Secondary ? cell.SelectedTextColor.MultiplyAlpha(0.5) : cell.SelectedTextColor;
        }
        else
        {
            cell.Label.TextColor = cell.Secondary ? cell.SecondaryTextColor : cell.TextColor;
        }
    }
}