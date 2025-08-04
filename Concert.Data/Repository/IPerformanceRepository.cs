using Concert.Data.Entity;

namespace Concert.Data.Repository
{
    public interface IPerformanceRepository : IRepository<Performance>
    {
        Task<Performance?> GetPerformanceByIdAsync(string id);
        Task<IEnumerable<Performance>> GetPerformancesAsync();
        Task<IEnumerable<Performance>> GetPerformancesByConcertIdAsync(string concertId);
        void AddPerformance(Performance performance);
        void UpdatePerformance(Performance performance);
        void DeletePerformance(Performance performance);
    }
}
