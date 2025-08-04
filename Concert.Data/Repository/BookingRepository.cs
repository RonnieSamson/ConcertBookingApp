using Concert.Data;
using Concert.Data.Entity;
using Concert.Data.Repository;

public class BookingRepository : Repository<Booking>, IBookingRepository
{
    public ApplicationDbContext? DbContext => Context as ApplicationDbContext;

    public BookingRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Booking?> GetBookingByIdAsync(string id)
    {
        return (await Find(b => b.Id == id)).FirstOrDefault();
    }

    public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(string userId)
    {
        // Legacy method - for backwards compatibility, could search by customer email if needed
        var bookings = await All();
        return bookings.ToList(); 
    }

    public async Task<IEnumerable<Booking>> GetBookingsByEmailAsync(string email)
    {
        var bookings = await Find(b => b.CustomerEmail == email);
        return bookings.ToList();
    }

    public async Task<IEnumerable<Booking>> GetBookingsAsync()
    {
        return await All();
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
