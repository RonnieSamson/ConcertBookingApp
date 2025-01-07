using AutoMapper;
using ConcertBookingApp.Data.DTO;
using ConsertBookingApp.MAUI.Models;

namespace ConsertBookingApp.MAUI.Profiles
{
    public class ConsertProfile : Profile
    {
        public ConsertProfile()
        {
            CreateMap<Concert, ConcertDto>().ReverseMap();
        }
       
    }
}
