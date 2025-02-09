using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Concert.MAUI.Models;
using Concert.DTO;

namespace Concert.MAUI.Profiles
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            CreateMap<ConcertDto, Konsert>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Titel, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Beskrivning, opt => opt.MapFrom(src => src.Description));
            CreateMap<Konsert, ConcertDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Titel))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Beskrivning));

        }

    }
}
