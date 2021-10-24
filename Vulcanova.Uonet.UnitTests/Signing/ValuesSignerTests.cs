using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Vulcanova.Uonet.Signing;
using Xunit;

namespace Vulcanova.Uonet.UnitTests.Signing
{
    public class ValuesSignerTests
    {
        [Fact]
        public void GetSignatureHeaders_ValuesWithBody_ReturnsCorrectHeaders()
        {
            const string fullUrl = "https://uonetplus-komunikacja.resman.pl/rzeszow/api/mobile/register/new";
            const string requestBody = "{ \"API\": 1 }";
            var date = new DateTime(2021, 10, 24);
            var (key, cert) = LoadCredentials();

            var values = ValuesSigner.GetSignatureHeaders(cert.Thumbprint, key, requestBody, fullUrl, date);
            
            Assert.Equal("api%2fmobile%2fregister%2fnew", 
                values["vCanonicalUrl"]);
            Assert.Equal("SHA-256=L2vGJ+gewtycjeTU+r8TtiAPLESbAQprwTYUze7znME=", 
                values["Digest"]);
            Assert.Equal("Sun, 24 Oct 2021 00:00:00 GMT", 
                values["vDate"]);
            Assert.Equal("keyId=\"44671E5D3BB7E3BBE00FF914E8008B0AB1ED1FC3\",headers=\"vCanonicalUrl Digest vDate\",algorithm=\"sha256withrsa\",signature=Base64(SHA256withRSA(f6GAMA53DBwALHBcrQVl07JALUvZfKVEQ72lhNIv/02e+37ZsfiqvNz4niHNVdayRTFhXo0uQ3A32yWZMwEBZvO60sqH3hB+vf4Y5bgdY69f5+IebaKaciqr7mY7vcWuuPEFmDgRBb/9YO65HoyHnX9oflqsZAZBvsqSsNlcsoUynRZC5R/kaskk7yuiMAhPurBtTvCCYluh6y25kFGCzvQiOTkXBBvR5GeIaD4swnG+t70QpZBDFfncSbbrK2iIT8rnXL5yZ52olDAJk9cUs07ZHk+OOuYgvrlBUu/Wl84xiIY0gMaZuTpkh3ZYE6RzSrMhkLZEaDDG4dIhdyl1+w==))", 
                values["Signature"]);
        }

        [Fact]
        public void GetSignatureHeaders_ValuesWithNullBody_ReturnsHeadersWithoutDigest()
        {
            const string fullUrl = "https://uonetplus-komunikacja.resman.pl/rzeszow/api/mobile/register/new";
            var date = new DateTime(2021, 10, 24);
            var (key, cert) = LoadCredentials();

            var values =
                ValuesSigner.GetSignatureHeaders(cert.Thumbprint, key, null, fullUrl, date);
            
            Assert.DoesNotContain("Digest", values.Keys);
        }

        private static (string, X509Certificate2) LoadCredentials()
        {
            var pemPrivateKey = File.ReadAllText("Resources/key.pem");
            var pemCert = new X509Certificate2("Resources/cert.pem");

            return (pemPrivateKey, pemCert);
        }
    }
}