using Prism.Ioc;

namespace Vulcanova.Features.Grades
{
    public static class Config
    {
        public static void RegisterGrades(this IContainerRegistry container)
        {
            container.RegisterForNavigation<GradesSummaryView, GradesViewModel>();

            container.RegisterScoped<IGradesService, GradesService>();
        }
    }
}