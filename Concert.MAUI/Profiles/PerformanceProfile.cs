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
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime));

            CreateMap<PerformanceDto, Concert.MAUI.Models.Performance>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime));


        }
    }
}
