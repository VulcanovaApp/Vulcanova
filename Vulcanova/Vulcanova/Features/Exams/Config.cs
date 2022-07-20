using Prism.Ioc;

namespace Vulcanova.Features.Exams;

public static class Config
{
    public static void RegisterExams(this IContainerRegistry container)
    {
        container.RegisterScoped<IExamsRepository, ExamsRepository>();
        container.RegisterScoped<IExamsService, ExamsService>();
    }
}