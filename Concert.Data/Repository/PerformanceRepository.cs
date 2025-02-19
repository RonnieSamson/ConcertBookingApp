using Concert.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concert.Data.Repository
{
    public class PerformanceRepository : Repository<Performance>, IPerformanceRepository
    {
        public ApplicationDbContext DbContext => Context as ApplicationDbContext;

        public PerformanceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Performance> GetPerformanceByIdAsync(string id)
        {
            return (await Find(p => p.Id == id)).FirstOrDefault();
        }

        public async Task<IEnumerable<Performance>> GetPerformancesAsync()
        {
            return await All();
        }

        public void AddPerformance(Performance performance)
        {
            Insert(performance);
        }

        public void UpdatePerformance(Performance performance)
        {
            Update(performance);
        }

        public void DeletePerformance(Performance performance)
        {
            Delete(performance);
        }



    }
}
