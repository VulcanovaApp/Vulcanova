using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vulcanova.Uonet.Api;

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

        public ValueTask<Dictionary<string, string>> CreateSignedHeaders(string fullUrl)
            => CreateSignedHeaders(null, fullUrl);

        public ValueTask<Dictionary<string, string>> CreateSignedHeaders(string body, string fullUrl)
        {
            var signatureHeaders = ValuesSigner.GetSignatureHeaders(_fingerprint, _privateKey, body, fullUrl);

            signatureHeaders["User-Agent"] = Constants.UserAgent;
            signatureHeaders["vOS"] = Constants.AppOs;
            signatureHeaders["vDeviceModel"] = Constants.DeviceModel;
            signatureHeaders["vAPI"] = "1";
            signatureHeaders["Content-Type"] = "application/json";

            return new ValueTask<Dictionary<string, string>>(signatureHeaders);
        }

        public ValueTask<SignedApiPayload> SignPayload(object o)
        {
            return new ValueTask<SignedApiPayload>(new SignedApiPayload
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
            });
        }
    }
}