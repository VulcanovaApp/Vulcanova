namespace Vulcanova.Uonet.Api.Auth
{
    // Why is it a GET request...
    public class RegisterHebeClientQuery : IApiQuery<AccountPayload[]>
    {
        public const string ApiEndpoint = "api/mobile/register/hebe";
    }
}