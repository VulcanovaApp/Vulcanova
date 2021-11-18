using System.Collections.Generic;

namespace Vulcanova.Features.Grades.Summary
{
    public class SubjectGrades
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public decimal? Average { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
}