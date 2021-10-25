using AutoMapper;
using Vulcanova.Features.Auth.Accounts;
using Vulcanova.Uonet.Api.Auth;
using ConstituentUnit = Vulcanova.Features.Auth.Accounts.ConstituentUnit;
using Pupil = Vulcanova.Features.Auth.Accounts.Pupil;
using Unit = Vulcanova.Features.Auth.Accounts.Unit;

namespace Vulcanova.Features.Auth
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<AccountPayload, Account>();
            CreateMap<Uonet.Api.Auth.Pupil, Pupil>();
            CreateMap<Uonet.Api.Auth.Unit, Unit>();
            CreateMap<Uonet.Api.Auth.ConstituentUnit, ConstituentUnit>();
        }
    }
}