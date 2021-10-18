using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace Vulcanova.Uonet.Signing
{
    // Based on https://github.com/wulkanowy/uonet-request-signer/tree/master/hebe-dotnet
    public static class ValuesSigner
    {
        public static (string digest, string canonicalUrl, string signature) GetSignatureValues(string fingerprint,
            string privateKey,
            string body, string requestPath, DateTime timestamp)
        {
            var digest = GetDigest(body);
            var formattedTimestamp = timestamp.ToString("R");
            var headers = GetHeaders(requestPath, formattedTimestamp, digest);

            return
            (
                $"SHA-256={digest}",
                headers.Values.First(),
                $"keyId=\"{fingerprint}\",headers=\"{string.Join(" ", headers.Keys)}\",algorithm=\"sha256withrsa\"," +
                $"signature=Base64(SHA256withRSA({GetSignatureValues(string.Join("", headers.Values), privateKey)}))"
            );
        }

        private static Dictionary<string, string> GetHeaders(string url, string date, string digest) =>
            new Dictionary<string, string>
            {
                {"vCanonicalUrl", GetEncodedPath(url)},
                {"Digest", digest},
                {"vDate", date}
            };

        private static string GetEncodedPath(string path)
        {
            var rx = Regex.Match(path, "(api/mobile/.+)");

            return HttpUtility.UrlEncode(rx.Value);
        }

        private static string GetDigest(string body)
        {
            var sha = SHA256.Create();
            var data = sha.ComputeHash(Encoding.UTF8.GetBytes(body));

            return Convert.ToBase64String(data);
        }

        private static string GetSignatureValues(string values, string privateKey)
        {
            var textReader = new StringReader(privateKey);
            var reader = new PemReader(textReader);
            var obj = reader.ReadObject();

            var key = (RsaPrivateCrtKeyParameters) obj;

            var dataToSign = Encoding.UTF8.GetBytes(values);

            var signer = SignerUtilities.GetSigner("SHA256WITHRSA");
            signer.Init(true, key);
            signer.BlockUpdate(dataToSign, 0, dataToSign.Length);

            var signature = signer.GenerateSignature();
            return Convert.ToBase64String(signature);
        }
    }
}