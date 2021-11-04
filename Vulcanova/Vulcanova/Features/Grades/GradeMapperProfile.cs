using System;
using AutoMapper;
using Vulcanova.Uonet.Api.Grades;

namespace Vulcanova.Features.Grades
{
    public class GradeMapperProfile : Profile
    {
        public GradeMapperProfile()
        {
            CreateMap<GradePayload, Grade>()
                .ForMember(g => g.CreatorName, cfg => cfg.MapFrom(src => src.Creator.DisplayName));

            CreateMap<Uonet.Api.Grades.Column, Column>();

            CreateMap<Date, DateTime>()
                .ConvertUsing(
                    d => d == null ? DateTime.MinValue : DateTimeOffset.FromUnixTimeMilliseconds(d.Timestamp).UtcDateTime);
        }
    }
}