using Concert.Data.Entity;
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
        
        public Task AddBookingAsync(Booking booking)
        {
            throw new NotImplementedException();
        }
        public Task DeleteBookingAsync(Booking booking)
        {
            throw new NotImplementedException();
        }
        public Task<Booking> GetBookingByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
        public Task<Booking?> GetBookingByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
        public Task UpdateBookingAsync(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
    
    
}
