using Concert.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concert.Data.Repository
{
    public interface IPerformanceRepository : IRepository<Performance>
    {
        Task<Performance> GetPerformanceByIdAsync(string id);
        Task<IEnumerable<Performance>> GetPerformancesAsync();
        void AddPerformance(Performance performance);
        void UpdatePerformance(Performance performance);
        void DeletePerformance(Performance performance);
    }
}
