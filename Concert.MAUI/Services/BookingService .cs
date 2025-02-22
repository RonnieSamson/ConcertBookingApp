using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Concert.MAUI.Models;

namespace Concert.MAUI.Services
{
    public  class BookingService : IBookingService
    {
        private readonly IRestService _restService;
        private readonly IMapper _mapper;

        public BookingService(IRestService restService, IMapper mapper)
        {
            _restService = restService;
            _mapper = mapper;
        }
        public async Task<bool> BookPerformanceAsync(string userid, string concertId)
        {
            var booking = new Booking { UserId = userid, ConcertId = concertId, BookingDate = DateTime.UtcNow };
            var response = await _restService.PostAsync<Booking>("bookings", booking);
            return response != null;
        }

        
    }
}
