using Prism.Ioc;

namespace Vulcanova.Features.Timetable
{
    public static class Config
    {
        public static void RegisterTimetable(this IContainerRegistry container)
        {
            container.RegisterScoped<ITimetableRepository, TimetableRepository>();
            container.RegisterScoped<ITimetableService, TimetableService>();
        }
    }
}