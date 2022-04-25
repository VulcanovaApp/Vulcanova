using System;
using System.Collections.Generic;

namespace Vulcanova.Features.Homeworks
{
    public interface IHomeworksService
    {
        IObservable<IEnumerable<Homework>> GetHomeworks(int accountId,  int periodId, bool forceSync = false);
    }
}