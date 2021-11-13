using System.IO;
using LiteDB;
using Prism.Ioc;
using Xamarin.Essentials;

namespace Vulcanova.Core.Data
{
    public static class Config
    {
        public static void RegisterLiteDb(this IContainerRegistry containerRegistry)
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "data.db");
            var db = new LiteDatabase(dbPath);

            containerRegistry.RegisterInstance(typeof(LiteDatabase), db);
        }
    }
}