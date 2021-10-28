using Prism.Ioc;

namespace Vulcanova.Features.LuckyNumber
{
    public static class Config
    {
        public static void RegisterLuckyNumber(this IContainerRegistry container)
        {
            container.RegisterForNavigation<LuckyNumberView, LuckyNumberViewModel>();
            container.RegisterScoped<ILuckyNumberService, LuckyNumberService>();
            container.RegisterScoped<ILuckyNumberRepository, LuckyNumberRepository>();
        }
    }
}