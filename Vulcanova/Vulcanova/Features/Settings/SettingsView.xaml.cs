using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace Vulcanova.Features.Settings;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SettingsView
{
    public SettingsView()
    {
        InitializeComponent();

        this.WhenActivated(disposable =>
        {
            ValueOfPlusCell.Events().Tapped
                .Select(_ => Unit.Default)
                .InvokeCommand(ViewModel, vm => vm.OpenValueOfPlusPicker)
                .DisposeWith(disposable);
                
            ValueOfMinusCell.Events().Tapped
                .Select(_ => Unit.Default)
                .InvokeCommand(ViewModel, vm => vm.OpenValueOfMinusPicker)
                .DisposeWith(disposable);

            this.OneWayBind(ViewModel, vm => vm.ValueOfPlus, v => v.ValueOfPlusLabel.Text)
                .DisposeWith(disposable);
                
            this.OneWayBind(ViewModel, vm => vm.ValueOfMinus, v => v.ValueOfMinusLabel.Text)
                .DisposeWith(disposable);
        });
    }
}