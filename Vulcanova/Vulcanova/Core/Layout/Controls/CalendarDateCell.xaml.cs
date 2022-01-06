using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Core.Layout.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarDateCell
    {
        public static readonly BindableProperty SelectedProperty =
            BindableProperty.Create(nameof(Selected), typeof(bool), typeof(CalendarDateCell), false, propertyChanged: SelectedPropertyChanged);

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

        public CalendarDateCell()
        {
            InitializeComponent();
        }
        
        private static void SelectedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var cell = (CalendarDateCell) bindable;
            var isSelected = (bool) newValue;
            cell.Container.BackgroundColor = isSelected ? Color.Red : Color.Transparent;
        }
    }
}