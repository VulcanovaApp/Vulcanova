namespace Vulcanova.Uonet.Api.Auth
{
    public class RegisterClientRequest : IApiRequest
    {
        public string OS { get; set; }
        public string DeviceModel { get; set; }
        public string Certificate { get; set; }
        public string CertificateType { get; set; }
        public string CertificateThumbprint { get; set; }
        public string PIN { get; set; }
        public string SecurityToken { get; set; }
        public string SelfIdentifier { get; set; }
    }
}