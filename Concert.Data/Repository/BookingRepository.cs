using Concert.Data;
using Concert.Data.Entity;
using Concert.Data.Repository;
using Microsoft.EntityFrameworkCore;

public class BookingRepository : Repository<Booking>, IBookingRepository
{
    public ApplicationDbContext? DbContext => Context as ApplicationDbContext;

    public BookingRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Booking?> GetBookingByIdAsync(string id)
    {
        if (DbContext == null) return null;
        
        return await DbContext.Bookings
            .Include(b => b.Performance)
                .ThenInclude(p => p!.Concert)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(string userId)
    {
        // Legacy method - for backwards compatibility, could search by customer email if needed
        var bookings = await All();
        return bookings.ToList(); 
    }

    public async Task<IEnumerable<Booking>> GetBookingsByEmailAsync(string email)
    {
        if (DbContext == null) return new List<Booking>();
        
        return await DbContext.Bookings
            .Include(b => b.Performance)
                .ThenInclude(p => p!.Concert)
            .Where(b => b.CustomerEmail == email)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetBookingsAsync()
    {
        if (DbContext == null) return new List<Booking>();
        
        return await DbContext.Bookings
            .Include(b => b.Performance)
                .ThenInclude(p => p!.Concert)
            .ToListAsync();
    }

    public void AddBooking(Booking booking)
    {
        Insert(booking);
    }

    public void UpdateBooking(Booking booking)
    {
        Update(booking);
    }

    public void DeleteBooking(Booking booking)
    {
        Delete(booking);
    }
}
