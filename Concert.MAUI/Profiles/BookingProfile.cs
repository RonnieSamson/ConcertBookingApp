namespace Concert.MAUI.Profiles
{
    public class BookingProfile : AutoMapper.Profile
    {
        public BookingProfile()
        {
            CreateMap<Concert.MAUI.Models.Booking, Concert.Data.DTO.BookingDto>()
                .ForMember(dest => dest.PerformanceId, opt => opt.MapFrom(src => src.PerformanceId))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.CustomerEmail));

            CreateMap<Concert.Data.DTO.BookingDto, Concert.MAUI.Models.Booking>()
                .ForMember(dest => dest.PerformanceId, opt => opt.MapFrom(src => src.PerformanceId))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.CustomerEmail));
        }
    }
}
