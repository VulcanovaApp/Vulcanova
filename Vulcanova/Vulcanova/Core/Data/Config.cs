using System.IO;
using LiteDB.Async;
using Prism.Ioc;
using Xamarin.Essentials;

namespace Vulcanova.Core.Data
{
    public static class Config
    {
        public static void RegisterLiteDb(this IContainerRegistry containerRegistry)
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "data.db");
            var db = new LiteDatabaseAsync(dbPath);

            containerRegistry.RegisterInstance(typeof(LiteDatabaseAsync), db);
        }
    }
}