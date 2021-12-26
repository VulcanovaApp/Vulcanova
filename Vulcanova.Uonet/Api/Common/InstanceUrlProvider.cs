using System;
using System.Linq;
using System.Threading.Tasks;

namespace Vulcanova.Uonet.Api.Common
{
    public class InstanceUrlProvider : IInstanceUrlProvider
    {
        private const string RoutingRulesUrl = "https://komponenty.vulcan.net.pl/UonetPlusMobile/RoutingRules.txt";

        public async Task<string> GetInstanceUrlAsync(string token, string symbol)
        {
            var contents = await Config.HttpClient.GetStringAsync(RoutingRulesUrl);

            var baseUrl = contents
                .Split('\n')
                .FirstOrDefault(l => l.StartsWith(token[..3]))?
                .Split(',')[1]
                .TrimEnd();

            if (baseUrl == null)
            {
                return null;
            }

            return baseUrl + "/" + symbol;
        }

        public string ExtractInstanceUrlFromRequestUrl(string apiEndpointUrl)
        {
            var uri = new Uri(apiEndpointUrl);
            return uri.GetLeftPart(UriPartial.Authority) + "/" + uri.PathAndQuery.Split('/')[1];
        }
    }
}