using System;
using System.Collections.Generic;

namespace Vulcanova.Features.Homework
{
    public interface IHomeworkService
    {
        IObservable<IEnumerable<Homework>> GetHomework(int accountId,  int periodId, bool forceSync = false);
    }
}