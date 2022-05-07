using System;
using AutoMapper;
using Vulcanova.Features.Shared;
using Vulcanova.Uonet.Api.Homework;

namespace Vulcanova.Features.Homework
{
    public class HomeworkMapperProfile : Profile
    {
        public HomeworkMapperProfile()
        {
            CreateMap<Uonet.Api.Common.Models.Subject, Subject>();
            CreateMap<HomeworkPayload, Homework>()
                .ForMember(h => h.PupilId, cfg => cfg.MapFrom(src => src.IdPupil))
                .ForMember(h => h.HomeworkId, cfg => cfg.MapFrom(src => src.IdHomework))
                .ForMember(h => h.CreatorName, cfg => cfg.MapFrom(src => src.Creator.DisplayName));
        }
    }
}