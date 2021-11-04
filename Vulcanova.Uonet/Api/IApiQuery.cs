using System;
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
            KeyValuePair<string, string> PropertyToKeyValuePair(PropertyInfo propertyInfo)
            {
                var camelCasePolicy = JsonNamingPolicy.CamelCase;

                var name = camelCasePolicy.ConvertName(propertyInfo.Name);
                var rawValue = propertyInfo.GetValue(this);

                string value;

                if (propertyInfo.PropertyType == typeof(DateTime))
                {
                    value = ((DateTime) rawValue).ToString("yyyy-MM-dd");
                }
                else
                {
                    value = rawValue.ToString();
                }

                return new KeyValuePair<string, string>(name, value);
            }

            var publicProperties = GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            return publicProperties
                .Select(PropertyToKeyValuePair)
                .ToArray();
        }
    }
}