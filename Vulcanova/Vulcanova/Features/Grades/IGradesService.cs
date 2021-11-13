using System;
using System.Collections.Generic;

namespace Vulcanova.Features.Grades
{
    public interface IGradesService
    {
        IObservable<IEnumerable<Grade>> GetCurrentPeriodGrades(int accountId);
    }
}