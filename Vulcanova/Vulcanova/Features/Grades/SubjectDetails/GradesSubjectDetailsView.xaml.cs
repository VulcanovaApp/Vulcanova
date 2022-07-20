using Vulcanova.Features.Grades.Summary;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Grades.SubjectDetails;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GradesSubjectDetailsView
{
    public static readonly BindableProperty SubjectProperty = BindableProperty.Create(
        nameof(Subject),
        typeof(SubjectGrades),
        typeof(GradesSubjectDetailsView));

    public SubjectGrades Subject
    {
        get => (SubjectGrades) GetValue(SubjectProperty);
        set => SetValue(SubjectProperty, value);
    }

    public GradesSubjectDetailsView()
    {
        InitializeComponent();
    }
}