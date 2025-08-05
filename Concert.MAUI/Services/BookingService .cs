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
        
        public async Task<bool> BookPerformanceAsync(string performanceId, string customerName, string customerEmail)
        {
            var bookingDto = new BookingDto
            {
                PerformanceId = performanceId,
                CustomerName = customerName,
                CustomerEmail = customerEmail,
                BookingDate = DateTime.UtcNow
            };

            var response = await _restService.PostAsync<BookingDto>("bookings", bookingDto);
            return response != null;
        }

        public async Task<IEnumerable<Booking>?> GetBookingsByEmailAsync(string email)
        {
            var bookingDtos = await _restService.GetAsync<IEnumerable<BookingDto>>($"bookings/byEmail/{email}");
            return bookingDtos?.Select(dto => _mapper.Map<Booking>(dto));
        }

        public async Task<bool> CancelBookingAsync(string bookingId)
        {
            return await _restService.DeleteAsync($"bookings/{bookingId}");
        }
    }
}
