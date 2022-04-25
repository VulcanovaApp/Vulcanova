using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Vulcanova.Features.Settings;

namespace Vulcanova.Features.Grades.Summary
{
    public static class AverageCalculator
    {
        public static decimal? Average(this IEnumerable<Grade> grades, ModifiersSettings modifiers)
        {
            var nonNullGrades = grades
                .Where(g => g.Value != null)
                .SelectMany(g => Enumerable.Repeat(TryGetValueFromContentRaw(g.ContentRaw, modifiers), g.Column.Weight))
                .Where(g => g != null)
                .ToList();

            if (!nonNullGrades.Any())
            {
                return null;
            }

            return nonNullGrades.Sum() / nonNullGrades.Count;
        }

        private static readonly Regex ValueRegex = new("(\\d+)([+|-])?");

        private static decimal? TryGetValueFromContentRaw(string contentRaw, ModifiersSettings modifiers)
        {
            var match = ValueRegex.Match(contentRaw);

            if (!match.Success)
            {
                return null;
            }

            if (!int.TryParse(match.Groups[1].Value, out var numericValue))
            {
                return null;
            }

            if (!match.Groups[2].Success)
            {
                return numericValue;
            }

            var modifier = GradeModifier.FromString(match.Groups[2].Value, modifiers);
            return modifier.ApplyTo(numericValue);
        }
    }
}