using Prism.Ioc;

namespace Vulcanova.Features.Attendance;

public static class Config
{
    public static void RegisterAttendance(this IContainerRegistry container)
    {
        container.RegisterScoped<ILessonsRepository, LessonsRepository>();
        container.RegisterScoped<ILessonsService, LessonsService>();
    }
}