using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concert.MAUI.Models;

namespace Concert.MAUI.Services
{
    public interface IBookingService
    {
        Task<bool> BookPerformanceAsync(string performanceId, string customerName, string customerEmail);
        Task<IEnumerable<Booking>?> GetBookingsByEmailAsync(string email);
        Task<bool> CancelBookingAsync(string bookingId);
    }
}
