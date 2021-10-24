using AutoMapper;
using Vulcanova.Uonet.Api.Auth;

namespace Vulcanova.Features.Auth
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<AccountPayload, Account>();
        }
    }
}