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

        public async Task<string> SendRequestAsync(string url, IApiRequest payload)
        {
            url = _apiInstanceUrl + "/" + url;
            
            var signed = await _requestSigner.SignPayload(payload);
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

            var r = await Config.HttpClient.SendAsync(requestMessage);
            var res = await r.Content.ReadAsStringAsync();

            return res;
        }
    }
}