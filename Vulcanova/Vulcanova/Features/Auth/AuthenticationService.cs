using System;
using System.Threading.Tasks;
using Vulcanova.Uonet;
using Vulcanova.Uonet.Auth;
using Vulcanova.Uonet.Crypto;
using Vulcanova.Uonet.Firebase;
using Vulcanova.Uonet.Signing;

namespace Vulcanova.Features.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<bool> AuthenticateAsync(string token, string symbol, string pin)
        {
            var (fingerprint, privateKey, cert) = KeyPairGenerator.GenerateKeyPair();
            
            var firebaseToken = await FirebaseTokenFetcher.FetchFirebaseToken();

            var certData = string.Join("", cert.Split('\n')[1..^2]);

            var request = new RegisterClientRequest
            {
                OS = Constants.AppOs,
                DeviceModel = Constants.DeviceModel,
                Certificate = certData,
                CertificateType = "X509",
                CertificateThumbprint = fingerprint,
                PIN = pin,
                SecurityToken = token,
                SelfIdentifier = Guid.NewGuid().ToString()
            };

            var signer = new RequestSigner(fingerprint, privateKey, firebaseToken);
            var client = new ApiClient(signer, token, symbol);

            await client.SendRequest("api/mobile/register/new", request);

            return true;
        }
    }
}