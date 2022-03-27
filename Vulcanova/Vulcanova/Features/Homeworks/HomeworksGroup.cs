using System;
using System.Collections.Generic;

namespace Vulcanova.Features.Homeworks
{
    public class HomeworksGroup : List<HomeworkEntry>
    {
        public DateTime Date { get; }

        public HomeworksGroup(DateTime date, IEnumerable<HomeworkEntry> animals) : base(animals)
        {
            Date = date;
        }
    }
}