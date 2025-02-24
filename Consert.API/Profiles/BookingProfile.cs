using AutoMapper;
using Concert.Data.DTO;
using Concert.Data.Entity;

namespace Concert.API.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            // DTO → Entity
            CreateMap<BookingDto, Booking>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID sätts av databasen
                .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            // Entity → DTO
            CreateMap<Booking, BookingDto>();
        }
    }
}
