using System.Collections.Generic;
using System.Threading.Tasks;
using Vulcanova.Uonet.Api;
using Vulcanova.Uonet.Signing;

namespace Vulcanova.Features.Auth
{
    public class RequestSignerAdapter : IRequestSigner
    {
        private RequestSigner _requestSigner;

        public async ValueTask<Dictionary<string, string>> CreateSignedHeaders(string body, string fullUrl)
        {
            var signer = await GetOrCreateBaseRequestSigner();

            return await signer.CreateSignedHeaders(body, fullUrl);
        }

        public async ValueTask<SignedApiPayload> SignPayload(object o)
        {
            var signer = await GetOrCreateBaseRequestSigner();

            return await signer.SignPayload(o);
        }

        private async ValueTask<RequestSigner> GetOrCreateBaseRequestSigner()
        {
            if (_requestSigner != null) return _requestSigner;

            var (certificate, privateKey, firebaseToken) =
                await ClientIdentityProvider.GetOrCreateClientIdentityAsync();

            _requestSigner = new RequestSigner(certificate.Thumbprint, privateKey, firebaseToken);

            return _requestSigner;
        }
    }
}