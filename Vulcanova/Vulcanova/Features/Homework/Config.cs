using Prism.Ioc;

namespace Vulcanova.Features.Homework
{
    public static class Config
    {
        public static void RegisterHomework(this IContainerRegistry container)
        {
            container.RegisterScoped<IHomeworkRepository, HomeworkRepository>();
            container.RegisterScoped<IHomeworkService, HomeworkService>();
        }
    }
}