using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Vulcanova.Features.Shared;

public class AccountContext : ReactiveObject
{
    [Reactive] public int AccountId { get; set; }
}