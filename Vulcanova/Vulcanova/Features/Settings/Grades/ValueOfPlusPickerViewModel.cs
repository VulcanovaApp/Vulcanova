using Prism.Navigation;
using Vulcanova.Core.Mvvm;

namespace Vulcanova.Features.Settings.Grades
{
    public class ValueOfPlusPickerViewModel : ViewModelBase
    {
        public decimal SelectedValue
        {
            get => _appSettings.Modifiers.PlusSettings.SelectedValue;
            set => _appSettings.Modifiers.PlusSettings.SelectedValue = value;
        }

        public bool UsesCustomValue
        {
            get => _appSettings.Modifiers.PlusSettings.UsesCustomValue;
            set => _appSettings.Modifiers.PlusSettings.UsesCustomValue = value;
        }

        private readonly AppSettings _appSettings;

        public ValueOfPlusPickerViewModel(INavigationService navigationService, AppSettings appSettings) : base(navigationService)
        {
            _appSettings = appSettings;
        }
    }
}