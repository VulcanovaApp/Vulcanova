using Prism.Ioc;
using Vulcanova.Features.Grades.Summary;

namespace Vulcanova.Features.Grades
{
    public static class Config
    {
        public static void RegisterGrades(this IContainerRegistry container)
        {
            container.RegisterForNavigation<GradesSummaryView, GradesViewModel>();

            container.RegisterScoped<IGradesRepository, GradesRepository>();
            container.RegisterScoped<IGradesService, GradesService>();
        }
    }
}