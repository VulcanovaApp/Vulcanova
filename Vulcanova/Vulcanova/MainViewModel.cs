using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Splat;

namespace Vulcanova
{
    public class MainViewModel
    {
        public ReactiveCommand<IRoutableViewModel, Unit> NavigateToMenuItem { get; }
        public RoutingState Router { get; }

        public MainViewModel()
        {
            Router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));
            
            NavigateToMenuItem = ReactiveCommand.CreateFromObservable<IRoutableViewModel, Unit>(
                routableVm => Router.NavigateAndReset.Execute(routableVm).Select(_ => Unit.Default));
        }
    }
}