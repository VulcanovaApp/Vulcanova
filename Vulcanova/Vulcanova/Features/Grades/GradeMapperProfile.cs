using System;
using AutoMapper;
using Vulcanova.Features.Grades.Final;
using Vulcanova.Uonet.Api.Common.Models;
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

            CreateMap<Uonet.Api.Common.Models.Subject, Subject>();

            CreateMap<Date, DateTime>()
                .ConvertUsing(d => DateTimeOffset.FromUnixTimeMilliseconds(d.Timestamp).UtcDateTime);

            CreateMap<GradesSummaryEntryPayload, FinalGradesEntry>()
                .ForMember(f => f.Id, cfg => cfg.MapFrom(src => $"{src.PeriodId}_{src.Id}"))
                .ForMember(f => f.PredictedGrade, cfg => cfg.MapFrom(src => src.Entry1))
                .ForMember(f => f.FinalGrade, cfg => cfg.MapFrom(src => src.Entry2));
        }
    }
}