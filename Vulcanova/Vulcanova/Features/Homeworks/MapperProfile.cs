using AutoMapper;
using Vulcanova.Uonet.Api.Homeworks;

namespace Vulcanova.Features.Homeworks
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<HomeworkPayload, HomeworkEntry>();
        }
    }
}