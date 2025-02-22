using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concert.MAUI.Models;

namespace Concert.MAUI.Services
{
    public interface IPerformanceService
    {
        Task<List<Performance>?> GetPerformancesByConcertIdAsync(string concertId);
    }
}
