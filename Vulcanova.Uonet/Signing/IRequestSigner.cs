using System.Collections.Generic;
using System.Threading.Tasks;
using Vulcanova.Uonet.Api;

namespace Vulcanova.Uonet.Signing
{
    public interface IRequestSigner
    {
        ValueTask<Dictionary<string, string>> CreateSignedHeaders(string fullUrl);
        ValueTask<Dictionary<string, string>> CreateSignedHeaders(string body, string fullUrl);
        ValueTask<SignedApiPayload> SignPayload(object o);
    }
}