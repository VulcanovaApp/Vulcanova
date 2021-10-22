using Vulcanova.Uonet.Api;

namespace Vulcanova.Core.Uonet
{
    public interface IApiClientFactory
    {
        IApiClient GetForApiInstanceUrl(string apiInstanceUrl);
    }
}