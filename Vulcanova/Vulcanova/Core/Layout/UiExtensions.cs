using System;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Vulcanova.Core.Layout.Controls;

namespace Vulcanova.Core.Layout;

public static class UiExtensions
{
    public static IDisposable WireUpNonNativeSheet<TViewModel, TDetailsControl, TProp>(TViewModel viewModel,
        TDetailsControl detailsControl,
        SlidingUpPanel panel,
        Expression<Func<TViewModel, TProp>> vmProp,
        Expression<Func<TDetailsControl, TProp>> vProp)
        where TDetailsControl : class
    {
        var disposable = new CompositeDisposable();

        viewModel.WhenAnyValue(vmProp)
            .Skip(1)
            .Subscribe(sub => { panel.Open = sub != null; })
            .DisposeWith(disposable);

        viewModel.WhenAnyValue(vmProp)
            .BindTo(detailsControl, vProp)
            .DisposeWith(disposable);

        return disposable;
    }
}