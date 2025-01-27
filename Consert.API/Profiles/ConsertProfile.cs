using AutoMapper;
using ConcertBookingApp.Data.DTO;
using ConcertBookingApp.Data.Entity;

namespace ConcertBookingApp.API.Profiles
{
    public class ConsertProfile : Profile
    {
        public ConsertProfile()
        {
            CreateMap<Concert, ConcertDto>().ReverseMap();
        }
    }
}
