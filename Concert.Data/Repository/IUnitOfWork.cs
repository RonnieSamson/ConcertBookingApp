using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concert.Data.Entity;
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
