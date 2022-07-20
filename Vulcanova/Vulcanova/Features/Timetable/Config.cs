using Prism.Ioc;
using Vulcanova.Features.Timetable.Changes;

namespace Vulcanova.Features.Timetable;

public static class Config
{
    public static void RegisterTimetable(this IContainerRegistry container)
    {
        container.RegisterScoped<ITimetableRepository, TimetableRepository>();
        container.RegisterScoped<ITimetableService, TimetableService>();

        container.RegisterScoped<ITimetableChangesRepository, TimetableChangesRepository>();
        container.RegisterScoped<ITimetableChangesService, TimetableChangesService>();
    }
}