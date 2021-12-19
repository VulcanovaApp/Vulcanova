using Prism.Ioc;

namespace Vulcanova.Features.Grades
{
    public static class Config
    {
        public static void RegisterGrades(this IContainerRegistry container)
        {
            container.RegisterForNavigation<GradesView>();

            container.RegisterScoped<IGradesRepository, GradesRepository>();
            container.RegisterScoped<IGradesService, GradesService>();
        }
    }
}