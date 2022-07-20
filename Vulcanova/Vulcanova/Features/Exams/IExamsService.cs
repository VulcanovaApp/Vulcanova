using System;
using System.Collections.Generic;

namespace Vulcanova.Features.Exams;

public interface IExamsService
{
    IObservable<IEnumerable<Exam>> GetExamsByDateRange(int accountId, DateTime from, DateTime to,
        bool forceSync = false);
}