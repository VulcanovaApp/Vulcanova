using Vulcanova.Uonet.Api.Common;
using Xunit;

namespace Vulcanova.Uonet.UnitTests.Api.Common
{
    public class InstanceUrlProviderTests
    {
        [Fact]
        public void ExtractInstanceUrlFromRequestUrl_QrCodeReturnedUrl_ReturnsInstanceUrl()
        {
            const string qrCodeUrl = "https://uonetplus-komunikacja.resman.pl/rzeszow/mobile-api";

            var instanceUrlProvider = new InstanceUrlProvider();

            var instanceUrl = instanceUrlProvider.ExtractInstanceUrlFromRequestUrl(qrCodeUrl);
            
            Assert.Equal("https://uonetplus-komunikacja.resman.pl/rzeszow", instanceUrl);
        }
    }
}