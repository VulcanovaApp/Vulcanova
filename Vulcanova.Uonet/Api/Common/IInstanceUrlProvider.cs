using System.Threading.Tasks;

namespace Vulcanova.Uonet.Api.Common
{
    public interface IInstanceUrlProvider
    {
        Task<string> GetInstanceUrlAsync(string token, string symbol);
        string ExtractInstanceUrlFromRequestUrl(string apiEndpointUrl);
    }
}