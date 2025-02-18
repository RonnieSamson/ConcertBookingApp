using Concert.Data.Repository;
using Concert.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;

    public IUserRepository Users { get; }
    public IBookingRepository Bookings { get; }
    public IConcertRepository Concerts { get; }
    public IPerformanceRepository Performances { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Users = new UserRepository(_context);
        Bookings = new BookingRepository(_context);
        Concerts = new ConcertRepository(_context);
        Performances = new PerformanceRepository(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}