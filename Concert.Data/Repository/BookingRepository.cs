﻿using Concert.Data;
using Concert.Data.Entity;
using Concert.Data.Repository;

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

    public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(string userId)
    {
        var bookings = await Find(b => b.UserId == userId);
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
