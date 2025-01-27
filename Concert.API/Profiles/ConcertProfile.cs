using AutoMapper;
using ConcertBookingApp.Data.Entity;
using Concert.DTO;

namespace Concert.API.Profiles
{
    public class ConsertProfile : Profile
    {
        public ConsertProfile()
        {
            CreateMap<ConcertBookingApp.Data.Entity.Concert, ConcertDto>().ReverseMap();
        }
    }
}
