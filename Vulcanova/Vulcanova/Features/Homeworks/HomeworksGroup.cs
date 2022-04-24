using System;
using System.Collections.Generic;

namespace Vulcanova.Features.Homeworks
{
    public class HomeworksGroup : List<Homework>
    {
        public DateTimeOffset Date { get; }

        public HomeworksGroup(DateTimeOffset date, IEnumerable<Homework> animals) : base(animals)
        {
            Date = date;
        }
    }
}