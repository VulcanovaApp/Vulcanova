using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Exams
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExamView
    {
        public static readonly BindableProperty ExamProperty =
            BindableProperty.Create(nameof(Exam), typeof(Exam), typeof(ExamView));

        public Exam Exam
        {
            get => (Exam) GetValue(ExamProperty);
            set => SetValue(ExamProperty, value);
        }

        public ExamView()
        {
            InitializeComponent();
        }
    }
}