using AutoMapper;
using Concert.Data.DTO;
using Concert.Data.Entity;

namespace Concert.API.Profiles
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            // DTO → Entity
            CreateMap<ConcertDto, ConcertEntity>()
                .ForMember(dest => dest.ConcertId, opt => opt.Ignore()) // ID sätts av controllern
                .ForMember(dest => dest.Performances, opt => opt.Ignore()); // Navigation property handled separately

            // Entity → DTO
            CreateMap<ConcertEntity, ConcertDto>()
                .ForMember(dest => dest.BookingCount, opt => opt.MapFrom(src => 
                    src.Performances.SelectMany(p => p.Bookings).Count()));
        }
    }
}
