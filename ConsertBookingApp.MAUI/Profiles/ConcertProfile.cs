using AutoMapper;
using ConcertBookingApp.Data.DTO;
using ConcertBookingApp.MAUI.Models;

namespace ConcertBookingApp.MAUI.Profiles
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            CreateMap<Concert, ConcertDto>().ReverseMap();
        }
       
    }
}
