using System;
using System.Collections.Generic;

namespace Vulcanova.Features.Grades.Final
{
    public interface IFinalGradesService
    {
        IObservable<IEnumerable<FinalGradesEntry>> GetPeriodGrades(int accountId, int periodId, bool forceSync = false);
    }
}