using Concert.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concert.Data.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {

        public ApplicationDbContext DbContext => Context as ApplicationDbContext;
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Booking> GetBookingByIdAsync(string id)
        {
            return await DbContext.Bookings.FindAsync(id);
        }

        public async Task<Booking?> GetBookingByUserIdAsync(string userId)
        {
            return await DbContext.Bookings.FirstOrDefaultAsync(b => b.UserId == userId);
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            return await DbContext.Bookings.ToListAsync();
        }

        public async Task AddBookingAsync(Booking booking)
        {
            await DbContext.Bookings.AddAsync(booking);
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            DbContext.Bookings.Update(booking);
        }

        public async Task DeleteBookingAsync(Booking booking)
        {
            DbContext.Bookings.Remove(booking);
        }





    }
}
