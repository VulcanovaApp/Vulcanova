using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Vulcanova.Features.Auth
{
    public static class ClientIdentityStore
    {
        private const string PemPrivateKeyItemKey = "PemPrivateKey";
        private const string FirebaseTokenItemKey = "FirebaseToken";

        public static async Task SaveIdentityAsync(ClientIdentity identity)
        {
            var (x509Certificate2, pemPrivateKey, firebaseToken) = identity;

            using var x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            x509Store.Open(OpenFlags.ReadWrite);
            
            x509Store.Add(x509Certificate2);

            await SecureStorage.SetAsync(PemPrivateKeyItemKey, pemPrivateKey);
            await SecureStorage.SetAsync(FirebaseTokenItemKey, firebaseToken);
        }

        public static async Task<ClientIdentity> GetIdentityAsync()
        {
            using var x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            x509Store.Open(OpenFlags.ReadOnly);

            if (x509Store.Certificates.Count == 0)
            {
                return null;
            }

            var cert = x509Store.Certificates[0];

            var pemPrivateKey = await SecureStorage.GetAsync(PemPrivateKeyItemKey);
            var firebaseToken = await SecureStorage.GetAsync(FirebaseTokenItemKey);

            if (pemPrivateKey == null || firebaseToken == null)
            {
                return null;
            }

            return new ClientIdentity(cert, pemPrivateKey, firebaseToken);
        }
    }

    public record ClientIdentity(X509Certificate2 Certificate, string PemPrivateKey, string FirebaseToken);
}