using System;
using System.Collections.Generic;

namespace Vulcanova.Uonet.Signing
{
    public class RequestSigner : IRequestSigner
    {
        private readonly string _fingerprint;
        private readonly string _privateKey;
        private readonly string _firebaseToken;

        public RequestSigner(string fingerprint, string privateKey, string firebaseToken)
        {
            _fingerprint = fingerprint;
            _privateKey = privateKey;
            _firebaseToken = firebaseToken;
        }

        public Dictionary<string, string> CreateSignedHeaders(string body, string fullUrl)
        {
            var date = DateTime.UtcNow;

            var (digest, canonicalUrl, signature) = ValuesSigner.GetSignatureValues(_fingerprint, _privateKey, body, fullUrl, date);

            return new Dictionary<string, string>
            {
                {"User-Agent", Constants.UserAgent},
                {"vOS", Constants.AppOs},
                {"vDeviceModel", Constants.DeviceModel},
                {"vAPI", "1"},
                {"vDate", date.ToString("R")},
                {"vCanonicalUrl", canonicalUrl},
                {"Signature", signature},
                {"Digest", digest},
                {"Content-Type", "application/json"}
            };
        }

        public SignedApiPayload SignPayload(object o)
        {
            return new SignedApiPayload
            {
                AppName = Constants.AppName,
                AppVersion = Constants.AppVersion,
                CertificateId = _fingerprint,
                FirebaseToken = _firebaseToken,
                API = 1,
                RequestId = Guid.NewGuid(),
                Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                TimestampFormatted = DateTime.Now.ToString("yyyy-M-d HH:mm:ss"),
                Envelope = o
            };
        }
    }
}