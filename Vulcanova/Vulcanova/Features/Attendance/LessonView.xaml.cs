using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Attendance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LessonView
    {
        public static readonly BindableProperty LessonProperty =
            BindableProperty.Create(nameof(Lesson), typeof(Lesson), typeof(LessonView));

        public Lesson Lesson
        {
            get => (Lesson) GetValue(LessonProperty);
            set => SetValue(LessonProperty, value);
        }

        public LessonView()
        {
            InitializeComponent();
        }
    }
}