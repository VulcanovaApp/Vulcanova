using Vulcanova.Features.Settings;

namespace Vulcanova.Features.Grades
{
    public class GradeModifier
    {
        public GradeModifierKind Kind { get; }

        private readonly ModifiersSettings _modifiers;

        public GradeModifier(GradeModifierKind kind, ModifiersSettings modifiers)
        {
            _modifiers = modifiers;
            Kind = kind;
        }

        public static GradeModifier FromString(string modifierString, ModifiersSettings modifiers)
        {
            var kind = modifierString switch
            {
                "+" => GradeModifierKind.Plus,
                "-" => GradeModifierKind.Minus,
                _ => GradeModifierKind.Unknown
            };

            return new GradeModifier(kind, modifiers);
        }

        public decimal ApplyTo(decimal gradeValue)
        {
            return gradeValue + GetValue();
        }

        private decimal GetValue()
        {
            var value = Kind switch
            {
                GradeModifierKind.Plus => _modifiers.PlusSettings.SelectedValue,
                GradeModifierKind.Minus => _modifiers.MinusSettings.SelectedValue,
                _ => 0
            };

            return value;
        }
    }

    public enum GradeModifierKind
    {
        Plus,
        Minus,
        Unknown
    }
}