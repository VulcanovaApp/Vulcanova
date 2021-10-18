using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Vulcanova.Uonet.Signing;

namespace Vulcanova.Uonet
{
    public class ApiClient
    {
        private const string RoutingRulesUrl = "https://komponenty.vulcan.net.pl/UonetPlusMobile/RoutingRules.txt";

        private readonly IRequestSigner _requestSigner;
        private readonly string _token;
        private readonly string _symbol;

        private string _baseUrl;

        public ApiClient(IRequestSigner requestSigner, string token, string symbol)
        {
            _requestSigner = requestSigner;
            _token = token;
            _symbol = symbol;
        }

        public async Task<string> SendRequest(string url, IApiRequest payload)
        {
            _baseUrl ??= await GetBaseUrlAsync();

            url = _baseUrl + "/" + _symbol + "/" + url;
            
            var signed = _requestSigner.SignPayload(payload);
            var json = JsonSerializer.Serialize((object)signed);

            var headers = _requestSigner.CreateSignedHeaders(json, url);

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

        private async Task<string> GetBaseUrlAsync()
        {
            var contents = await Config.HttpClient.GetStringAsync(RoutingRulesUrl);

            return contents
                .Split('\n')
                .First(l => l.StartsWith(_token[..3]))
                .Split(',')[1]
                .TrimEnd();
        }
    }
}