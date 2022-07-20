using System.Reflection;
using AutoMapper;
using Prism.Ioc;

namespace Vulcanova.Core.Mapping;

public static class Config
{
    public static void RegisterAutoMapper(this IContainerRegistry containerRegistry)
    {
        var conf = new MapperConfiguration(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));
        var mapper = new Mapper(conf);

        containerRegistry.RegisterInstance<IMapper>(mapper);
    }
}