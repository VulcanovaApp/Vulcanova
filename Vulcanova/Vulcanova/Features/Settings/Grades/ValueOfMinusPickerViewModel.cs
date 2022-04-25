using Prism.Navigation;
using Vulcanova.Core.Mvvm;

namespace Vulcanova.Features.Settings.Grades
{
    public class ValueOfMinusPickerViewModel : ViewModelBase
    {
        public decimal SelectedValue
        {
            get => _appSettings.Modifiers.MinusSettings.SelectedValue;
            set => _appSettings.Modifiers.MinusSettings.SelectedValue = value;
        }

        public bool UsesCustomValue
        {
            get => _appSettings.Modifiers.MinusSettings.UsesCustomValue;
            set => _appSettings.Modifiers.MinusSettings.UsesCustomValue = value;
        }

        private readonly AppSettings _appSettings;

        public ValueOfMinusPickerViewModel(INavigationService navigationService, AppSettings appSettings) : base(navigationService)
        {
            _appSettings = appSettings;
        }
    }
}