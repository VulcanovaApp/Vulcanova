using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Vulcanova.Uonet.Crypto;
using Vulcanova.Uonet.Firebase;

namespace Vulcanova.Features.Auth;

public static class ClientIdentityProvider
{
    private static ClientIdentity _identity;

    public static async ValueTask<ClientIdentity> GetOrCreateClientIdentityAsync()
    {
        if (_identity != null)
        {
            return _identity;
        }

        _identity = await ClientIdentityStore.GetIdentityAsync();
            
        if (_identity != null)
        {
            return _identity;
        }

        _identity = await CreateClientIdentityAsync();

        await ClientIdentityStore.SaveIdentityAsync(_identity);

        return _identity;
    }

    private static async Task<ClientIdentity> CreateClientIdentityAsync()
    {
        var (privateKey, cert) = KeyPairGenerator.GenerateKeyPair();
        var firebaseToken = await FirebaseTokenFetcher.FetchFirebaseTokenAsync();

        return new ClientIdentity(new X509Certificate2(cert.GetEncoded()), privateKey, firebaseToken);
    }
}