using AutoMapper;
using Concert.Data.DTO;
using Concert.Data.Entity;

namespace Concert.API.Profiles
{
    public class PerformanceProfile : Profile
    {
        public PerformanceProfile()
        {
            // DTO → Entity
            CreateMap<PerformanceDto, Performance>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID sätts av controllern
                .ForMember(dest => dest.Concert, opt => opt.Ignore()) // Navigation property handled separately
                .ForMember(dest => dest.Bookings, opt => opt.Ignore()); // Navigation property handled separately

            // Entity → DTO
            CreateMap<Performance, PerformanceDto>()
                .ForMember(dest => dest.BookingCount, opt => opt.MapFrom(src => src.Bookings.Count()));
        }
    }
}
