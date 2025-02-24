using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Concert.Data.DTO;
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
        public async Task<bool> BookPerformanceAsync(string userId, string concertId)
        {
            var bookingDto = new BookingDto
            {
                UserId = userId,
                ConcertId = concertId
            };

            var response = await _restService.PostAsync<BookingDto>("bookings/book", bookingDto);
            return response != null;
        }


    }
}
