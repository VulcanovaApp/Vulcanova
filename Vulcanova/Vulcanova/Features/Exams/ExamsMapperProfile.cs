using AutoMapper;
using Vulcanova.Uonet.Api.Exams;

namespace Vulcanova.Features.Exams
{
    public class ExamsMapperProfile : Profile
    {
        public ExamsMapperProfile()
        {
            CreateMap<ExamPayload, Exam>()
                .ForMember(e => e.CreatorName, cfg => cfg.MapFrom(src => src.Creator.DisplayName));
        }
    }
}