using System.IO;
using DryIoc;
using Microsoft.EntityFrameworkCore;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Essentials;

namespace Vulcanova.Core.Data
{
    public static class Config
    {
        public static void RegisterDbContext(this IContainerRegistry containerRegistry)
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "data.db3");

            containerRegistry.RegisterScoped<AppDbContext>(() => new AppDbContext(dbPath));

            containerRegistry.GetContainer().Resolve<AppDbContext>().Database.Migrate();
        }
    }
}