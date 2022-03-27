﻿using System;
using System.Collections.Generic;

namespace Vulcanova.Features.Homeworks
{
    public interface IHomeworksService
    {
        IObservable<IEnumerable<HomeworkEntry>> GetHomeworksByDateRange(int accountId,  int periodId, DateTime from, DateTime to,
            bool forceSync = false);
    }
}