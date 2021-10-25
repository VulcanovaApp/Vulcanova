using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Vulcanova.Uonet.Signing;

namespace Vulcanova.Uonet.Api
{
    public class ApiClient : IApiClient
    {
        private readonly IRequestSigner _requestSigner;
        private readonly string _apiInstanceUrl;

        public ApiClient(IRequestSigner requestSigner, string apiInstanceUrl)
        {
            _requestSigner = requestSigner;
            _apiInstanceUrl = apiInstanceUrl;
        }

        public async Task<ApiResponse<TResponse>> GetAsync<TResponse>(string url, IApiQuery<TResponse> query)
        {
            url = GetFullRequestUrl(url) + query.ToQueryString();

            var headers = await _requestSigner.CreateSignedHeaders(url);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            
            foreach (var (key, value) in headers)
            {
                requestMessage.Headers.TryAddWithoutValidation(key, value);
            }

            return await SendMessageAsync<TResponse>(requestMessage);
        }

        public async Task<ApiResponse<TResponse>> PostAsync<TResponse>(string url, IApiRequest<TResponse> request)
        {
            url = GetFullRequestUrl(url);
            
            var signed = await _requestSigner.SignPayload(request);
            var json = JsonSerializer.Serialize((object) signed);

            var headers = await _requestSigner.CreateSignedHeaders(json, url);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = signed.ToJsonStringContent()
            };

            foreach (var (key, value) in headers)
            {
                requestMessage.Headers.TryAddWithoutValidation(key, value);
            }

            return await SendMessageAsync<TResponse>(requestMessage);
        }

        private string GetFullRequestUrl(string relativeUrl) => _apiInstanceUrl + "/" + relativeUrl;

        private static async Task<ApiResponse<TResponse>> SendMessageAsync<TResponse>(HttpRequestMessage requestMessage)
        {
            var responseMessage = await Config.HttpClient.SendAsync(requestMessage);
            var res = await responseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ApiResponse<TResponse>>(res);
        }
    }
}