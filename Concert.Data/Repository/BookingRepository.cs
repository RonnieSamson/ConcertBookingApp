using Concert.Data.Repository;
using Concert.Data;
using Concert.Data.Entity;

public class BookingRepository : Repository<Booking>, IBookingRepository
{
    public ApplicationDbContext DbContext => Context as ApplicationDbContext;

    public BookingRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Booking?> GetBookingByIdAsync(string id)
    {
        return (await Find(b => b.Id == id)).FirstOrDefault();
    }

    public async Task<Booking?> GetBookingByUserIdAsync(string userId)
    {
        return (await Find(b => b.UserId == userId)).FirstOrDefault();
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
        DbContext.Bookings.Update(booking); 
    }

    public void DeleteBooking(Booking booking)
    {
        Delete(booking); 
    }
}
