using ReactiveUI;
using Sextant;

namespace Vulcanova.Core.Mvvm
{
    public abstract class ViewModelBase : ReactiveObject, IViewModel
    {
        protected readonly IViewStackService ViewStackService;

        protected ViewModelBase(IViewStackService viewStackService) => ViewStackService = viewStackService;

        public abstract string Id { get; }
    }
}