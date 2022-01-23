using System;

namespace Vulcanova.Core.Rx
{
    public static class RxUiExtensions
    {
        public static void SubscribeAndIgnoreErrors<T>(this IObservable<T> observable)
            => observable.Subscribe(_ => { }, _ => { });
    }
}