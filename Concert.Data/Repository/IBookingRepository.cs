using Concert.Data.Entity;

namespace Concert.Data.Repository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<Booking> GetBookingByIdAsync(string id);
        Task<Booking?> GetBookingByUserIdAsync(string userId);
        Task<IEnumerable<Booking>> GetBookingsAsync();
        void AddBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void DeleteBooking(Booking booking);
    }
}
