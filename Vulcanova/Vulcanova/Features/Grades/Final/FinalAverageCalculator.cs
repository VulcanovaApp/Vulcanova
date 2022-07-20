using System.Collections.Generic;
using System.Linq;
using Vulcanova.Features.Grades.Summary;
using Vulcanova.Features.Settings;
using Vulcanova.Features.Shared;

namespace Vulcanova.Features.Grades.Final;

public static class FinalAverageCalculator
{
    public static decimal? Average(this IEnumerable<FinalGradesEntry> entries, ModifiersSettings modifiers)
    {
        bool TryGetValueFromDescriptiveForm(string s, out decimal value) =>
            (value = s switch
            {
                "cel" or "celujący" => 6,
                "bdb" or "bardzo dobry" => 5,
                "db" or "dobry" => 4,
                "dst" or "dostateczny" => 3,
                "dop" or "dopuszczający" => 2,
                "ndst" or "niedostateczny" => 1,
                _ => 0
            }) != 0;

        var calculableEntries = entries
            .Where(e => e.Subject.Id != Subject.BehaviourSubjectId)
            .Select(e => e.FinalGrade)
            .Where(e => !string.IsNullOrEmpty(e))
            .ToArray();

        var values = new List<decimal>(calculableEntries.Length);

        foreach (var entry in calculableEntries)
        {
            if (AverageCalculator.TryGetValueFromContentRaw(entry, modifiers, out var value))
            {
                values.Add(value);
            }
            else if (TryGetValueFromDescriptiveForm(entry, out value))
            {
                values.Add(value);
            }
        }

        if (!values.Any()) return null;

        return values.Sum() / values.Count;
    }
}