using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Web;

namespace Vulcanova.Uonet.Api
{
    internal static class QueryExtensions
    {
        internal static string ToQueryString(this IApiQuery apiQuery)
        {
            string PropertyToKeyValuePair(PropertyInfo property)
            {
                var camelCasePolicy = JsonNamingPolicy.CamelCase;

                var propertyName = HttpUtility.UrlEncode(camelCasePolicy.ConvertName(property.Name));
                var propertyValue = HttpUtility.UrlEncode(property.GetValue(apiQuery).ToString());

                return $"{propertyName}={propertyValue}";
            }
            
            var publicProperties = apiQuery.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var pairs = publicProperties
                .Select(PropertyToKeyValuePair)
                .ToArray();

            if (!pairs.Any())
            {
                return string.Empty;
            }

            return "?" + string.Join("&", pairs);
        }
    }
}