using System.Collections.Generic;

namespace Vulcanova.Uonet.Signing
{
    public interface IRequestSigner
    {
        Dictionary<string, string> CreateSignedHeaders(string body, string fullUrl);
        SignedApiPayload SignPayload(object o);
    }
}