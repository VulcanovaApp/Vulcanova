using System;
using System.Collections.Generic;

namespace Vulcanova.Features.Homework
{
    public class HomeworkGroup : List<Homework>
    {
        public DateTimeOffset Date { get; }

        public HomeworkGroup(DateTimeOffset date, IEnumerable<Homework> animals) : base(animals)
        {
            Date = date;
        }
    }
}