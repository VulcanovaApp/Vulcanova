using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Timetable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimetableListEntryView
    {
        public static readonly BindableProperty EntryProperty =
            BindableProperty.Create(nameof(Entry), typeof(TimetableListEntry), typeof(TimetableListEntryView));

        public TimetableListEntry Entry
        {
            get => (TimetableListEntry) GetValue(EntryProperty);
            set => SetValue(EntryProperty, value);
        }

        public TimetableListEntryView()
        {
            InitializeComponent();
        }
    }
}