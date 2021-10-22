using System;

namespace Vulcanova.Uonet.Api
{
    public class SignedApiPayload
    {
        public string AppName { get; set; }
        public string AppVersion { get; set; }
        public string CertificateId { get; set; }
        public object Envelope { get; set; }
        public string FirebaseToken { get; set; }
        public int API { get; set; }
        public Guid RequestId { get; set; }
        public long Timestamp { get; set; }
        public string TimestampFormatted { get; set; }
    }
}