using Concert.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concert.Data.Repository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<Booking> GetBookingByIdAsync(string id);
        Task<Booking?> GetBookingByUserIdAsync(string userId);   
        Task<IEnumerable<Booking>> GetBookingsAsync();
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(Booking booking);
    }
}
