using System;
using System.Collections.Generic;

namespace Vulcanova.Features.Timetable
{
    public interface ITimetableService
    {
        IObservable<IEnumerable<TimetableEntry>> GetPeriodEntriesByMonth(int accountId, DateTime monthAndYear,
            bool forceSync = false);
    }
}