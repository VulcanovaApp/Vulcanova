using Prism.Ioc;
using Vulcanova.Features.Grades.Final;

namespace Vulcanova.Features.Grades
{
    public static class Config
    {
        public static void RegisterGrades(this IContainerRegistry container)
        {
            container.RegisterForNavigation<GradesView>();

            container.RegisterScoped<IGradesRepository, GradesRepository>();
            container.RegisterScoped<IGradesService, GradesService>();

            container.RegisterScoped<IFinalGradesRepository, FinalGradesRepository>();
            container.RegisterScoped<IFinalGradesService, FinalGradesService>();
        }
    }
}