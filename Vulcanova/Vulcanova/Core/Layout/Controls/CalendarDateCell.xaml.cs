using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Core.Layout.Controls
{
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
            var isSelected = (bool) newValue;
            cell.Container.BackgroundColor = isSelected ? cell.SelectedColor : Color.Transparent;
            cell.Label.TextColor = isSelected ? cell.SelectedTextColor : Color.Default;
        }

        private static void SecondaryPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var cell = (CalendarDateCell) bindable;
            var isSecondary = (bool) newValue;
            cell.Label.TextColor = isSecondary ? cell.SecondaryTextColor : Color.Default;
        }

        private static void SelectedColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var cell = (CalendarDateCell) bindable;
            cell.Container.BackgroundColor = cell.Selected ? (Color) newValue : Color.Transparent;
        }

        private static void SelectedTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var cell = (CalendarDateCell) bindable;
            cell.Label.TextColor = cell.Selected ? (Color) newValue : Color.Default;
        }

        private static void SecondaryTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var cell = (CalendarDateCell) bindable;
            cell.Label.TextColor = cell.Secondary ? (Color) newValue : Color.Default;
        }
    }
}