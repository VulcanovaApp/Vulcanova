using Prism.Ioc;

namespace Vulcanova.Features.Homeworks
{
    public static class Config
    {
        public static void RegisterHomeworks(this IContainerRegistry container)
        {
            container.RegisterScoped<IHomeworksRepository, HomeworksRepository>();
            container.RegisterScoped<IHomeworksService, HomeworksService>();
        }
    }
}