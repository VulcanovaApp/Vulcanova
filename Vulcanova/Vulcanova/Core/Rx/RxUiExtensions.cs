using System;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using Xamarin.Forms;

namespace Vulcanova.Core.Rx;

public static class RxUiExtensions
{
    public static void SubscribeAndIgnoreErrors<T>(this IObservable<T> observable)
        => observable.Subscribe(_ => { }, _ => { });

    /// <summary>
    /// Utility for implementing force refresh by pull-to-refresh with RefreshView
    /// </summary>
    /// <param name="view">The containing view</param>
    /// <param name="refreshView">The RefreshView to apply bindings to</param>
    /// <param name="commandSelector">Force refresh command property selector</param>
    /// <param name="dontSetIsRefreshingToTrue">
    /// This workarounds the XF issue where the Header of a CollectionView
    /// surrounded with a RefreshView would end up cropped when setting <c>IsRefreshing</c>
    /// to <c>true</c>
    /// </param>
    /// <typeparam name="TView">The containing view type</typeparam>
    /// <typeparam name="TDontCare">The type of force refresh command result</typeparam>
    /// <returns><see cref="IDisposable"/> object used to unsubscribe from the observable sequences.</returns>
    public static IDisposable BindForceRefresh<TView, TDontCare>(this TView view,
        RefreshView refreshView,
        Expression<Func<TView, ReactiveCommand<bool, TDontCare>>> commandSelector,
        bool dontSetIsRefreshingToTrue = false)
        where TView : class, IViewFor
    {
        var disposable = new CompositeDisposable();

        var selectorFunc = commandSelector.Compile();

        // BindCommand doesn't really support passing a parameter in such way and we need to pass "true"
        refreshView.Events().Refreshing.Subscribe(_ =>
            {
                selectorFunc(view).Execute(true).Subscribe();
            })
            .DisposeWith(disposable);

        view.WhenAnyValue(commandSelector)
            .WhereNotNull()
            .Subscribe(vm => vm.IsExecuting
                .Subscribe(value =>
                {
                    if (!dontSetIsRefreshingToTrue || !value)
                    {
                        refreshView.IsRefreshing = value;
                    }
                })
                .DisposeWith(disposable))
            .DisposeWith(disposable);

        return disposable;
    }
}