using System.IO;
using Org.BouncyCastle.Utilities.IO.Pem;
using PemWriter = Org.BouncyCastle.OpenSsl.PemWriter;

namespace Vulcanova.Uonet.Crypto
{
    public static class CryptoExtensions
    {
        public static string DumpToString(this PemObject @object)
        {
            return DumpToString((object) @object);
        }

        private static string DumpToString(object o)
        {
            using var textWriter = new StringWriter();
            var pemWriter = new PemWriter(textWriter);
            pemWriter.WriteObject(o);

            return textWriter.ToString();
        }
    }
}