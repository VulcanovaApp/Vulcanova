using Vulcanova.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Attendance.LessonDetails;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LessonTimeView
{
    public static readonly BindableProperty LessonProperty =
        BindableProperty.Create(nameof(Lesson), typeof(Lesson), typeof(LessonDetailsView),
            propertyChanged: LessonChanged);

    private static void LessonChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (newvalue == null)
            return;

        var lesson = (Lesson) newvalue;
        var view = (LessonTimeView) bindable;

        view.ValueLabel.Text = string.Format(Strings.LessonTimeValueLabel, lesson.Start, lesson.End, lesson.No);
    }

    public Lesson Lesson
    {
        get => (Lesson) GetValue(LessonProperty);
        set => SetValue(LessonProperty, value);
    }

    public LessonTimeView()
    {
        InitializeComponent();
    }
}