using AutoMapper;
using ConcertBookingApp.Data.DTO;
using ConcertBookingApp.Data.Entity;

namespace ConsertBookingApp.API.Profiles
{
    public class ConsertProfile : Profile
    {
        public ConsertProfile()
        {
            CreateMap<Concert, ConsertDTO>().ReverseMap();
        }
    }
}
