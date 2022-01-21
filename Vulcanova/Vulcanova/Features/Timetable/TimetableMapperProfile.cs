using System;
using System.Globalization;
using AutoMapper;
using Vulcanova.Features.Timetable.Changes;
using Vulcanova.Uonet.Api.Schedule;

namespace Vulcanova.Features.Timetable
{
    public class TimetableMapperProfile : Profile
    {
        public TimetableMapperProfile()
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById("Europe/Warsaw");

            CreateMap<ScheduleEntry, TimetableEntry>()
                .ForMember(dest => dest.RoomName, cfg => cfg.MapFrom(src => src.Room.Code))
                .ForMember(dest => dest.TeacherName, cfg => cfg.MapFrom(src => src.TeacherPrimary.DisplayName))
                .ForMember(dest => dest.Start, cfg => cfg.MapFrom(src => TimeZoneInfo.ConvertTimeToUtc(DateTime.ParseExact(src.TimeSlot.Start, "HH:mm", CultureInfo.InvariantCulture), tz)))
                .ForMember(dest => dest.End, cfg => cfg.MapFrom(src => TimeZoneInfo.ConvertTimeToUtc(DateTime.ParseExact(src.TimeSlot.End, "HH:mm", CultureInfo.InvariantCulture), tz)));

            CreateMap<ScheduleChangeEntry, TimetableChangeEntry>()
                .ForMember(dest => dest.RoomName, cfg => cfg.MapFrom(src => src.Room.Code))
                .ForMember(dest => dest.TeacherName, cfg => cfg.MapFrom(src => src.TeacherPrimary.DisplayName))
                .ForMember(dest => dest.TimetableEntryId, cfg => cfg.MapFrom(src => src.ScheduleId));
        }
    }
}