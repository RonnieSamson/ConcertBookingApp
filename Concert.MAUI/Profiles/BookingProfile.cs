namespace Concert.MAUI.Profiles
{
    public class BookingProfile : AutoMapper.Profile
    {
        public BookingProfile()
        {
            CreateMap<Concert.MAUI.Models.Booking, Concert.Data.DTO.BookingDto>()
                
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<Concert.Data.DTO.BookingDto, Concert.MAUI.Models.Booking>()
                
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        }
    }
}
