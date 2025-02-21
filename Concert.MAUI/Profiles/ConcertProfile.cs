using AutoMapper;
using Concert.Data.DTO;

namespace Concert.MAUI.Profiles
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            CreateMap<Concert.MAUI.Models.Concert, ConcertDto>()
                .ForMember(dest => dest.ConcertId, opt => opt.MapFrom(src => src.ConcertId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<ConcertDto, Concert.MAUI.Models.Concert>()
                .ForMember(dest => dest.ConcertId, opt => opt.MapFrom(src => src.ConcertId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        }

    }
}
