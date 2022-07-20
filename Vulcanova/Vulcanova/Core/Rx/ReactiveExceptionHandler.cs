using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using ReactiveUI;

namespace Vulcanova.Core.Rx;

public class ReactiveExceptionHandler : IObserver<Exception>
{
    public void OnNext(Exception value)
    {
        if (Debugger.IsAttached) Debugger.Break();

        RxApp.MainThreadScheduler.Schedule(() => Interactions.Errors.Handle(value).Subscribe());
    }

    public void OnError(Exception error)
    {
        if (Debugger.IsAttached) Debugger.Break();

        RxApp.MainThreadScheduler.Schedule(() => Interactions.Errors.Handle(error).Subscribe());
    }

    public void OnCompleted()
    {
    }
}