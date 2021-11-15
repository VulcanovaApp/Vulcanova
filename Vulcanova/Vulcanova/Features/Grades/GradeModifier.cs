using Xamarin.Essentials;

namespace Vulcanova.Features.Grades
{
    public class GradeModifier
    {
        public GradeModifierKind Kind { get; }

        public GradeModifier(GradeModifierKind kind)
        {
            Kind = kind;
        }

        public static GradeModifier FromString(string modifierString)
        {
            var kind = modifierString switch
            {
                "+" => GradeModifierKind.Plus,
                "-" => GradeModifierKind.Minus,
                _ => GradeModifierKind.Unknown
            };

            return new GradeModifier(kind);
        }

        public decimal ApplyTo(decimal gradeValue)
        {
            return gradeValue + GetValue();
        }

        private decimal GetValue()
        {
            var floatValue = Kind switch
            {
                GradeModifierKind.Plus => Preferences.Get("Options_ValueOfPlus", 0.5f),
                GradeModifierKind.Minus => Preferences.Get("Options_ValueOfMinus", -0.25f),
                _ => 0
            };

            return (decimal)floatValue;
        }
    }

    public enum GradeModifierKind
    {
        Plus,
        Minus,
        Unknown
    }
}