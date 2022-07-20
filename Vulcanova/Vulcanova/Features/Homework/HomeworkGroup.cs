using System;
using System.Collections.Generic;

namespace Vulcanova.Features.Homework;

public class HomeworkGroup : List<Homework>
{
    public DateTime Date { get; }

    public HomeworkGroup(DateTime date, IEnumerable<Homework> homework) : base(homework)
    {
        Date = date;
    }
}