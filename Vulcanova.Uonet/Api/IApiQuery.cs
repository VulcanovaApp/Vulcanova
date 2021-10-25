using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Vulcanova.Uonet.Api
{
    public interface IApiQuery<TResponse> : IApiQuery
    {
    }

    public interface IApiQuery
    {
        public IEnumerable<KeyValuePair<string, string>> GetPropertyKeyValuePairs()
        {
            var camelCasePolicy = JsonNamingPolicy.CamelCase;

            var publicProperties = GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            return publicProperties
                .Select(p => new KeyValuePair<string, string>(
                    camelCasePolicy.ConvertName(p.Name),
                    p.GetValue(this).ToString())
                )
                .ToArray();
        }
    }
}