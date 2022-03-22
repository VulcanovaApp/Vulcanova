using System;
using System.Collections.Generic;

namespace Vulcanova.Features.Exams
{
    public class ExamsGroup : List<Exam>
    {
        public DateTime Date { get; }

        public ExamsGroup(DateTime date, IEnumerable<Exam> animals) : base(animals)
        {
            Date = date;
        }
    }
}