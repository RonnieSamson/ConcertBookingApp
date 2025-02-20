namespace Concert.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IBookingRepository Bookings { get; }
        IConcertRepository Concerts { get; }
        IPerformanceRepository Performances { get; }
        Task<int> CompleteAsync();

    }
}
