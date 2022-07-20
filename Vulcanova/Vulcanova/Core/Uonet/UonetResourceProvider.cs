using System;
using Xamarin.Essentials;

namespace Vulcanova.Core.Uonet;

public abstract class UonetResourceProvider
{
    protected abstract TimeSpan OfflineDataLifespan { get; }

    protected bool ShouldSync(string resourceKey)
    {
        var lastSync = Preferences.Get($"LastSync_{resourceKey}", DateTime.MinValue);
        return DateTime.UtcNow - lastSync > OfflineDataLifespan;
    }

    protected static DateTime GetLastSync(string resourceKey)
        => Preferences.Get($"LastSync_{resourceKey}", DateTime.MinValue);

    protected static void SetJustSynced(string resourceKey)
    {
        Preferences.Set($"LastSync_{resourceKey}", DateTime.UtcNow);
    }
}