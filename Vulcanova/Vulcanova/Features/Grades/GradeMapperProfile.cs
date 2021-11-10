using System;
using AutoMapper;
using Vulcanova.Uonet.Api.Grades;
using Subject = Vulcanova.Features.Shared.Subject;

namespace Vulcanova.Features.Grades
{
    public class GradeMapperProfile : Profile
    {
        public GradeMapperProfile()
        {
            CreateMap<GradePayload, Grade>()
                .ForMember(g => g.CreatorName, cfg => cfg.MapFrom(src => src.Creator.DisplayName));

            CreateMap<Uonet.Api.Grades.Column, Column>();

            CreateMap<Uonet.Api.Grades.Subject, Subject>();

            CreateMap<Date, DateTime>()
                .ConvertUsing(d => DateTimeOffset.FromUnixTimeMilliseconds(d.Timestamp).UtcDateTime);
        }
    }
}