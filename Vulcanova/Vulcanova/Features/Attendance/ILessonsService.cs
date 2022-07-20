using System;
using System.Collections.Generic;

namespace Vulcanova.Features.Attendance;

public interface ILessonsService
{
    IObservable<IEnumerable<Lesson>> GetLessonsByMonth(int accountId, DateTime monthAndYear, bool forceSync = false);
}