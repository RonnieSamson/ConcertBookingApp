using Concert.Data.DTO;

namespace Concert.MAUI.Profiles
{
    public class PerformanceProfile : AutoMapper.Profile
    {
        public PerformanceProfile()
        {
            CreateMap<Concert.MAUI.Models.Performance, PerformanceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.ConcertId, opt => opt.MapFrom(src => src.ConcertId))
                .ForMember(dest => dest.BookingCount, opt => opt.MapFrom(src => src.BookingCount));

            CreateMap<PerformanceDto, Concert.MAUI.Models.Performance>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.ConcertId, opt => opt.MapFrom(src => src.ConcertId))
                .ForMember(dest => dest.BookingCount, opt => opt.MapFrom(src => src.BookingCount));


        }
    }
}
