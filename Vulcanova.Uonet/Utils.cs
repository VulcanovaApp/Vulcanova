using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Vulcanova.Uonet
{
    public static class Utils
    {
        public static StringContent ToJsonStringContent(this object o)
        => new StringContent(JsonSerializer.Serialize(o), Encoding.UTF8, "application/json");
    }
}