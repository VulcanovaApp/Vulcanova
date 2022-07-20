using System;
using AutoMapper;
using Vulcanova.Features.Auth.Accounts;
using Vulcanova.Uonet.Api.Auth;
using ConstituentUnit = Vulcanova.Features.Auth.Accounts.ConstituentUnit;
using Period = Vulcanova.Features.Shared.Period;
using Pupil = Vulcanova.Features.Auth.Accounts.Pupil;
using Unit = Vulcanova.Features.Auth.Accounts.Unit;

namespace Vulcanova.Features.Auth;

public class AccountMapperProfile : Profile
{
    public AccountMapperProfile()
    {
        CreateMap<AccountPayload, Account>();
        CreateMap<Uonet.Api.Auth.Pupil, Pupil>();
        CreateMap<Uonet.Api.Auth.Unit, Unit>();
        CreateMap<Uonet.Api.Auth.ConstituentUnit, ConstituentUnit>();
        CreateMap<Uonet.Api.Auth.Period, Period>();
        CreateMap<Uonet.Api.Auth.YearEnd, DateTime>()
            .ConvertUsing(y => DateTimeOffset.FromUnixTimeMilliseconds(y.Timestamp).Date);
    }
}