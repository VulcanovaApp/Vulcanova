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
        public static Dictionary<string, string> GetSignatureHeaders(string fingerprint,
            string privateKey,
            string body,
            string requestUrl,
            DateTime? timestamp = null)
        {
            timestamp ??= DateTime.UtcNow;

            var formattedTimestamp = timestamp.Value.ToString("R");

            var headers = new Dictionary<string, string>
            {
                {"vCanonicalUrl", GetEncodedPath(requestUrl)},
            };

            if (!string.IsNullOrEmpty(body))
            {
                var digest = GetDigest(body);
                headers.Add("Digest", digest);
            }
            
            headers.Add("vDate", formattedTimestamp);
            headers.Add("Signature", BuildSignatureHeaderValue(headers, fingerprint, privateKey));
            
            if (headers.ContainsKey("Digest"))
            {
                headers["Digest"] = $"SHA-256={headers["Digest"]}";
            }

            return headers;
        }

        private static string BuildSignatureHeaderValue(Dictionary<string, string> headers, string fingerprint, string privateKey) =>
            $"keyId=\"{fingerprint}\",headers=\"{string.Join(" ", headers.Keys)}\",algorithm=\"sha256withrsa\"," +
            $"signature=Base64(SHA256withRSA({GetHeadersSignature(string.Join("", headers.Values), privateKey)}))";

        private static string GetEncodedPath(string path)
        {
            var rx = Regex.Match(path, "(api/mobile/.+)");

            return HttpUtility.UrlEncode(rx.Value);
        }

        private static string GetDigest(string body)
        {
            using var sha = SHA256.Create();
            var data = sha.ComputeHash(Encoding.UTF8.GetBytes(body));

            return Convert.ToBase64String(data);
        }

        private static string GetHeadersSignature(string values, string privateKey)
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