using System;
using System.IO;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Vulcanova.Uonet.Crypto
{
    public static class KeyPairGenerator
    {
        public static (string Fingerprint, string PrivateKey, string Cert) GenerateKeyPair()
        {
            var keygen = new RsaKeyPairGenerator();
            keygen.Init(new KeyGenerationParameters(new SecureRandom(), 2048));

            var pair = keygen.GenerateKeyPair();

            var cert = GenerateCert(pair);
            var fingerprint = cert.GetFingerprint();

            var pkcs8 = new Pkcs8Generator(pair.Private);
            var privateKey = pkcs8.Generate().DumpToString();

            return (fingerprint, privateKey, cert.DumpToString());
        }

        private static X509Certificate GenerateCert(AsymmetricCipherKeyPair pair)
        {
            var x509 = new X509V3CertificateGenerator();
            x509.SetSerialNumber(BigInteger.One);

            var dn = new X509Name("CN=APP_CERTIFICATE CA Certificate");
            
            x509.SetSubjectDN(dn);
            x509.SetIssuerDN(dn);
            
            x509.SetPublicKey(pair.Public);

            x509.SetNotBefore(DateTime.UtcNow);
            x509.SetNotAfter(DateTime.UtcNow.AddYears(15));
            
            var signatureFactory = new Asn1SignatureFactory("SHA256WITHRSA", pair.Private);

            return x509.Generate(signatureFactory);
        }
    }
}