using System.Threading.Tasks;

namespace Vulcanova.Uonet.Api
{
    public interface IApiClient
    {
        Task<ApiResponse<TResponse>> GetAsync<TResponse>(string url, IApiQuery<TResponse> query);
        Task<ApiResponse<TResponse>> PostAsync<TResponse>(string url, IApiRequest<TResponse> request);
    }
}