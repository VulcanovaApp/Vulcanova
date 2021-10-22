using System.Threading.Tasks;

namespace Vulcanova.Uonet.Api
{
    public interface IApiClient
    {
        Task<string> SendRequestAsync(string url, IApiRequest payload);
    }
}