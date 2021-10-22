using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vulcanova.Uonet.Firebase
{
    public static class FirebaseTokenFetcher
    {
        public static async Task<string> FetchFirebaseTokenAsync()
        {
            const string aid = "4609707972546570896:3626695765779152704";
            var device = aid.Split(':')[0];

            const string app = "pl.edu.vulcan.hebe";

            var message = new HttpRequestMessage(HttpMethod.Post, "https://android.clients.google.com/c2dm/register3")
            {
                Headers =
                {
                    {"Authorization", $"AidLogin {aid}"},
                    {"User-Agent", "Android-GCM/1.5"},
                    {"app", app}
                },
                Content = new FormUrlEncodedContent(new []
                {
                    new KeyValuePair<string, string>("sender", "987828170337"),
                    new KeyValuePair<string, string>("X-Scope", "*"),
                    new KeyValuePair<string, string>("X-gmp_app_id", "1:987828170337:android:ac97431a0a4578c3"),
                    new KeyValuePair<string, string>("app", app),
                    new KeyValuePair<string, string>("device", device)
                })
            };

            var result = await Config.HttpClient.SendAsync(message);
            var response = await result.Content.ReadAsStringAsync();

            return response.Split('=')[1];
        }
    }
}