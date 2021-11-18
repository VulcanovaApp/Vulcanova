using System.Collections.Generic;
using Prism.Navigation;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Grades.SubjectDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradesSubjectDetailsView : IInitialize
    {
        public GradesSubjectDetailsView()
        {
            InitializeComponent();
        }

        public void Initialize(INavigationParameters parameters)
        {
            Title = parameters.GetValue<string>("subjectName");
            SubjectGrades.ItemsSource = parameters.GetValue<IEnumerable<Grade>>("grades");
        }
    }
}