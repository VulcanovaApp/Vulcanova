using System;
using System.IO;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO.Pem;
using Org.BouncyCastle.X509;
using PemWriter = Org.BouncyCastle.OpenSsl.PemWriter;

namespace Vulcanova.Uonet.Crypto
{
    public static class CryptoExtensions
    {
        public static string DumpToString(this PemObject @object)
        {
            return DumpToString((object) @object);
        }
        
        public static string DumpToString(this X509Certificate cert)
        {
            return DumpToString((object) cert);
        }

        private static string DumpToString(object o)
        {
            using var textWriter = new StringWriter();
            var pemWriter = new PemWriter(textWriter);
            pemWriter.WriteObject(o);

            return textWriter.ToString();
        }

        public static string GetFingerprint(this X509Certificate x509Certificate)
        {
            var digest = DigestUtilities.CalculateDigest("sha1", x509Certificate.GetEncoded());
            return BitConverter.ToString(digest).Replace("-", "").ToLower();
        }
    }
}