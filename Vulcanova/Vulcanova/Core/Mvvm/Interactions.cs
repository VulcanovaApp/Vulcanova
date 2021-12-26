using System;
using System.Reactive;
using ReactiveUI;

namespace Vulcanova.Core.Mvvm
{
    public class Interactions
    {
        public static readonly Interaction<Exception, Unit> Errors = new();
    }
}