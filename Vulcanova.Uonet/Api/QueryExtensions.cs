using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vulcanova.Uonet.Api
{
    internal static class QueryExtensions
    {
        internal static string ToQueryString(this IApiQuery apiQuery)
        {
            string KeyValuePairToQueryFragment(KeyValuePair<string, string> pair)
            {
                var (key, value) = pair;
                var propertyName = HttpUtility.UrlEncode(key);
                var propertyValue = HttpUtility.UrlEncode(value);

                return $"{propertyName}={propertyValue}";
            }

            var publicProperties = apiQuery.GetPropertyKeyValuePairs();

            var pairs = publicProperties
                .Select(KeyValuePairToQueryFragment)
                .ToArray();

            if (!pairs.Any())
            {
                return string.Empty;
            }

            return "?" + string.Join("&", pairs);
        }
    }
}